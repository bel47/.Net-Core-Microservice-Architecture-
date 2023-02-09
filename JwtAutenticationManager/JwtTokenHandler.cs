using JwtAutenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAutenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "jhj7Bhgf2gfd5fMdRfsd454Usadfs";
        public int JWT_VALIDITY_TOKEN_MINS = 20;
        public readonly List<UserAccount> _userAccountsList;

        public JwtTokenHandler()
        {
            _userAccountsList = new List<UserAccount> {
                new UserAccount { UserName="admin", Password="admin123", Role="Administrator" },
                new UserAccount { UserName = "user", Password = "user123", Role = "User" }
            };
        }

        public AuthenticationResponse GenrateJwtToken(AuthenticationRequest authenticationRequest) {

            if (string.IsNullOrWhiteSpace(authenticationRequest.UserName) || string.IsNullOrWhiteSpace(authenticationRequest.UserName))
                return null;

            /* Validate */
            var userAccount = _userAccountsList.Where(x => x.UserName == authenticationRequest.UserName && x.Password == authenticationRequest.Password).FirstOrDefault();
            if (userAccount == null) return null;
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_VALIDITY_TOKEN_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.UserName),
                //new Claim(ClaimTypes.Role, userAccount.Role)
                //Ocelot Role base
                new Claim("Role", userAccount.Role)
            });

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDesciptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                UserName = authenticationRequest.UserName,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token,
            };
        }
    }
}
