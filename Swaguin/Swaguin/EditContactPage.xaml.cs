using Swaguin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinUniversity.Interfaces;

namespace Swaguin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditContactPage : ContentPage
    {
        // Constructeur
        public EditContactPage()
        {
            InitializeComponent();
        }


        // Retourner sur la page précédente de l'application
        async void OnGoBack(object sender, EventArgs e)
        {
            // Récupérer le ViewModel qui gère la liste de contacts
            var vm = DependencyService.Get<ContactListViewModel>();
            // Inserrer le contact sélectionné
            await App.ContactRepo.AddNewContactAsync(vm.SelectedContact.Model);

            // Retourner sur la page précédente
            await DependencyService.Get<INavigationService>().GoBackAsync();

            vm.SelectedContact = null;
        }
    }
}
