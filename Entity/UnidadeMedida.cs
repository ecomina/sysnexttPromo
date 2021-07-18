using System.Collections.Generic;

public class UnidadeMedida
{
    public int Id { get; set; }
    public string Unidade { get; set; }
    public string Medida { get; set; }
    public string Sigla { get; set; }
    public byte CasaDecimal { get; set; }
    public bool Ativo { get; set; }
    public List<Produto> Produtos { get; set; }
}
