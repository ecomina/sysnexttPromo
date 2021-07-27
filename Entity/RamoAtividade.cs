using System.Collections.Generic;

public class RamoAtividade 
{
    public short Id { get; set; }
    public string Descricao { get; set; }
    // public string MateriaPrima { get; set; }
    public List<Segmento> Segmentos { get; set; }
}

