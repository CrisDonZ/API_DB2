﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Business.Interfaces
{
    public interface IPartidosServices
    {
        List<JugadorDTO> ListarJugadores();

        List<JugadorDTO> BuscarJugadorPorNombre(string nombre);

        Task<bool> AgregarJugador(JugadorDTO nuevoJugador);

        Task<bool> ActualizarJugador(int jugadorId, JugadorDTO jugadorActualizado);

        Task<bool> EliminarJugador(int jugadorId);

        Task<bool> ValidarJugadorIdExistente(int jugadorId);
        Task<bool> ValidarEquipoExistente(int equipoId);
        Task<bool> ValidarNombre(string nombre);

        Task<bool> ValidarPosicion(string posicion);

    }
}
