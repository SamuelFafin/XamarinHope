using Swaguin.Model;
using Swaguin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinUniversity.Interfaces;
using XamarinUniversity.Services;

namespace Swaguin
{
    public partial class App : Application
    {

        public static ContactRepository ContactRepo { get; private set; }

        static App()
        {
            // Register dependencies.
            DependencyService.Register<ContactListViewModel>();
            // Register standard XamU services
            XamUInfrastructure.Init();
        }

        public App(string dbPath)
        {
            InitializeComponent();

            ContactRepo = new ContactRepository(dbPath);

            MasterDetailPage mdPage = new MainPage();
            MainPage = mdPage;

            var navService = DependencyService.Get<INavigationService>() as FormsNavigationPageService;
            navService.RegisterPage(AppPages.Edit, () => new EditContactPage());
            navService.RegisterPage(AppPages.Detail, () => new ContactDetail());
        }

        public async void Insert()
        {
            await App.ContactRepo.AddNewContactAsync(new Contact { FirstName = "Samuel", LastName = "Fafin", Email = "sa.fafin@gmail.com", PhoneNumber = "0603252609" });
        }

        public IEnumerable<Contact> GetAllContact()
        {
            return App.ContactRepo.GetAllContactAsync().Result;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
