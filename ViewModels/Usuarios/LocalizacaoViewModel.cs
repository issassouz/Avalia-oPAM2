namespace AppRpgEtec.ViewModels.Usuarios;

using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;
public class LocalizacaoViewModel : ContentPage
{
    private Map meuMapa;
    public Map MeuMapa
    {
        get => meuMapa;
        set
        {
            if (value != null)
            {
                meuMapa = value;
                OnPropertyChanged();
            }
        }
    }

    public async void InicializarMapa()
    {
        try
        {
            Location location = new Location(-23.5200241d, -46.596498d);

            Pin pinEtec = new Pin()
            {
                Type = PinType.Place,
                Label = "Etec Horácio",
                Address = "Rua Alcântara, 113, Vila Guilherme",
                Location = location
            };

            Map map = new Map();
            MapSpan mapSpan = MapSpan
                .FromCenterAndRadius(location, Distance.FromKilometers(5));

            map.Pins.Add(pinEtec);
            map.MoveToRegion(mapSpan);

            MeuMapa = map;
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage
                .DisplayAlert("Erro", ex.Message, "OK");
        }
    }

}
