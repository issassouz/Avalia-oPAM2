using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppRpgEtec.Models;
using AppRpgEtec.Services.Armas;

namespace AppRpgEtec.ViewModels.Armas
{
    public class ListagemArmaViewModel : BaseViewModel
    {
        private ArmaService aService;
        public ObservableCollection<Arma> Armas { get; set; }

        public ListagemArmaViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            aService = new ArmaService(token);
            Armas = new ObservableCollection<Arma>();

            _ = ObterArmas();

            NovaArmaCommand = new Command(async () => { await ExibirCadastroArma(); });
            RemoverArmaCommand = new Command<Arma>(async (Arma a) => { await RemoverArma(a); });
        }

        public ICommand NovaArmaCommand { get; }
        public ICommand RemoverArmaCommand { get; set; }

        public async Task ObterArmas()
        {
            try
            {
                Armas = await aService.GetArmasAsync();
                OnPropertyChanged(nameof(Armas));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhe: " + ex.InnerException, "Ok");
            }
        }

        public async Task ExibirCadastroArma()
        {
            try
            {
                await Shell.Current.GoToAsync("cadArmaView");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task RemoverArma(Arma a)
        {
            try
            {
                if (await Application.Current.MainPage.DisplayAlert("Confirma��o", $"Confirma a remo��o de {a.Nome}?", "Sim", "N�o"))
                {
                    await aService.DeleteArmaAsync(a.Id);

                    await Application.Current.MainPage.DisplayAlert("Mensagem", "Arma removida com sucesso!", "OK");

                    _ = ObterArmas();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "OK");
            }
        }
    }
}
