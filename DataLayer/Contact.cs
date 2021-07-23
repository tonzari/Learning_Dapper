using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace DataLayer
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }

        [Computed] // this attribute tells Dapper.Contrib ignore it when doing the SQL generation
        public bool IsNew => this.Id == default(int);

        [Write(false)] // we don't want the Contrib SQL generation to attempt to insert it into the DB because there is no column called: Addresses
        public List<Address> Addresses { get; } = new List<Address>();
    }
}
