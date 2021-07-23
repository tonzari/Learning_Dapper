using System.Collections.Generic;

namespace DataLayer
{
    public interface IContactRepository
    {
        Contact Find(int id); // READ
        List<Contact> GetAll();
        Contact Add(Contact contact); // CREATE
        Contact Update(Contact contact); // UPDATE
        void Remove(int id); // DELETE
        Contact GetFullContact(int id);
        void Save(Contact contact);
    }
}
