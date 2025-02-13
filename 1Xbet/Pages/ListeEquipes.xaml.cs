using _1Xbet.Services;

namespace _1Xbet.Pages;

public partial class ListeEquipes : ContentPage
{
    private readonly Dbcontext _dbContext;

    public ListeEquipes()
    {
        InitializeComponent();

    }

   

    private void OnAjouterEquipeClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AjouterEquipePage());
    }
}
