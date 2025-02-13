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
                await DisplayAlert("Attention", "Veuillez entrer un nom d'équipe.", "Ok");
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
                    await DisplayAlert("Erreur", "Une équipe avec ce nom existe déjà.", "Ok");
                    return;
                }

                var nouvelleEquipe = new Equipe
                {
                    NomEquipe = nomEquipeEntry.Text,
                    Description = descriptionEquipeEntry.Text
                };

                Dbcontext._database.Insert(nouvelleEquipe);
                await DisplayAlert("Succès", "L'équipe a été ajoutée avec succès.", "Ok");
            }
            else
            {
                _equipe.NomEquipe = nomEquipeEntry.Text;
                _equipe.Description = descriptionEquipeEntry.Text;

                Dbcontext._database.Update(_equipe);
                await DisplayAlert("Succès", "L'équipe a été mise à jour avec succès.", "Ok");
            }

            await Navigation.PopAsync();
        }

    }
}
