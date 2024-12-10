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

        Task<bool> AgregarJugador(string nombre, int edad, string posicion, int equipoId);

        Task<bool> ActualizarJugador(int jugadorId, JugadorDTO jugadorActualizado);

        Task<bool> EliminarJugador(int jugadorId);

    }
}