public class TipoPagamento
{
    public short Id { get; set; }
    public string Descricao { get; set; }
    public byte QtdeParcelas { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
}
