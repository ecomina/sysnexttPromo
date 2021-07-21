using System.Collections.Generic;

public class Secao 
{
    public short Id { get; set; }
    public Segmento Segmento { get; set; }
     public short IDSegmento { get; set; }
    public string Descricao { get; set; }
    public List<Especie> Especies { get; set; }
    public List<Produto> Produtos { get; set; }
    public List<ProdutoCodigo> ProdutoCodigos { get; set; }
}

