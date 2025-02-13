using _1Xbet.Models;
using _1Xbet.Services; // Assurez-vous d'avoir un service pour g�rer les donn�es
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
                await DisplayAlert("Erreur", "Le nom de l'�quipe ne peut pas �tre vide.", "OK");
                return;
            }

            // V�rifier si l'�quipe existe d�j�
            var equipeExistante = _dbContext.GetTeams().FirstOrDefault(e => e.NomEquipe.Equals(nomEquipe, StringComparison.OrdinalIgnoreCase));
            if (equipeExistante != null)
            {
                await DisplayAlert("Erreur", "Cette �quipe existe d�j�.", "OK");
                return;
            }

            // Ajouter l'�quipe dans la base de donn�es
            var nouvelleEquipe = new Equipe
            {
                NomEquipe = nomEquipe,
                NbreMatchJoue = 0,
                NbreButsMarque = 0,
                NbreButsEncaisse = 0,
                NbrePoints = 0
            };

            _dbContext.AddTeam(nouvelleEquipe);

            // Envoyer un message pour rafra�chir la liste des �quipes
            MessagingCenter.Send(this, "NouvelleEquipe", nouvelleEquipe);

            // Revenir � la liste des �quipes
            await Navigation.PopAsync();
        }
    }
}
