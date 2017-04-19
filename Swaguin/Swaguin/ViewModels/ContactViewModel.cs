using Swaguin.Infrastructure;
using Swaguin.Model;
using System;
using System.Threading.Tasks;

namespace Swaguin.ViewModels
{
    public class ContactViewModel : SimpleViewModel
    {
        readonly Contact contact;
        private ContactViewModel q;

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

        public ContactViewModel(Contact contact)
        {
            this.contact = contact;
        }

        public ContactViewModel(ContactViewModel q)
        {
            this.q = q;
        }

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

        internal Contact Model
        {
            get { return contact; }
        }

    }
}
