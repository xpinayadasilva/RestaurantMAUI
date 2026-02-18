using System.Net;
using System.Net.Http.Json;
using Restaurant.Models;


namespace Restaurant.ConexionDatos
{
    public class RestConexionDatos : IRestConexionDatos
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/posts";
        public RestConexionDatos(HttpClient http)
        {
            _http = http;                       
        }
        // GET
        public async Task<List<Plato>> ObtenerPlatos()
        {
            try
            {
                var response = await _http.GetAsync(BaseUrl);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error al obtener platos. Código: {response.StatusCode}");
                }

                var posts = await response.Content.ReadFromJsonAsync<List<PostDto>>()
                            ?? new List<PostDto>();

                var random = new Random();  //para dar precio aleatorio a cada plato

                return posts.Select(p => new Plato
                {
                    id = p.id,
                    nombre = p.title,
                    ingredientes = p.body,
                    precio = random.Next(50, 151) // ⬅️ entre 50 y 150
                }).ToList();
            }
            catch (HttpRequestException ex)
            {
                // Error de red / HTTP
                throw new Exception("No se pudo conectar al servidor.", ex);
            }
            catch (TaskCanceledException ex)
            {
                // Timeout
                throw new Exception("La solicitud tardó demasiado tiempo.", ex);
            }
            catch (Exception ex)
            {
                // Error inesperado
                throw new Exception("Ocurrió un error inesperado al obtener los platos.", ex);
            }
        }

        // ==========================
        // POST
        // ==========================
        public async Task AddPlato(Plato platoSeleccionado)
        {
            try
            {
                var post = new PostDto
                {
                    title = platoSeleccionado.nombre,
                    body = platoSeleccionado.ingredientes,
                    userId = 1
                };

                var response = await _http.PostAsJsonAsync(BaseUrl, post);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Error al agregar plato. Código: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo agregar el plato.", ex);
            }
        }

        // ==========================
        // PUT
        // ==========================
        public async Task UpdatePlato(Plato platoSeleccionado)
        {
            try
            {
                var post = new PostDto
                {
                    id = platoSeleccionado.id,
                    title = platoSeleccionado.nombre,
                    body = platoSeleccionado.ingredientes,
                    userId = 1
                };

                var response = await _http.PutAsJsonAsync($"{BaseUrl}/{platoSeleccionado.id}", post);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Error al actualizar plato. Código: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo actualizar el plato.", ex);
            }
        }

        // ==========================
        // DELETE
        // ==========================
        public async Task DeletePlato(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"{BaseUrl}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Error al eliminar plato. Código: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo eliminar el plato.", ex);
            }
        }

        // ==========================
        // DTO interno
        // ==========================
        private class PostDto
        {
            public int userId { get; set; }
            public int id { get; set; }
            public string title { get; set; } = "";
            public string body { get; set; } = "";
        }
    }
}
