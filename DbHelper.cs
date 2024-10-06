using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Contacts
{
    public class DbHelper
    {
        private SQLiteAsyncConnection _database;
        public DbHelper()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "contacts.db");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Contact>().Wait();
        }

        public async Task<List<Contact>> GetContacts()
        {
            List<Contact> contacts = await _database.Table<Contact>().ToListAsync();
            return contacts;
        }

        public async Task<Contact> GetContactById(int id)
        {
            Contact contact = await _database.Table<Contact>().FirstOrDefaultAsync(c => c.Id == id);
            return contact;
        }

        public Task<int> SaveContact(Contact contact)
        {
            if (contact.Id != 0)
            {
                return _database.UpdateAsync(contact);
            }
            else
            {
                return _database.InsertAsync(contact);
            }
        }

        public Task<int> DeleteContact(Contact contact)
        {
            return _database.DeleteAsync(contact);
        }
    }
}