using GameRateApp.Data.Enums;

namespace GameRateApp.WebApi.Jwt
{
    public class JwtDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public RoleType RoleType { get; set; }
        public string SecretKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpireMinute { get; set; }
    }
}
