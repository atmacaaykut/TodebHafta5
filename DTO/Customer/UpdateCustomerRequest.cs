namespace DTO.Customer
{
    public class UpdateCustomerRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
