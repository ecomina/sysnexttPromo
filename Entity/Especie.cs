using System.Collections.Generic;

public class Especie 
{
    public int Id { get; set; }
    public Secao Secao { get; set; }
    public short IDSecao { get; set; }
    public short Codigo { get; set; }
    public string Descricao { get; set; }
    public List<Produto> Produtos { get; set; }
    public List<ProdutoCodigo> ProdutoCodigos { get; set; }
}
