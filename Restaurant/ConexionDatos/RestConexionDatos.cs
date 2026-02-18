using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Restaurant.Models;

namespace Restaurant.ConexionDatos
{
    public class RestConexionDatos : IRestConexionDatos
    {
        public readonly HttpClient httpClient;
        private readonly string dominio;
        private readonly string url;
        private readonly JsonSerializerOptions jsonSerializerOptions;
        public RestConexionDatos(HttpClient httpClient)
        {
            //httpClient = new HttpClient();
            this.httpClient = httpClient;
            //dominio = DeviceInfo.Platform == DevicePlatform.Android? "http://10.0.2.2:5245" : "http://localhost:5245";
            dominio = DeviceInfo.Platform == DevicePlatform.Android ? "https://jsonplaceholder.typicode.com/posts" : "http://localhost:5245";
            //url = $"{dominio}/api/v1";
            url = $"{dominio}";
            jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        public async Task AddPlato(Plato Plato)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("[RED] No hay acceso a internet");
                return;
            }
            try
            {
                string platoJson = JsonSerializer.Serialize<Plato>(Plato, jsonSerializerOptions);
                StringContent contenido = new StringContent(platoJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync($"{url}/plato", contenido);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("[HTTP] Plato agregado correctamente");
                }
                else
                {
                    Debug.WriteLine($"[HTTP] Error en la respuesta: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[HTTP] Excepción al registrar plato: {ex.Message}");
            }
        }

        public async Task DeletePlato(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("[RED] No hay acceso a internet");
                return;
            }
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"{url}/plato/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("[HTTP] Plato eliminado correctamente");
                }
                else
                {
                    Debug.WriteLine($"[HTTP] Error en la respuesta: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[HTTP] Excepción al eliminar plato {id}: {ex.Message}");
            }
        }

        public async Task<List<Plato>> ObtenerPlatos()
        {
            List<Plato> platos = new List<Plato>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("[RED] No hay acceso a internet");
                return platos;
            }
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"{url}/plato");
                if (response.IsSuccessStatusCode)
                {
                    string contenido = await response.Content.ReadAsStringAsync();
                    platos = JsonSerializer.Deserialize<List<Plato>>(contenido, jsonSerializerOptions) ?? new List<Plato>();
                }
                else
                {
                    Debug.WriteLine($"[HTTP] Error en la respuesta: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[HTTP] Excepción al obtener platos: {ex.Message}");
            }
            return platos;
        }

        public async Task UpdatePlato(Plato Plato)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("[RED] No hay acceso a internet");
                return;
            }
            try
            {
                string platoJson = JsonSerializer.Serialize<Plato>(Plato, jsonSerializerOptions);
                StringContent contenido = new StringContent(platoJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PutAsync($"{url}/plato/{Plato.id}", contenido);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("[HTTP] Plato modificado correctamente");
                }
                else
                {
                    Debug.WriteLine($"[HTTP] Error en la respuesta: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[HTTP] Excepción al modificar plato: {ex.Message}");
            }
        }
    }
}
