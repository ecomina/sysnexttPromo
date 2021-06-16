using System.Collections.Generic;

public class Especie 
{
    public short Id { get; set; }
    public Secao Secao { get; set; }
    public short IDSecao { get; set; }
    public string Descricao { get; set; }
    public List<Produto> Produtos { get; set; }
}
