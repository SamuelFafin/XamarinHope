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
        private readonly SQLiteAsyncConnection conn;

        public string StatusMessage { get; set; }

        public ContactRepository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Contact>().Wait();
        }

        public async Task AddNewContactAsync(Contact contact)
        {
            try
            {

                var existingContact = await conn.FindAsync<Contact>(u => u.Id == contact.Id);

                if(existingContact != null)
                {
                    var result = await conn.UpdateAsync(contact).ConfigureAwait(continueOnCapturedContext: false);
                    StatusMessage = string.Format("{0} record(s) added [firstname: {1})", result, contact);
                }
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

        public Task<List<Contact>> GetAllContactAsync()
        {
            //return a list of people saved to the Contact table in the database
            return conn.Table<Contact>().ToListAsync();
        }

        public Task DeleteContactById(Contact contact)
        {
            return conn.DeleteAsync(contact);
        }
    }
}
