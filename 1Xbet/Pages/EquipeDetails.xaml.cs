using _1Xbet.DTO;
using _1Xbet.Models;
using _1Xbet.Services;

namespace _1Xbet.Pages;

public partial class EquipeDetails : ContentPage
{
	List<Match> match;
	private int _IdEquipe;
	public EquipeDetails(EquipeDTO equipeDto)
	{
		InitializeComponent();
		_IdEquipe = equipeDto.Id;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var equipe = Dbcontext._database.Find<Equipe>(_IdEquipe);
        match = Dbcontext._database.Table<Match>().OrderBy(x => x.Date).ToList();
        Title = equipe.NomEquipe;
        description.Text = equipe.Description;
        var listeMatchesEquipe = match.FindAll(x => x.EquipeDom == equipe.NomEquipe || x.EquipeExt == equipe.NomEquipe);
        listeMatches.ItemsSource = listeMatchesEquipe;
    }
    private async void modifyClicked(object sender, EventArgs e)
    {
        if (match.Count > 0)
        {
            await DisplayAlert("Message", "La saison a commencée, l'équipe ne peut pas être modifiée.", "Ok");
            return;
        }
        var equipe = Dbcontext._database.Find<Equipe>(_IdEquipe);
        await Navigation.PushAsync(new AjouterEquipePage(equipe));
    }
}