using System;
using System.Collections.Generic;


public class Promocao
{
    public Promocao()
    {
        Filiais = new List<PromocaoFilial>();
        Regras = new List<PromocaoRegra>();
    }

    public int Id { get; set; }
    public string Descricao { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataTermino { get; set; }
    public string Status { get; set; }
    public string ObservacaoOperador { get; set; }
    public List<PromocaoRegra> Regras {get; set;}
    public List<PromocaoFilial> Filiais {get; set;}
    public bool Dom { get; set; }
    public bool Seg { get; set; }
    public bool Ter { get; set; }
    public bool Qua { get; set; }
    public bool Qui { get; set; }
    public bool Sex { get; set; }
    public bool Sab { get; set; }

}
