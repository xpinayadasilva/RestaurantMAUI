using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ConexionDatos
{
    public interface IRestConexionDatos
    {
        Task<List<Models.Plato>> ObtenerPlatos();
        Task AddPlato(Models.Plato Plato);
        Task UpdatePlato(Models.Plato Plato);
        Task DeletePlato(int id);
    }
}
