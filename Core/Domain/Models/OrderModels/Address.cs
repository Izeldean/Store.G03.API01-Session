using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModels
{
   public class Address
    {
        public Address()
        {
            
        }

        public Address(string firstName, string lastName, string street, string city, string country)
        {
            
            FirstName = firstName;
            LastName= lastName;
            Street = street;
            City = city;
            Country = country;
        }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Street {  get; set; }
        public string City { get; set; }    
        public string Country {  get; set; }
        // Order Items
        //[NotMapped]
        //public ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();// Navigational Property
    
    public DeliveryMethod DeliveryMethod { get; set; }
    }

    
}
