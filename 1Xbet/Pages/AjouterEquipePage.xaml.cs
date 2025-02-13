using _1Xbet.Models;
using _1Xbet.Services; // Assurez-vous d'avoir un service pour gérer les données
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace _1Xbet.Pages
{
    public partial class AjouterEquipePage : ContentPage
    {
        private readonly Dbcontext _dbContext;

        public AjouterEquipePage()
        {
            InitializeComponent();
            _dbContext = new Dbcontext("/1Xbet.db");
        }

        private async void OnAjouterClicked(object sender, EventArgs e)
        {
            string nomEquipe = nomEquipeEntry.Text?.Trim();

            if (string.IsNullOrEmpty(nomEquipe))
            {
                await DisplayAlert("Erreur", "Le nom de l'équipe ne peut pas être vide.", "OK");
                return;
            }

            // Vérifier si l'équipe existe déjà
            var equipeExistante = _dbContext.GetTeams().FirstOrDefault(e => e.NomEquipe.Equals(nomEquipe, StringComparison.OrdinalIgnoreCase));
            if (equipeExistante != null)
            {
                await DisplayAlert("Erreur", "Cette équipe existe déjà.", "OK");
                return;
            }

            // Ajouter l'équipe dans la base de données
            var nouvelleEquipe = new Equipe
            {
                NomEquipe = nomEquipe,
                NbreMatchJoue = 0,
                NbreButsMarque = 0,
                NbreButsEncaisse = 0,
                NbrePoints = 0
            };

            _dbContext.AddTeam(nouvelleEquipe);

            // Envoyer un message pour rafraîchir la liste des équipes
            MessagingCenter.Send(this, "NouvelleEquipe", nouvelleEquipe);

            // Revenir à la liste des équipes
            await Navigation.PopAsync();
        }
    }
}
