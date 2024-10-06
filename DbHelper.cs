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

        public async Task AddSampleContacts()
        {
            var contacts = await GetContacts();

            if (contacts.Count == 0)
            {
                var sampleContacts = new List<Contact>
                {
                    new Contact { Name = "Alice Smith", Photo = "dotnet_bot.png", Email = "alice@example.com", PhoneNumber = "1234567890", Address = "123 Apple St", Description = "Friend from school" },
                    new Contact { Name = "Bob Johnson", Photo = "dotnet_bot.png", Email = "bob@example.com", PhoneNumber = "2345678901", Address = "456 Orange Ave", Description = "Colleague at work" },
                    new Contact { Name = "Charlie Brown", Photo = "dotnet_bot.png", Email = "charlie@example.com", PhoneNumber = "3456789012", Address = "789 Banana Blvd", Description = "Neighbor" },
                    new Contact { Name = "Ivan Moore", Photo = "dotnet_bot.png", Email = "ivan@example.com", PhoneNumber = "9012345678", Address = "333 Maple Dr", Description = "Old roommate" },
                };

                foreach (var contact in sampleContacts)
                {
                    await SaveContact(contact);
                }
            }
        }


    }
}