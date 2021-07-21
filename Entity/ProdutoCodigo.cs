using System.Collections.Generic;

public class ProdutoCodigo
{
    #region Properties

    public int Id { get; set; }
    public Secao Secao { get; set; }
    public Especie Especie { get; set; }  
    public short IDSecao { get; set; }
    public short IDEspecie { get; set; }
    public int Codigo { get; set; }
    public List<Produto> Produtos { get; set; }

    #endregion    
}