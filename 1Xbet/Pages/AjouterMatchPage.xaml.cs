using _1Xbet.Models;
using _1Xbet.Services;

namespace _1Xbet.Pages;

public partial class AjouterMatchPage : ContentPage
{
	public AjouterMatchPage()
	{
		InitializeComponent();
		choixEquipeDom.ItemsSource = Dbcontext._database.Table<Equipe>().OrderBy(e => e.NomEquipe).ToList();
        choixEquipeExt.ItemsSource = Dbcontext._database.Table<Equipe>().OrderBy(e => e.NomEquipe).ToList();
    }

    private void pikEquipeDom(object sender, EventArgs e)
    {
        var equipe = choixEquipeDom.SelectedItem as Equipe;
        EquipeDom.Path = equipe.NomEquipe;
    }

    private void pikEquipeExt(object sender, EventArgs e)
    {
        var equipe = choixEquipeExt.SelectedItem as Equipe;
        EquipeExt.Path = equipe.NomEquipe;
    }

    private async void EnregistrerMatchClicked(object sender, EventArgs e)
    {
        int butEquipeDom;
        int butEquipeExt;

        if (EquipeDom.Path == "NomEquipe" || EquipeExt.Path == "NomEquipe" || ButEquipeDom.Text == null || ButEquipeExt.Text == null)
        {
            await DisplayAlert("Attention", "Veuillez remplir tous les champs avant de continuer.", "D'accord");
            return;
        }

        if (EquipeDom.Path == EquipeExt.Path)
        {
            await DisplayAlert("Attention", "Une �quipe ne peut pas jouer contre elle-m�me. Veuillez en choisir une autre.", "D'accord");
            return;
        }

        bool isValidScoreHote = int.TryParse(ButEquipeDom.Text, out butEquipeDom);
        bool isValidScoreVisiteur = int.TryParse(ButEquipeExt.Text, out butEquipeExt);

        if (!isValidScoreHote)
        {
            await DisplayAlert("Erreur", "Le score de l'�quipe � domicile doit �tre un nombre valide.", "D'accord");
            return;
        }
        if (!isValidScoreVisiteur)
        {
            await DisplayAlert("Erreur", "Le score de l'�quipe visiteuse doit �tre un nombre valide.", "D'accord");
            return;
        }

        if (choixDate.Date > DateTime.Now.Date)
        {
            await DisplayAlert("Attention", "La date du match ne peut pas �tre dans le futur.", "D'accord");
            return;
        }

        var match = new Match
        {
            EquipeDom = EquipeDom.Path,
            EquipeExt = EquipeExt.Path,
            ButEquipeDom = butEquipeDom,
            ButEquipeExt = butEquipeExt,
            Date = choixDate.Date.ToString("yyyy-MM-dd"),
        };

        List<Match> matches = Dbcontext._database.Table<Match>().OrderBy(x => x.Date).ToList();
        List<Match> listeDeTouteLesMatchesDeLaJournee = matches.FindAll(m => m.Date == match.Date);

        if (listeDeTouteLesMatchesDeLaJournee.Any(m => m.EquipeDom == match.EquipeDom || m.EquipeDom == match.EquipeExt
         || m.EquipeExt == match.EquipeDom || m.EquipeExt == match.EquipeExt))
        {
            await DisplayAlert("Attention", "Une �quipe ne peut pas jouer deux matchs le m�me jour. Veuillez v�rifier votre s�lection.", "D'accord");
            return;
        }

        Dbcontext._database.Insert(match);

        await DisplayAlert("Succ�s", "Le match a �t� enregistr� avec succ�s.", "OK");

        await Navigation.PopAsync();
    }



}