using Restaurant.Models;
using Restaurant.ConexionDatos;
using System.Collections.ObjectModel;

namespace Restaurant.Pages;

public partial class ListaPlatosPage : ContentPage
{
    private readonly IRestConexionDatos conexionDatos;

    private List<Plato> _todosLosPlatos = new();
    private ObservableCollection<Plato> _platosPagina = new();

    private const int TamanoPagina = 4;
    private int _paginaActual = 0;

    public ListaPlatosPage(IRestConexionDatos conexionDatos)
    {
        InitializeComponent();
        this.conexionDatos = conexionDatos;
        PlatosCollection.ItemsSource = _platosPagina;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarPlatos();
    }

    async Task CargarPlatos()
    {

        var platos = await conexionDatos.ObtenerPlatos();

        _todosLosPlatos = platos
            .OrderBy(p => Guid.NewGuid())
            .ToList();

        _paginaActual = 0;
        CargarPagina();
    }

    void CargarPagina()
    {
        _platosPagina.Clear();

        var platosPagina = _todosLosPlatos
            .Skip(_paginaActual * TamanoPagina)
            .Take(TamanoPagina);

        foreach (var plato in platosPagina)
            _platosPagina.Add(plato);

        PaginaLabel.Text = $"Página {_paginaActual + 1} de {TotalPaginas()}";
    }

    int TotalPaginas()
        => (int)Math.Ceiling((double)_todosLosPlatos.Count / TamanoPagina);

    void OnAnteriorClicked(object sender, EventArgs e)
    {
        if (_paginaActual > 0)
        {
            _paginaActual--;
            CargarPagina();
        }
    }

    void OnSiguienteClicked(object sender, EventArgs e)
    {
        if (_paginaActual + 1 < TotalPaginas())
        {
            _paginaActual++;
            CargarPagina();
        }
    }

    async void OnAgregarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PlatosPage),
            new Dictionary<string, object>
            {
                { "Plato", new Plato() }
            });
    }

    async void OnPlatoSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Plato plato)
            return;

        await Shell.Current.GoToAsync(nameof(PlatosPage),
            new Dictionary<string, object>
            {
                { "Plato", plato }
            });

        PlatosCollection.SelectedItem = null;
    }
}
