using Restaurant.Models;
using Restaurant.ConexionDatos;
using Restaurant.Pages;
using System.Diagnostics;


namespace Restaurant
{
    public partial class MainPage : ContentPage
    {
        private readonly IRestConexionDatos restConexionDatos;

        public MainPage(IRestConexionDatos restConexionDatos)
        {
            InitializeComponent();
            this.restConexionDatos = restConexionDatos;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var platos = await restConexionDatos.ObtenerPlatos();

            coleccionPlatosView.ItemsSource = platos;
        }

        // Evento Add
        private async void OnAddPlatoClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("[EVENTO] Agregar plato");
            var param = new Dictionary<string, object>
            {
                { nameof(Plato), new Plato() }
            };
            await Shell.Current.GoToAsync(nameof(PlatosPage), param);
        }
        // Evento clic sobre un plato
        private async void OnPlatoSelected(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("[EVENTO] Plato seleccionado");
            var param = new Dictionary<string, object>
            {
                { nameof(Plato), e.CurrentSelection.FirstOrDefault() as Plato }
            };
            await Shell.Current.GoToAsync(nameof(PlatosPage), param);
        }
    }
}