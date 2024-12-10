using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Core.EFModels;
using Core.Models;
using Infraestructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Implementations
{
    public class PartidosServices : IPartidosServices
    {
        private readonly LigaFutContext? _ligaFutContext;

        public PartidosServices(LigaFutContext ligaFutContext)
        {
            _ligaFutContext = ligaFutContext;
        }

        public List<JugadorDTO> ListarJugadores()
        {
            List<JugadorDTO> listaretornar = new List<JugadorDTO>();
            try
            {
                var lista = _ligaFutContext.Jugadores.ToList();

                if (lista != null)
                {
                    foreach(var jugador in lista)
                    {
                        JugadorDTO jugadorDTO = new JugadorDTO()
                        {
                            JugadorId = jugador.JugadorId,
                            Nombre = jugador.Nombre,
                            Edad = jugador.Edad,
                            Posicion = jugador.Posicion,
                            EquipoId = jugador.EquipoId
                        };
                        listaretornar.Add(jugadorDTO);
                    }
                }
                return listaretornar;
            }
            catch (Exception ex)
            {

            }
            return listaretornar;

        }

        public List<JugadorDTO> BuscarJugadorPorNombre(string nombre)
        {
            List<JugadorDTO> resultado = new List<JugadorDTO>();
            try
            {
                var jugadores = _ligaFutContext.Jugadores.Where(c => c.Nombre.Contains(nombre)).ToList();

                foreach(var jugador in jugadores)
                {
                    resultado.Add(new JugadorDTO
                    {
                        JugadorId = jugador.JugadorId,
                        Nombre = jugador.Nombre,
                        Edad = jugador.Edad,
                        Posicion = jugador.Posicion,
                        EquipoId = jugador.EquipoId
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }

            return resultado;
        }

        public async Task<bool> AgregarJugador(JugadorDTO nuevoJugador)
        {
            try
            {
                var jugador = new Jugadore
                {
                    JugadorId = nuevoJugador.JugadorId,
                    Nombre = nuevoJugador.Nombre,
                    Edad = nuevoJugador.Edad,
                    Posicion = nuevoJugador.Posicion,
                    EquipoId = nuevoJugador.EquipoId
                };

                await _ligaFutContext.Jugadores.AddAsync(jugador);
                await _ligaFutContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Ocurrió un error");
                return false;
            }
        }


        public async Task<bool> ActualizarJugador(int jugadorId, JugadorDTO jugadorActualizado)
        {
            try
            {
                var jugador = await _ligaFutContext.Jugadores.FindAsync(jugadorId);

                if (jugador == null)
                {
                    throw new Exception($"No se encontró un jugador con el Id {jugadorId}.");
                }
                if(jugadorId != jugadorActualizado.JugadorId)
                {
                    throw new Exception("No se permite cambiar el Id del jugador");
                }
                var equipoExiste = await _ligaFutContext.Equipos.AnyAsync(e => e.EquipoId== jugadorActualizado.EquipoId);
                if (!equipoExiste)
                {
                    throw new Exception("El equipo especificado no existe.");
                }

                jugador.Nombre = jugadorActualizado.Nombre ?? jugador.Nombre;
                jugador.Edad = jugadorActualizado.Edad > 0 ? jugadorActualizado.Edad : jugador.Edad;
                jugador.EquipoId = jugadorActualizado.EquipoId > 0 ? jugadorActualizado.EquipoId : jugador.EquipoId;
                jugador.Posicion = jugadorActualizado.Posicion ?? jugador.Posicion;

                await _ligaFutContext.SaveChangesAsync();

                return true;

            }
            catch(Exception)
            {
                throw;
            }
        }


        public async Task<bool>EliminarJugador(int jugadorId)
        {
            try
            {
                var jugador = await _ligaFutContext.Jugadores.FindAsync(jugadorId);

                if(jugador == null)
                {
                    return false;
                }

                _ligaFutContext.Jugadores.Remove(jugador);

                await _ligaFutContext.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }
        }

    }
}
