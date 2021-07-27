using System;

public class Produto 
{
    #region Contructor

    public Produto() {}
    public Produto(ProdutoCodigo pdc)
    {
        this.Id = pdc.Id;
        this.DataCadastro = DateTime.Now;
        this.Especie = pdc.Especie;
        this.IDEspecie = pdc.Especie.Id;
        this.Codigo = pdc.Codigo;
        this.MateriaPrima = false;
        this.Ativo = false;
        this.Status = "IN";
    }

    #endregion

    #region Propeties

    public int Id { get; set; }
    public DateTime DataCadastro { get; set; }
    public int IDEspecie { get; set; }    
    public Especie Especie { get; set; }    
    public Secao Secao {
        get {
            if (Especie != null)
            return Especie.Secao;
            else
            return null;
        }
    }
    public int Codigo { get; set; }
    public int IDMarca { get; set; }
    public Marca Marca { get; set; }
    public string Descricao { get; set; }
    public string Referencia { get; set; }
    public bool MateriaPrima { get; set; }
    public bool Ativo { get; set; }
    public string Status { get; set; }
    // public string Tipo { get; set; }
    public UnidadeMedida UnidadeMedida { get; set; }
    public string CodigoInterno {
        get {
                return 
                    Especie.Secao.Id.ToString().PadLeft(3, '0')+
                    Especie.Codigo.ToString().PadLeft(2, '0')+
                    Codigo.ToString().PadLeft(4, '0');    
        }
    }

    #endregion
}
