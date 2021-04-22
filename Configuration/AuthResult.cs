using System.Collections.Generic;

namespace Authentic.Configuration 
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public List<string> Erros {get; set;}
    }
}