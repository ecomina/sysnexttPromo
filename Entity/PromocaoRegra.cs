using System.Collections.Generic;


public enum TipoRegra : byte
{
    Venda = 1,
    Pagamento = 2,
    Produto = 3,
    Cliente = 4
}
public class PromocaoRegra
{
    public PromocaoRegra()
    {
        Produtos = new List<PromocaoProduto>();
        Vendas = new List<PromocaoVenda>();
        Pagamentos = new List<PromocaoPagamento>();
        Clientes = new List<PromocaoCliente>();
    }
    public int Id { get; set; }
    public TipoRegra Tipo { get; set; }
    public bool Revogar { get; set; }
    public Promocao Promocao { get; set; }
    public List<PromocaoProduto> Produtos { get; set; }
    public List<PromocaoVenda> Vendas { get; set; }
    public List<PromocaoPagamento> Pagamentos { get; set; }
    public List<PromocaoCliente> Clientes { get; set; }
}