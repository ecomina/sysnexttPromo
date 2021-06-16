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

}