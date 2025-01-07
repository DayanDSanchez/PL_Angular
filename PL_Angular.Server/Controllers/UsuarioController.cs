using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace PL_Angular.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        [EnableCors("API")]
        [HttpGet]
        public IActionResult Get()
        {
            ML.Result result = BL.Usuario.GetAll();
            if (result.Correct)
            {
                ML.Usuario usuario = new ML.Usuario();

                usuario.Usuarios = result.Objects;
                return Ok(usuario.Usuarios);
            }
            else
            {
                return BadRequest();
            }
        }

        [EnableCors("API")]
        [HttpPost]
        public IActionResult Post([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Add(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [EnableCors("API")]
        [HttpPut("{IdUsuario}")]
        public IActionResult Put(int IdUsuario, [FromBody] ML.Usuario usuario)
        {
            usuario.IdUsuario = IdUsuario;
            ML.Result result = BL.Usuario.Update(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [EnableCors("API")]
        [HttpDelete("{IdUsuario}")]
        public IActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.Delete(IdUsuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [EnableCors("API")]
        [HttpGet("{IdUsuario}")]
        public IActionResult GetById(int IdUsuario)
        {
            ML.Result result = BL.Usuario.GetById(IdUsuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
