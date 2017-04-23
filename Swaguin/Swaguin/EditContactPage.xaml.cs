using Plugin.Media;
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

        async void TakePhoto(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            DisplayAlert("File Location", file.Path, "OK");

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        async void PickPhoto(object sender, EventArgs e)
        {

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });


            if (file == null)
                return;

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
    }
}
