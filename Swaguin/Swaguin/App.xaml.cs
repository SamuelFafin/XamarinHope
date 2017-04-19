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
        // Attribut static stockant l'instance de la base de données
        public static ContactRepository ContactRepo { get; private set; }

        static App()
        {
            // Register dependencies.
            DependencyService.Register<ContactListViewModel>();
            // Register standard XamU services
            XamUInfrastructure.Init();
        }

        // Constructeur
        public App(string dbPath)
        {
            InitializeComponent();

            // Initialiser l'accès vers la base de données
            ContactRepo = new ContactRepository(dbPath);

            // Gérer les vues
            MasterDetailPage mdPage = new MainPage();
            MainPage = mdPage;

            // Gérer la navigation dans l'application
            var navService = DependencyService.Get<INavigationService>() as FormsNavigationPageService;
            navService.RegisterPage(AppPages.Edit, () => new EditContactPage());
            navService.RegisterPage(AppPages.Detail, () => new ContactDetail());
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
