
public class PromocaoPagamento
{
    public int Id { get; set; }
    public PromocaoRegra PromocaoRegra { get; set; }
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public string Parcela { get; set; }
    public short IDFormaPagamento { get; set; }
    // public FormaPagamento FormaPagamento { get; set; }
    // public TipoPagamento TipoPagamento { get; set; }
}
