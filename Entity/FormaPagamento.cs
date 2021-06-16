using System.Collections.Generic;

public class FormaPagamento
{
    public short Id { get; set; }
    public string Descricao { get; set; }
    public List<TipoPagamento> Tipos { get; set; }
}
