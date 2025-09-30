namespace AppRpgEtec.Views.Armas;

public partial class ListagemViewArmas : ContentPage
{
    public ListagemViewArmas()
    {
        InitializeComponent();

        this.BindingContext = new ViewModels.Armas.ListagemArmaViewModel();
    }
}