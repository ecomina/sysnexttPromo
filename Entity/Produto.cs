public class Produto
{
    #region Contructor

    public Produto() {}
    public Produto(ProdutoCodigo pdc)
    {
        this.Id = pdc.Id;
        this.Secao = pdc.Secao;
        this.IDSecao = pdc.Secao.Id;
        this.Especie = pdc.Especie;
        this.IDEspecie = pdc.Especie.Id;
        this.Codigo = pdc.Codigo;
    }

    #endregion

    #region Propeties

    public int Id { get; set; }
    public Secao Secao { get; set; }
    public Especie Especie { get; set; }    
    public Marca Marca { get; set; }
    public short IDSecao { get; set; }
    public short IDEspecie { get; set; }
    public int Codigo { get; set; }
    public string Descricao { get; set; }
    public string Referencia { get; set; }
    public bool Ativo { get; set; }
    public string Status { get; set; }
    public string Tipo { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; }
    public string CodigoInterno {
        get {
                return "0";
                // Especie.Secao.Id.ToString().PadLeft(3, '0')+
                // Especie.Id.ToString().PadLeft(2, '0')+
                // Codigo.ToString().PadLeft(4, '0');    
        }
    }

    #endregion
}
