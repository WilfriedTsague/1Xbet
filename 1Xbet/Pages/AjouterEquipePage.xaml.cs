using _1Xbet.Models;
using _1Xbet.Services; 
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace _1Xbet.Pages
{
    public partial class AjouterEquipePage : ContentPage
    {
        private readonly Equipe _equipe;

        public AjouterEquipePage()
        {
            InitializeComponent();
            _equipe = null;
        }

        public AjouterEquipePage(Equipe equipe)
        {
            InitializeComponent ();
            _equipe = equipe;

            nomEquipeEntry.Text = _equipe.NomEquipe;
            descriptionEquipeEntry.Text = _equipe.Description;

        }

        private async void AjouterOuModifierEquipe(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nomEquipeEntry.Text))
            {
                await DisplayAlert("Attention", "Veuillez entrer un nom d'�quipe.", "Ok");
                return;
            }

            if (string.IsNullOrWhiteSpace(descriptionEquipeEntry.Text))
            {
                await DisplayAlert("Attention", "Veuillez entrer une description.", "Ok");
                return;
            }

            if (_equipe == null)
            {

                bool equipeExiste = Dbcontext._database.Table<Equipe>().Any(e => e.NomEquipe == nomEquipeEntry.Text);
                if (equipeExiste)
                {
                    await DisplayAlert("Erreur", "Une �quipe avec ce nom existe d�j�.", "Ok");
                    return;
                }

                var nouvelleEquipe = new Equipe
                {
                    NomEquipe = nomEquipeEntry.Text,
                    Description = descriptionEquipeEntry.Text
                };

                Dbcontext._database.Insert(nouvelleEquipe);
                await DisplayAlert("Succ�s", "L'�quipe a �t� ajout�e avec succ�s.", "Ok");
            }
            else
            {
                _equipe.NomEquipe = nomEquipeEntry.Text;
                _equipe.Description = descriptionEquipeEntry.Text;

                Dbcontext._database.Update(_equipe);
                await DisplayAlert("Succ�s", "L'�quipe a �t� mise � jour avec succ�s.", "Ok");
            }

            await Navigation.PopAsync();
        }

    }
}
