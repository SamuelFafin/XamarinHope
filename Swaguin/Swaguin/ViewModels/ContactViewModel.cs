using Swaguin.Infrastructure;
using Swaguin.Model;
using System;
using System.Threading.Tasks;

// ==============================
// ViewModel permettant d'accéder au Model Contact
// ==============================

namespace Swaguin.ViewModels
{
    public class ContactViewModel : SimpleViewModel
    {
        // Instance d'un contact
        readonly Contact contact;
        private ContactViewModel q;

        // Constructeur
        public ContactViewModel()
            : this(new Contact())
        {
            FirstName = "";
            LastName = "";
            Email = "";
            Gender = false;
            Favorite = false;
            Birthdate = DateTime.Now;
            PhoneNumber = "";
            ImagePath = "";            
        }

        // Constructeur
        public ContactViewModel(Contact contact)
        {
            this.contact = contact;
        }

        // Constructeur
        public ContactViewModel(ContactViewModel q)
        {
            this.q = q;
        }

        // Get et Set pour l'attribut FirstName
        public string FirstName
        {
            get { return contact.FirstName; }
            set
            {
                if (contact.FirstName != value)
                {
                    contact.FirstName = value;
                    RaisePropertyChanged();
                }
            }

        }

        // Get et Set pour l'attribut LastName
        public string LastName
        {
            get { return contact.LastName; }
            set
            {
                if (contact.LastName != value)
                {
                    contact.LastName = value;
                    RaisePropertyChanged();
                }
            }
        }

        // Get et Set pour l'attribut Email
        public string Email
        {
            get { return contact.Email; }
            set
            {
                if (contact.Email != value)
                {
                    contact.Email = value;
                    RaisePropertyChanged();
                }
            }
        }

        // Get et Set pour l'attribut ImagePath
        public string ImagePath
        {
            set
            {
                if (contact.ImagePath != value)
                {
                    contact.ImagePath = value;

                    RaisePropertyChanged();
                }
            }
            get
            {
                return contact.ImagePath;
            }
        }

        // Get et Set pour l'attribut PhoneNumber
        public string PhoneNumber
        {
            set
            {
                if (contact.PhoneNumber != value)
                {
                    contact.PhoneNumber = value;

                    RaisePropertyChanged();
                }
            }
            get
            {
                return contact.PhoneNumber;
            }
        }

        // Get et Set pour l'attribut Birthdate
        public DateTime Birthdate
        {
            set
            {
                if (contact.Birthdate != value)
                {
                    contact.Birthdate = value;

                    RaisePropertyChanged();
                }
            }
            get
            {
                return contact.Birthdate;
            }
        }

        // Get et Set pour l'attribut Favorite
        public bool Favorite
        {
            set
            {
                if (contact.Favorite != value)
                {
                    contact.Favorite = value;

                    RaisePropertyChanged();
                }
            }
            get
            {
                return contact.Favorite;
            }
        }

        // Get et Set pour l'attribut Gender
        public bool Gender
        {
            set
            {
                if (contact.Gender != value)
                {
                    contact.Gender = value;

                    RaisePropertyChanged();
                }
            }
            get
            {
                return contact.Gender;
            }
        }

        // Get permetant de récupérer directement la variable contact
        internal Contact Model
        {
            get { return contact; }
        }

    }
}
