using Swaguin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swaguin.Model
{
    public static class ContactManager
    {
        public static IEnumerable<Contact> Load(IContactLoader loader)
        {
            if (loader == null)
                throw new Exception("Missing quote loader from platform.");

            return loader.Load();
        }

        public static void Save(IContactLoader loader, IEnumerable<ContactViewModel> contacts)
        {
            if (loader == null)
                throw new Exception("Missing quote loader from platform.");

            loader.Save(contacts);
        }
    }
}
