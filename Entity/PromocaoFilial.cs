using System.ComponentModel.DataAnnotations.Schema;


public class PromocaoFilial
{
    public int Id { get; set; }
    public short IDFilial { get; set; }
    public Filial Filial { get; set; }
    public Promocao Promocao { get; set; }

}
