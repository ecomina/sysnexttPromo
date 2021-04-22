using System.Linq;
using System.Threading.Tasks;
using Authentic.DAL;
using Authentic.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentic.Controllers
{
    //[Route("api/[controller]")]
    public class HomeController : ControllerBase
    {

        public HomeController()
        {
            _context = new DaoContext();
        }

        private readonly DaoContext _context;
        
        [HttpGet]
        [Route("Ola")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public string Ola()
        {
            return "Hello Word";
        }

        [HttpGet]
        [Route("OlaUser")]
        public Usuario OlaUser()
        {
            return _context.Usuarios.First();

            //return "Hello Word";
        }
    }

}
