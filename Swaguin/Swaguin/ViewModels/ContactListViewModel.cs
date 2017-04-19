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

namespace Swaguin.ViewModels
{
    public class ContactListViewModel : SimpleViewModel
    {
        IDependencyService serviceLocator;
        public ObservableCollection<ContactViewModel> Contacts { get; private set; }

        public ICommand AddContact { get; private set; }
        public ICommand DeleteContact { get; private set; }
        public ICommand EditContact { get; private set; }
        public ICommand ShowContactDetail { get; private set; }
        public ICommand SendEmail { get; private set; }
        public ICommand Call { get; private set; }
        public ICommand Sms { get; private set; }

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

        public ContactListViewModel() : this(new XamarinUniversity.Services.DependencyServiceWrapper())
        {
        }
        public ContactListViewModel(IDependencyService serviceLocator)
        {
            this.serviceLocator = serviceLocator;

            Contacts = new ObservableCollection<ContactViewModel>(
                 ((IEnumerable<Contact>)(App.ContactRepo.GetAllContactAsync().Result)).Select(q => new ContactViewModel(q)));

            AddContact = new Command(async () => await OnAddContact());
            DeleteContact = new Command<ContactViewModel>(async qvm => await OnDeleteContact(qvm));
            EditContact = new Command<ContactViewModel>(async qvm => await OnEditContact(qvm));
            ShowContactDetail = new Command<ContactViewModel>(async qvm => await OnShowContactDetails(qvm));
            SendEmail = new Command<ContactViewModel>(OnSendMail);
            Call = new Command<ContactViewModel>(OnCall);
            Sms = new Command<ContactViewModel>(OnSms);
        }

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

            // Trouver à quel moment sauvegarder  
            //await App.ContactRepo.AddNewContactAsync(newContact.Model);
        }

        private async Task OnShowContactDetails(ContactViewModel qvm)
        {
            SelectedContact = qvm;
            await serviceLocator.Get<INavigationService>()
                .NavigateAsync(AppPages.Detail);
        }

        private async Task OnEditContact(ContactViewModel contact)
        {
            SelectedContact = contact;
            await serviceLocator.Get<INavigationService>()
                .NavigateAsync(AppPages.Edit, contact);
        }

        private async Task OnDeleteContact(ContactViewModel contact)
        {
            bool result = await serviceLocator.Get<IMessageVisualizerService>()
                .ShowMessage("Are you sure?",
                    "Are you sure you want to delete this quote from " + contact.FirstName + "?",
                    "Yes", "No");

            if (result == true)
            {
                int pos = Contacts.IndexOf(contact);
                Contacts.Remove(contact);
                if (SelectedContact == contact)
                {
                    if (pos > Contacts.Count - 1)
                        pos = Contacts.Count - 1;
                    SelectedContact = Contacts[pos];
                }

                await App.ContactRepo.DeleteContactById(contact.Model);

                await DependencyService.Get<INavigationService>().GoBackAsync();

                SelectedContact = null;
            }
        }

        private void OnSendMail(ContactViewModel contact)
        {
            Device.OpenUri(new Uri("mailto:" + contact.Email));
        }

        private void OnCall(ContactViewModel contact)
        {
            Device.OpenUri(new Uri("tel:" + contact.PhoneNumber));
        }

        private void OnSms(ContactViewModel contact)
        {
            Device.OpenUri(new Uri("smsto:" + contact.PhoneNumber));
        }
    }
}
