using Models.Entities;


namespace DTO.User
{
    public class CreateUserRegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public UserRole Role { get; set; }
    }
}
