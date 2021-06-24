using System.Collections.Generic;
using System.Linq;
using Authentic.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var list = context.Segmentos.Include(s => s.Secoes).ThenInclude(s => s.Produtos).Select(s => new {
                s.Id,
                s.Descricao,
                RamoAtividadeId = s.RamoAtividade.Id,
                RamoAtividadeDescricao = s.RamoAtividade.Descricao,
                QtdeProdutos = s.Secoes.Count
            });

            return list;
        }      

        [HttpGet]
        [Route("Secoes")]
        [AllowAnonymous]
        public IEnumerable<dynamic> Secoes()
        {
            var context = new DaoContext();
            var list = context.Secoes.Include(s => s.Produtos).Select(s => new {
                s.Id,
                s.Descricao,
                SegmentoId = s.Segmento.Id,
                SegmentoDescricao = s.Segmento.Descricao,
                QtdeProdutos = s.Produtos.Count
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

            switch (nivel)
            {
                case "ramoAtividades":
                    var ramoAtividades = RamoAtividades().Select(s => new {
                        s.Id,
                        s.Descricao,
                        PaiId = 0,
                        PaiDescricao = "",
                        QtdeProdutos = s.QtdeSegmentos});

                    list.AddRange(ramoAtividades);

                        break;
                case "segmentos":
                    var segmentos = context.Segmentos.Include(s => s.Secoes).ThenInclude(s => s.Produtos).Select(s => new {
                        s.Id,
                        s.Descricao,
                        PaiId = s.RamoAtividade.Id,
                        PaiDescricao = s.RamoAtividade.Descricao,
                        QtdeProdutos = s.Secoes.Count});

                    list.AddRange(segmentos);                
                    break;
                case "secoes":
                    var filtro = filtroPai.Split(',').Select(int.Parse).ToList();

                    var secoes = context.Secoes.Include(s => s.Produtos).Where(f => filtro.Contains((filtroPai != "0") ? f.Segmento.Id : 0)).Select(s => new {
                        s.Id,
                        s.Descricao,
                        PaiId = s.Segmento.Id,
                        PaiDescricao = s.Segmento.Descricao,
                        QtdeProdutos = s.Especies.Count});
                
                    list.AddRange(secoes);                
                    break;                
                case "especies":
                    var especies = context.Especies.Include(p => p.Produtos).Select(s => new {
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

}