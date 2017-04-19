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
        public EditContactPage()
        {
            InitializeComponent();
        }

        async void OnGoBack(object sender, EventArgs e)
        {
            var vm = DependencyService.Get<ContactListViewModel>();
            await App.ContactRepo.AddNewContactAsync(vm.SelectedContact.Model);

            await DependencyService.Get<INavigationService>().GoBackAsync();

            vm.SelectedContact = null;
        }
    }
}
