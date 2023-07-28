namespace JobManagementSystem.Models
{
    public class UserRegistrationDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmployer { get; set; }
        public string Company { get; set; }
    }
}
