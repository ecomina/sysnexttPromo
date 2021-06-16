using System;

public class PostLog
{
    #region Constructor
    public PostLog(string registro)
    {
        this.Registro = registro;
        this.Data = DateTime.Now;
    }

    #endregion

    public int Id { get; set; }
    public DateTime Data { get; set; }
    public string Registro { get; set; }
}

