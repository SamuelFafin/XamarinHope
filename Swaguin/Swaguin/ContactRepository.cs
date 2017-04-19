using SQLite;
using Swaguin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swaguin
{
    public class ContactRepository
    {
        // Attribut de connexion à la base de données
        private readonly SQLiteAsyncConnection conn;

        public string StatusMessage { get; set; }

        // Constructeur
        public ContactRepository(string dbPath)
        {
            // Créer une connexion à la base de données
            conn = new SQLiteAsyncConnection(dbPath);
            // Créer la table contact si elle n'existe pas 
            conn.CreateTableAsync<Contact>().Wait();
        }

        // Ajouter un nouveau contact dans la base de données ou l'update
        public async Task AddNewContactAsync(Contact contact)
        {
            try
            {
                // Vérifier que le contact existe
                var existingContact = await conn.FindAsync<Contact>(u => u.Id == contact.Id);

                // S'il existe alors Update ses valeurs
                if(existingContact != null)
                {
                    var result = await conn.UpdateAsync(contact).ConfigureAwait(continueOnCapturedContext: false);
                    StatusMessage = string.Format("{0} record(s) added [firstname: {1})", result, contact);
                }
                //Sinon Insert une nouvelle ligne
                else
                {
                    var result = await conn.InsertAsync(contact).ConfigureAwait(continueOnCapturedContext: false);
                    StatusMessage = string.Format("{0} record(s) added [firstname: {1})", result, contact);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", contact, ex.Message);
            }
        }

        // Obtenir la liste de contacts
        public Task<List<Contact>> GetAllContactAsync()
        {
            //return a list of people saved to the Contact table in the database
            return conn.Table<Contact>().ToListAsync();
        }

        // Supprimer un contact en fonctioin de son Id
        public Task DeleteContactById(Contact contact)
        {
            return conn.DeleteAsync(contact);
        }
    }
}
