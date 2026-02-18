using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Models;

namespace Restaurant.ConexionDatos
{
    public interface IRestConexionDatos
    {
        Task<List<Plato>> ObtenerPlatos();
        Task AddPlato(Plato plato);
        Task UpdatePlato(Plato plato);
        Task DeletePlato(int id);
    }
}
