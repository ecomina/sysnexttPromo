using System.Collections.Generic;

public class MTipoPgtoParcelas
{
    public short TipoPagamentoID { get; set; }
    public string TipoPagamentoDescricao { get; set; }
}
public class MParcelas
{
    public byte Parcela { get; set; }

    public bool Check {get; set; }

    public List<MTipoPgtoParcelas> Tipos { get; set;}
}
public class FormaPagtoParcelas
{
    public short  FormaPagamentoID { get; set; }
    public string FormaPagamentoDescricao { get; set; }
    public bool Check {get; set; }
    public List<MParcelas> Parcelas { get; set; }
}
