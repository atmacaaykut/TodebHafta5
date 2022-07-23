using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace Models.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Product> Products { get; set; }

        public Address Address { get; set; }
    }
}