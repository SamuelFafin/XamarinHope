using Swaguin.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swaguin.Model
{
    public class ContactLoader : IContactLoader
    {
        public IEnumerable<Contact> Load()
        {
            IEnumerable<Contact> Contacts = App.ContactRepo.GetAllContactAsync().Result;
            return Contacts;
        }

        public void Save(IEnumerable<ContactViewModel> contacts)
        {
            throw new NotImplementedException();
        }
    }
}
