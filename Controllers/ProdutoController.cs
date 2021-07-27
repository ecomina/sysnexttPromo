using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Authentic.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ListParametrs {
    public bool Ativo {get; set;}
}


[Route("v1/Produto")]
public class ProdutoController : ControllerBase 
{
    DaoContext context = new DaoContext();

        [HttpGet]
        [Route("Filiais")]
        [AllowAnonymous]
        public List<Filial> Filiais()
        {
            var context = new DaoContext();
            var list = context.Filiais.ToList();

            return list.ToList();
        }

        [HttpGet]
        [Route("UnidadeMedidas")]
        [AllowAnonymous]
        public IEnumerable<dynamic> UnidadeMedidas()
        {
            var context = new DaoContext();
            var list = context.UnidadeMedidas.ToList();

            return list;
        }

        [HttpGet]
        [Route("Marcas")]
        [AllowAnonymous]
        public IEnumerable<dynamic> Marcas()
        {
            var context = new DaoContext();
            var list = context.Marcas.Include(p => p.Produtos).Select(s => new {
                s.Id,
                s.Descricao,
                QtdeProdutos = s.Produtos.Count
            });

            return list.OrderBy(o => o.Descricao).ToList();
        }

        [HttpGet]
        [Route("Cores")]
        [AllowAnonymous]
        public IEnumerable<dynamic> Cores()
        {
            var context = new DaoContext();
            var list = context.Cores.ToList();

            return list;
        }

        [HttpGet]
        [Route("Tamanhos")]
        [AllowAnonymous]
        public IEnumerable<dynamic> Tamanhos()
        {
            var context = new DaoContext();
            var list = context.Tamanhos.ToList();

            return list;
        }

        [HttpGet]
        [Route("RamoAtividades")]
        [AllowAnonymous]
        public IEnumerable<dynamic> RamoAtividades()
        {
            var context = new DaoContext();
            var list = context.RamosAtividades.Include(s => s.Segmentos).Select(s => new {
                s.Id,
                s.Descricao,
                QtdeSegmentos = s.Segmentos.Count
            });

            return list;
        }        
        [HttpGet]
        [Route("Segmentos")]
        [AllowAnonymous]
        public IEnumerable<dynamic> Segmentos()
        {
            var context = new DaoContext();
            var list = context.Segmentos.Include(s => s.Secoes).ThenInclude(s => s.Especies).Select(s => new {
                s.Id,
                s.Descricao,
                RamoAtividadeId = s.RamoAtividade.Id,
                RamoAtividadeDescricao = s.RamoAtividade.Descricao,
                QtdeProdutos = s.Secoes.Count
            });

            return list;
        }

        [HttpGet]
        [Route("ListHierarquia")]
        [AllowAnonymous]
        public IEnumerable<dynamic> ListHierarquia(string nivel, string pais = "0")
        {
            List<int> listPai = new List<int>();

            if (pais == null)
                pais = "0";

            if (pais != "0")
                listPai.AddRange(pais.Split(',').Select(int.Parse).ToList());

            var context = new DaoContext();

            IEnumerable<dynamic> list = null;

            switch(nivel)
            {
                case "ram":
                    list = GetRamoAtividades(listPai);
                    break;
                case "seg":
                    list = GetSegmentos(listPai);
                    break;
                case "sec":
                    list = GetSecoes(listPai);
                    break;
                case "esp":
                    list = GetEspecies(listPai);
                    break;
                default:    
                    list = null;
                    break;
            }

            return list;
        }      

        [HttpGet]
        [Route("Secoes")]
        [AllowAnonymous]
        public IEnumerable<dynamic> Secoes()
        {
            var context = new DaoContext();
            var list = context.Secoes.Include(s => s.Especies).Select(s => new {
                s.Id,
                s.Descricao,
                SegmentoId = s.Segmento.Id,
                SegmentoDescricao = s.Segmento.Descricao,
                QtdeProdutos = s.Especies.Count
            });

            return list;
        }

        [HttpGet]
        [Route("Especies")]
        [AllowAnonymous]
        public IEnumerable<dynamic> Especies()
        {
            var context = new DaoContext();
            var list = context.Especies.Include(p => p.Produtos).Select(s => new {
                s.Id,
                s.Descricao,
                s.Codigo,
                SecaoId = s.Secao.Id,
                SecaoDescricao = s.Secao.Descricao,
                QtdeProdutos = s.Produtos.Count
            });

            return list;
        } 

        [HttpGet]
        [Route("ListNivel")]
        [AllowAnonymous]
        public IEnumerable<dynamic> ListNivel(string nivel, string filtroPai = "0")
        {
            var context = new DaoContext();
            var list = new List<dynamic>();

            if (filtroPai == null)
                filtroPai = "0";

            var filtro = filtroPai.Split(',').Select(int.Parse).ToList();

            switch (nivel)
            {
                case "ramoAtividades":
                    var ramoAtividades = RamoAtividades().Select(s => new {
                        s.Id,
                        s.Descricao,
                        PaiId = 0,
                        PaiDescricao = "Ramo Atividade",
                        QtdeProdutos = s.QtdeSegmentos});

                    list.AddRange(ramoAtividades);

                        break;
                case "segmentos":
                    var segmentos = context.Segmentos.Include(s => s.Secoes).ThenInclude(s => s.Especies).Where(f => filtro.Contains((filtroPai != "0") ? f.RamoAtividade.Id : 0)).Select(s => new {
                        s.Id,
                        s.Descricao,
                        PaiId = s.RamoAtividade.Id,
                        PaiDescricao = s.RamoAtividade.Descricao,
                        QtdeProdutos = s.Secoes.Count});

                    list.AddRange(segmentos);                
                    break;
                case "secoes":

                    var secoes = context.Secoes.Include(s => s.Especies).Where(f => filtro.Contains((filtroPai != "0") ? f.Segmento.Id : 0)).Select(s => new {
                        s.Id,
                        s.Descricao,
                        PaiId = s.Segmento.Id,
                        PaiDescricao = s.Segmento.Descricao,
                        QtdeProdutos = s.Especies.Count});
                
                    list.AddRange(secoes);                
                    break;                
                case "especies":
                    var especies = context.Especies.Include(p => p.Produtos).Where(f => filtro.Contains((filtroPai != "0") ? f.Secao.Id : 0)).Select(s => new {
                        s.Id,
                        s.Descricao,
                        PaiId = s.Secao.Id,
                        PaiDescricao = s.Secao.Descricao,
                        QtdeProdutos = s.Produtos.Count});

                    list.AddRange(especies);      
                    break;
                case "marcas":
                    var marcas = Marcas().Select(s => new {
                        s.Id,
                        s.Descricao,
                        PaiId = 0,
                        PaiDescricao = "",
                        QtdeProdutos = s.QtdeProdutos});

                    list.AddRange(marcas);      
                        break;            
              case "cores":
                    var cores = context.Cores.Select(s => new {
                        s.Id,
                        s.Descricao,
                        PaiId = 0,
                        PaiDescricao = "",
                        QtdeProdutos = 0});

                    list.AddRange(cores);      
                    break;              
              case "tamanhos":
                    var tamanhos = context.Tamanhos.Select(s => new {
                        s.Id,
                        s.Descricao,
                        PaiId = 0,
                        PaiDescricao = "",
                        QtdeProdutos = 0});

                    list.AddRange(tamanhos);      
                    break;                  
            }

            return list;
        } 

        [HttpGet]
        [Route("New")]
        [AllowAnonymous]
        public int New(string especie)
        {

            var context = new DaoContext();

            int codigoNew = 1;
            IQueryable<ProdutoCodigo> produtoCodigo = context.ProdutoCodigos.Where(p => p.IDEspecie ==  Int32.Parse(especie)).DefaultIfEmpty();
            
            if (produtoCodigo.ToList()[0] != null)
            {
                codigoNew += produtoCodigo.Max(m => m.Codigo);
            }

            Especie esp = context.Especies.Where(s => s.Id ==  Int32.Parse(especie)).Include(s=> s.Secao).FirstOrDefault();

            ProdutoCodigo pdc = new ProdutoCodigo();
            pdc.Especie = esp;
            pdc.Codigo = codigoNew;



            context.ProdutoCodigos.Add(pdc);
            context.SaveChanges();

            Produto prd = new Produto(pdc);
            prd.Descricao = "Produto teste";
            context.Produtos.Add(prd);
            context.SaveChanges();

            return prd.Id;
        }

        [HttpGet]
        [Route("GetId")]
        [AllowAnonymous]
        public dynamic GetId(int id)
        {
            var produto = context.Produtos.Where(p => p.Id == id).Include(e => e.Especie).ThenInclude(s => s.Secao).ThenInclude(s => s.Segmento).ThenInclude(r => r.RamoAtividade).Include(m => m.Marca).Include(u => u.UnidadeMedida).FirstOrDefault(); 
            var especie =  new { 
                        produto.Especie.Id, 
                        produto.Especie.Descricao,
                        Secao = new {
                            produto.Especie.Secao.Id,
                            produto.Especie.Secao.Descricao,
                        }};


            var result = new {
                produto = new {
                    produto.Id,
                    produto.DataCadastro,
                    Especie = especie,
                    produto.Codigo,
                    produto.Descricao,
                    produto.Ativo,
                    produto.Status,
                    produto.MateriaPrima,
                    produto.Referencia,
                    Marca = (produto.Marca == null) ? null : new {produto.Marca.Id, produto.Marca.Descricao },
                    UnidadeMedida = (produto.UnidadeMedida == null) ? null : new { produto.UnidadeMedida.Id, produto.UnidadeMedida.Unidade} 
                }
            };

            return result;
        }

        [HttpGet]
        [Route("List")]
        [AllowAnonymous]
        public IEnumerable<dynamic>  List()
        {
            var produtos = context.Produtos.Include(p => p.Especie).ThenInclude(s => s.Secao).Include(p => p.Marca).DefaultIfEmpty();

            var list = produtos.Select(p => new {
                p.Id,
                p.Descricao,
                p.CodigoInterno,
                p.Ativo,
                p.Status,
                MarcaId = (p.Marca == null) ? 0 : p.Marca.Id,
                MarcaDescricao = p.Marca.Descricao
            });

            return list;
        }


        #region Methods Privates

        private IEnumerable<RamoAtividade> GetRamoAtividades(List<int> pais)
        {
            return context.RamosAtividades.ToList();
        }

        private IEnumerable<Segmento> GetSegmentos(List<int> pais)
        {
            if (pais.Count == 0)
                return context.Segmentos.Include(i => i.RamoAtividade).ToList();
            else
                return context.Segmentos.Where(s => pais.Contains(s.RamoAtividade.Id)).Include(i => i.RamoAtividade).ToList();
        }

        private IEnumerable<Secao> GetSecoes(List<int> pais)
        {
            if (pais.Count == 0)
                return context.Secoes.Include(i => i.Segmento).ToList();
            else
                return context.Secoes.Where(s => pais.Contains(s.Segmento.Id)).Include(i => i.Segmento).ToList();
        }

        private IEnumerable<Especie> GetEspecies(List<int> pais)
        {
            if (pais.Count == 0)
                return context.Especies.Include(i => i.Secao).ToList();
            else
                return context.Especies.Where(s => pais.Contains(s.Secao.Id)).Include(i => i.Secao).ToList();
        }  

        #endregion

}