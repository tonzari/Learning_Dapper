using DataLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.IO;

namespace Runner
{
    class Program
    {
        private static IConfigurationRoot config;

        static void Main(string[] args)
        {
            Console.WriteLine("Program starting...");
            Initialize();

            //Get_all_should_return_6_results();

            //Insert_should_assign_identity_to_new_entity();

            //Find_should_retrieve_existing_entity(7);

            //Modify_should_update_existing_entity(7);

            Delete_should_remove_entity(3);
        }

        // TEST methods

        static void Delete_should_remove_entity(int id)
        {
            //arrange
            IContactRepository repository = CreateRepository();

            //act
            var contactToRemove = repository.Find(id);
            if (contactToRemove != null)
            {
                Console.WriteLine($"Deleting {contactToRemove.FirstName}...");
                repository.Remove(id);
            }

            //rearrange verification
            IContactRepository repositoryUpdated = CreateRepository();
            var deletedContact = repositoryUpdated.Find(id);

            //assert
            if (deletedContact == null)
            {
                Console.WriteLine("**CONTACT DELETED**");
            }
        }

        static void Modify_should_update_existing_entity(int id)
        {
            //arrange
            IContactRepository repository = CreateRepository();

            //act
            Contact contact = repository.Find(id);
            contact.LastName = "Manzana";
            repository.Update(contact);

            //rearrange! verification
            IContactRepository repositoryUpdated = CreateRepository();
            Contact contactModified = repositoryUpdated.Find(id);

            //assert
            Console.WriteLine("**CONTACT MODIFIED**");
            Console.WriteLine($"Updated Contact Lastname: {contactModified.LastName}");
        }

        static void Get_all_should_return_6_results()
        {
            //arrange
            var repository = CreateRepository();

            //act
            var contacts = repository.GetAll();

            //assert
            Console.WriteLine($"Count: {contacts.Count}");
            // you can add some Debug.Asserts here if you want

        }

        static int Insert_should_assign_identity_to_new_entity()
        {
            //arrange
            var repository = CreateRepository();
            var newContact = new Contact()
            {
                FirstName = "Antonio",
                LastName = "Manzari",
                Email = "whocares",
                Company = "GC",
                Title = "Pants Boy"
            };

            //act
            repository.Add(newContact);

            //assert
            Console.WriteLine("Inserted NEW CONTACT!!");
            Console.WriteLine($"New ID: {newContact.Id}");
            return newContact.Id;
        }

        static void Find_should_retrieve_existing_entity(int id)
        {
            //arrange
            IContactRepository repository = CreateRepository();

            //act
            var contact = repository.Find(id);

            //assert
            Console.WriteLine("**GET CONTACT**");
            Console.WriteLine(contact.FirstName + " " + contact.LastName);
        }

        private static void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            config = builder.Build();
        }

        private static IContactRepository CreateRepository()
        {
            return new ContactRepository(config.GetConnectionString("DefaultConnection"));
        }
    }
}
