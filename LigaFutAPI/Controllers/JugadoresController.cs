using Business.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LigaFutAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugadoresController : ControllerBase
    {
        // GET: api/<JugadoresController>
        private readonly IPartidosServices _partidosServices;

        public JugadoresController(IPartidosServices partidosServices)
        {
            _partidosServices = partidosServices;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var listaJugadores = _partidosServices.ListarJugadores();

                return Ok(listaJugadores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Buscar/{nombre}")]
        public async Task<ActionResult> BuscarJugadorPorNombre(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    return BadRequest("El nombre no puede estar vacío");
                }

                // Validar que el nombre no contenga números
                if (nombre.Any(char.IsDigit))
                {
                    return BadRequest("El nombre no puede contener números");
                }

                // Realizar la búsqueda
                var resultado = _partidosServices.BuscarJugadorPorNombre(nombre);

                // Verificar si se encontró algún resultado
                if (resultado == null || !resultado.Any())
                {
                    return NotFound("No se encontró ningún jugador con ese nombre");
                }
                return Ok(resultado);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("agregar")]
        public async Task<ActionResult> AgregarJugador(string nombre, int edad, string posicion, int equipoId)
        {
            try
            {
                // Llama al método del servicio para agregar el jugador
                bool resultado = await _partidosServices.AgregarJugador(nombre, edad, posicion, equipoId);

                // Evalúa el resultado y devuelve la respuesta adecuada
                if (resultado)
                {
                    return Ok("Jugador agregado exitosamente.");
                }
                else
                {
                    return StatusCode(500, "No se pudo agregar el jugador.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }




        [HttpPut("Actualizar/{id}")]
        public async Task<ActionResult> ActualizarJugador(int id, [FromBody] JugadorDTO jugadorActualizado)
        {
            try
            {
                // Validar entrada
                if (jugadorActualizado == null)
                {
                    return BadRequest("La información del jugador no puede estar vacía.");
                }
                if (id != jugadorActualizado.JugadorId)
                {
                    return BadRequest("No se permite cambiar el ID del jugador");
                }

                if (string.IsNullOrWhiteSpace(jugadorActualizado.Nombre))
                {
                    return BadRequest("El nombre del jugador es obligatorio.");
                }

                if (jugadorActualizado.Nombre.Any(char.IsDigit))
                {
                    return BadRequest("El nombre del jugador no puede contener números.");
                }

                if (jugadorActualizado.Edad <= 0)
                {
                    return BadRequest("La edad debe ser mayor a 0.");
                }

                if (string.IsNullOrWhiteSpace(jugadorActualizado.Posicion))
                {
                    return BadRequest("La posición del jugador es obligatoria.");
                }

                if (jugadorActualizado.EquipoId <= 0)
                {
                    return BadRequest("El ID del equipo debe ser válido.");
                }

                // Llamar al servicio para actualizar al jugador
                var resultado = await _partidosServices.ActualizarJugador(id, jugadorActualizado);

                return Ok("Jugador actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("No se encontró el jugador"))
                {
                    return NotFound(new {message = ex.Message});
                }
                return BadRequest(new {message = ex.Message});
            }
        }


        // DELETE api/<JugadoresController>/5
        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult>EliminarJugador(int id)
        {
            try
            {
                if(id < 0)
                {
                    return BadRequest("El Id del jugador debe ser positivo");
                }

                var resultado = await _partidosServices.EliminarJugador(id);

                // Verificar si se encontró algún resultado
                if (!resultado)
                {
                    return NotFound("No se encontró ningún jugador con ese id");
                }
                return Ok("Jugador eliminado");
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
