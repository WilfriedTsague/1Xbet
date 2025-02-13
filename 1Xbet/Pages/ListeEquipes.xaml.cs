using _1Xbet.DTO;
using _1Xbet.Models;
using _1Xbet.Services;

namespace _1Xbet.Pages;

public partial class ListeEquipes : ContentPage
{

    List<EquipeDTO> equipes;
    List<Match> match;

    public ListeEquipes()
    {
        InitializeComponent();

    }

   

    private async void OnAjouterEquipeClicked(object sender, EventArgs e)
    {
        if (match.Count > 0)
        {
            await DisplayAlert("Message", "La saison a commencée, aucune équipe ne peut être ajoutée.", "Ok");
            return;
        }

        await Navigation.PushAsync(new AjouterEquipePage());
    }

    private async void SelectEquipeTarget(object sender, TappedEventArgs e)
    {
        var label = sender as Label;
        var item = label?.BindingContext as EquipeDTO;
        await Navigation.PushAsync(new EquipeDetails(item));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        match = Dbcontext._database.Table<Match>().OrderBy(x => x.Date).ToList();
        equipes = Dbcontext._database.Table<Equipe>().Select(e => new EquipeDTO
        {
            Id = e.Id,
            NomEquipe = e.NomEquipe,
            Description = e.Description,
            NbreMatchJoue = ObtenirNombreDeMatchsJoues(e),
            NbreButsMarque = ObtenirTotalButsMarques(e),
            NbreButsEncaisse = ObtenirTotalButsRecus(e),
            NbrePoints = CalculerPoints(e)
        }).OrderByDescending(equipe => equipe.NbrePoints).ToList();

        equipesCollectionView.ItemsSource = equipes;
    }

    private int ObtenirNombreDeMatchsJoues(Equipe equipe)
    {
        return Dbcontext._database.Table<Match>().OrderBy(x => x.Date).ToList()
          .FindAll(x => x.EquipeDom == equipe.NomEquipe || x.EquipeExt == equipe.NomEquipe).Count;
    }

    private int ObtenirTotalButsMarques(Equipe equipe)
    {
        int total = 0;
        var matchsJoues = Dbcontext._database.Table<Match>().OrderBy(x => x.Date).ToList()
            .FindAll(x => x.EquipeDom == equipe.NomEquipe || x.EquipeExt == equipe.NomEquipe);

        foreach (var match in matchsJoues)
        {
            total += (match.EquipeDom == equipe.NomEquipe) ? match.ButEquipeDom : match.ButEquipeExt;
        }

        return total;
    }

    private int ObtenirTotalButsRecus(Equipe equipe)
    {
        int total = 0;
        var matchsJoues = Dbcontext._database.Table<Match>().OrderBy(x => x.Date).ToList()
            .FindAll(x => x.EquipeDom == equipe.NomEquipe || x.EquipeExt == equipe.NomEquipe);

        foreach (var match in matchsJoues)
        {
            total += (match.EquipeDom == equipe.NomEquipe) ? match.ButEquipeExt : match.ButEquipeDom;
        }

        return total;
    }

    private int CalculerPoints(Equipe equipe)
    {
        int points = 0;
        var matchsJoues = Dbcontext._database.Table<Match>().OrderBy(x => x.Date).ToList()
            .FindAll(x => x.EquipeDom == equipe.NomEquipe || x.EquipeExt == equipe.NomEquipe);

        foreach (var match in matchsJoues)
        {
            if (match.EquipeDom == equipe.NomEquipe)
            {
                points += (match.ButEquipeDom > match.ButEquipeExt) ? 3 : (match.ButEquipeExt == match.ButEquipeDom ? 1 :  0);
            }
            else
            {
                points += (match.ButEquipeExt > match.ButEquipeDom) ? 3 : (match.ButEquipeExt == match.ButEquipeDom ? 1 : 0);
            }
        }

        return points;
    }



}
