using System.Collections.Generic;

public class PromocaoProduto
{
    public PromocaoProduto()
    {
        Marcas = new List<PromocaoProdutoMarca>();
    }
    public int Id {get; set;}
    public int IDPromocaoRegra {get; set;}
    public PromocaoRegra PromocaoRegra {get; set;}
    public byte Tipo { get; set; }
    public string Operador { get; set; }
    public short Qtde { get; set; }
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool MesmoItem { get; set; }
    public bool MesmoPreco { get; set; }
    public List<PromocaoProdutoMarca> Marcas {get; set;}

}
