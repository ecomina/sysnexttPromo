using System.Collections.Generic;

public class StatusPromocao 
{
    public short Id { get; set; }
    public string Descricao { get; set; }
    public List<Promocao> Promocoes {get; set;}
}

