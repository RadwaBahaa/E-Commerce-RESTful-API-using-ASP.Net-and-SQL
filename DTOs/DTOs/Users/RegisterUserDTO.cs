namespace DTOs.DTOs.Users
{
    public class RegisterUserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public string? PhoneNumber {  get; set; }
        public string? Address { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
