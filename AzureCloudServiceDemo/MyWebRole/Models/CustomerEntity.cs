using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace MyWebRole.Models
{
    public class CustomerEntity : TableEntity
    {
        // Your entity type must expose a parameter-less constructor
        public CustomerEntity() { }

        // Define the PK and RK
        public CustomerEntity(string lastName, string firstName)
        {
            this.PartitionKey = Guid.NewGuid().ToString();           
            this.RowKey = "";

            LastName = lastName;
            FirstName = firstName;

           
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        //For any property that should be stored in the table service, the property must be a public property of a supported type that exposes both get and set.        
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}