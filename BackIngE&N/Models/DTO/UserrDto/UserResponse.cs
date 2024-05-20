namespace BackIngE_N.Models.DTO.UserrDto
{
    public class UserResponse
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Token { get; set; }
        public string Role { get; set; } = null!;

        public UserResponse(Guid id, string name, string lastName, string email, string role, string? token)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Email = email;
            Role = role;
            Token = token;
        }

    }
}
