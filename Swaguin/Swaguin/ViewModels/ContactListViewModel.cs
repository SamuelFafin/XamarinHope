using Swaguin.Infrastructure;
using Swaguin.Model;
using Swaguin.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinUniversity.Interfaces;

// ========================================
// ViewModel principal de l'application
// ========================================

namespace Swaguin.ViewModels
{
    public class ContactListViewModel : SimpleViewModel
    {
        //
        // Déclaration des Attributs
        //

        IDependencyService serviceLocator;

        // Liste de tous les contacts disponibles dans la base de données
        public ObservableCollection<ContactViewModel> Contacts { get; private set; }


        // Déclaration des différentes ICommand
        public ICommand AddContact { get; private set; }
        public ICommand DeleteContact { get; private set; }
        public ICommand EditContact { get; private set; }
        public ICommand ShowContactDetail { get; private set; }
        public ICommand SendEmail { get; private set; }
        public ICommand Call { get; private set; }
        public ICommand Sms { get; private set; }


        // Contact sélectionné dans l'application
        ContactViewModel selectedContact;
        public ContactViewModel SelectedContact
        {
            get
            {
                return selectedContact;
            }
            set
            {
                SetPropertyValue(ref selectedContact, value);
            }
        }

        //
        // Déclaration des Méthodes
        //

        // Constructeur
        public ContactListViewModel() : this(new XamarinUniversity.Services.DependencyServiceWrapper())
        {
        }

        // Constructeur
        public ContactListViewModel(IDependencyService serviceLocator)
        {
            this.serviceLocator = serviceLocator;

            // Récupérer la liste de contacts
            Contacts = new ObservableCollection<ContactViewModel>(
                 ((IEnumerable<Contact>)(App.ContactRepo.GetAllContactAsync().Result)).Select(q => new ContactViewModel(q)));

            // Relier les ICommand à leur méthode
            AddContact = new Command(async () => await OnAddContact());
            DeleteContact = new Command<ContactViewModel>(async qvm => await OnDeleteContact(qvm));
            EditContact = new Command<ContactViewModel>(async qvm => await OnEditContact(qvm));
            ShowContactDetail = new Command<ContactViewModel>(async qvm => await OnShowContactDetails(qvm));
            SendEmail = new Command<ContactViewModel>(OnSendMail);
            Call = new Command<ContactViewModel>(OnCall);
            Sms = new Command<ContactViewModel>(OnSms);  
        }

        // Ajouter un contact en redirigeant vers la page d'édition
        private async Task OnAddContact()
        {
            ContactViewModel newContact = new ContactViewModel();
            Contacts.Add(newContact);

            SelectedContact = newContact;

            if (!serviceLocator.Get<INavigationService>().CanGoBack)
            {
                await serviceLocator.Get<INavigationService>()
                                   .NavigateAsync(AppPages.Edit, newContact);
            }
        }

        // Détailler les informations d'un contact en redirigeant vers la page de détail
        private async Task OnShowContactDetails(ContactViewModel qvm)
        {
            SelectedContact = qvm;
            await serviceLocator.Get<INavigationService>()
                .NavigateAsync(AppPages.Detail);
        }

        // Modifier les informations d'un contact en redirigeant vers la page d'édition
        private async Task OnEditContact(ContactViewModel contact)
        {
            SelectedContact = contact;
            await serviceLocator.Get<INavigationService>()
                .NavigateAsync(AppPages.Edit, contact);
        }

        // Supprimer un contact
        private async Task OnDeleteContact(ContactViewModel contact)
        {

            // Ajouter une étape de validation avant de supprimer
            bool result = await serviceLocator.Get<IMessageVisualizerService>()
                .ShowMessage("Are you sure?",
                    "Are you sure you want to delete this quote from " + contact.FirstName + "?",
                    "Yes", "No");

            // Confirmer la demande de suppression par l'utilisateur
            if (result == true)
            {
                // Récupérer l'Id et l'adapter pour supprimer la bonne valeur
                int pos = Contacts.IndexOf(contact);
                if (SelectedContact == contact)
                {
                    if (pos > Contacts.Count - 1)
                        pos = Contacts.Count - 1;
                    SelectedContact = Contacts[pos];
                }

                // Supprimer l'élément de la liste affiché sur la page ContactList
                Contacts.Remove(contact);

                // Appeler la méthode pour supprimer l'élément de la base de données
                await App.ContactRepo.DeleteContactById(contact.Model);

                // Rediriger la view affichée par la précédente
                await DependencyService.Get<INavigationService>().GoBackAsync();

                SelectedContact = null;
            }
        }

        // Envoyer un email en redirigeant vers l'outil de messagerie par défaut
        private void OnSendMail(ContactViewModel contact)
        {
            Device.OpenUri(new Uri("mailto:" + contact.Email));
        }

        // Passer un appel en redirigeant vers le dial d'appel
        private void OnCall(ContactViewModel contact)
        {
            Device.OpenUri(new Uri("tel:" + contact.PhoneNumber));
        }

        // Envoyer un sms en redirigeant vers l'application de sms
        private void OnSms(ContactViewModel contact)
        {
            Device.OpenUri(new Uri("smsto:" + contact.PhoneNumber));
        }
    }
}
