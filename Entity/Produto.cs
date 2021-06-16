public class Produto
{
    public int Id { get; set; }
    public Secao Secao { get; set; }
    public Especie Especie { get; set; }
    public short IDSecao { get; set; }
    public short IDEspecie { get; set; }
    public Marca Marca { get; set; }
    public int Codigo { get; set; }
    public string Descricao { get; set; }
    public string CodigoInterno {
        get {
                return "0";
                // Especie.Secao.Id.ToString().PadLeft(3, '0')+
                // Especie.Id.ToString().PadLeft(2, '0')+
                // Codigo.ToString().PadLeft(4, '0');    
        }
    }
}
