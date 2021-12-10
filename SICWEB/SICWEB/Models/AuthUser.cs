namespace SICWEB.Models
{
    public class AuthUser : IAuthUser
    {
        public string Status { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public AuthUser(string status, string token, string userName, string userEmail)
        {
            Status = status;
            Token = token;
            UserName = userName;
            Email = userEmail;
        }
    }
}