namespace APP.Entities
{
    public  class Account
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public string Role { get; set; } = "User";
    } 
}