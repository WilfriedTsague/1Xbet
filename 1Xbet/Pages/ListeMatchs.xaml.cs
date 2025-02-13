using _1Xbet.Models;
using _1Xbet.Services;

namespace _1Xbet.Pages;

public partial class ListeMatchs : ContentPage
{
	List<Match> matchs;
	public ListeMatchs()
	{
		InitializeComponent();
	}

	private async void AjouterMatch(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AjouterMatchPage());
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        matchs = Dbcontext._database.Table<Match>().OrderBy(x => x.Date).ToList();
        listeDesMatchs.ItemsSource = matchs;
    }
}