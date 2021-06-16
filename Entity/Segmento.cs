using System.Collections.Generic;

public class Segmento 
{
    public short Id { get; set; }
    public short IDRamoAtividade { get; set; }
    public string Descricao { get; set; }
    public RamoAtividade RamoAtividade { get; set; }
    public List<Secao> Secoes { get; set; }

}
