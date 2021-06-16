public class PromocaoVenda
{
    public int Id { get; set; }
    public int IDPromocaoRegra {get; set;}
    public PromocaoRegra PromocaoRegra {get; set;}
    public decimal ValorCompraMinima { get; set; }
    public decimal ValorCompraMaxima { get; set; }
    public short QtdeItemMinimo { get; set; }
    public short QtdeItemMaximo { get; set; }
    public short LimiteOcorrencia { get; set; }
}
