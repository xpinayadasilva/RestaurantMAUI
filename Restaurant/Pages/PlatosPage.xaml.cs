using Restaurant.Models;
using Restaurant.ConexionDatos;

namespace Restaurant.Pages;

[QueryProperty(nameof(Plato), "Plato")]
public partial class PlatosPage : ContentPage
{
    private readonly IRestConexionDatos conexionDatos;
    private Plato _plato;
    private bool _esNuevo;

    public Plato Plato
    {
        get => _plato;
        set
        {
            _esNuevo = EsNuevo(value);
            _plato = value;
            OnPropertyChanged();
        }
    }

    public PlatosPage(IRestConexionDatos conexionDatos)
    {
        InitializeComponent();
        this.conexionDatos = conexionDatos;
        BindingContext = this;

        Plato ??= new Plato();
    }

    bool EsNuevo(Plato plato) => plato.id == 0;

    async void OnCancelarClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");

    async void OnGuardarClicked(object sender, EventArgs e)
    {
        if (_esNuevo)
            await conexionDatos.AddPlato(Plato);
        else
            await conexionDatos.UpdatePlato(Plato);

        await Shell.Current.GoToAsync("..");
    }

    async void OnEliminarClicked(object sender, EventArgs e)
    {
        if (!_esNuevo)
            await conexionDatos.DeletePlato(Plato.id);

        await Shell.Current.GoToAsync("..");
    }
}