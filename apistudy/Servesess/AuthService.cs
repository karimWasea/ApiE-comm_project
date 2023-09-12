using apistudy.interfaces;
using apistudy.Models.Entityies;
using apistudy.Seting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace apistudy.Servesess
{

    public class AuthService : IAuthService

    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }
        #region RegisterAsync
        /// <summary>
        /// Registers a new user asynchronously and returns authentication information.
        /// </summary>
        /// <param name="model">The registration information provided by the user.</param>
        /// <returns>An AuthModel containing authentication details.</returns>
        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            // Check if email is already registered
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return new AuthModel { Message = "Email is already registered!" };
            }

            // Check if username is already registered
            if (await _userManager.FindByNameAsync(model.Username) is not null)
            {
                return new AuthModel { Message = "Username is already registered!" };
            }

            // Create a new ApplicationUser with provided registration data
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            // Attempt to create the user using the UserManager
            var result = await _userManager.CreateAsync(user, model.Password);

            // If user creation fails, construct an error message with details
            if (!result.Succeeded)
            {
                var errorsBuilder = new StringBuilder();

                foreach (var error in result.Errors)
                {
                    errorsBuilder.Append($"{error.Description},");
                }

                return new AuthModel { Message = errorsBuilder.ToString() };
            }

            // Assign the "User" role to the newly registered user
            await _userManager.AddToRoleAsync(user, "User");

            // Generate a JWT token for the registered user
            JwtSecurityToken? jwtSecurityToken = await CreateJwtToken(user);

            // Generate a refresh token and associate it with the user
            RefreshToken? refreshToken = GenerateRefreshToken();
            user.RefreshTokens?.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            // Create and return an AuthModel containing authentication details
            // Construct and return an AuthModel containing authentication details
            return new AuthModel
            {
                // Assigns the registered user's email address to the Email property of the AuthModel.
                Email = user.Email,

                // Sets the ExpiresOn property of the AuthModel to the expiration time of the JWT token (jwtSecurityToken.ValidTo).
                // This reflects the time when the JWT token becomes invalid.
                ExpiresOn = jwtSecurityToken.ValidTo,

                // Sets the IsAuthenticated property of the AuthModel to true,
                // indicating that the user has been successfully authenticated.
                IsAuthenticated = true,

                // Creates a list containing the user's role(s), which in this case is set to "User".
                Roles = new List<string> { "User" },

                // Uses the JwtSecurityTokenHandler().WriteToken(jwtSecurityToken) expression
                // to serialize the JWT token into its string representation
                // and assigns it to the Token property of the AuthModel.
                // This token is used for authentication in subsequent requests.
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),

                // Assigns the registered user's username to the Username property of the AuthModel.
                Username = user.UserName,

                // Assigns the value of the refresh token (refreshToken.Token) to the RefreshToken property of the AuthModel.
                RefreshToken = refreshToken.Token,

                // Sets the RefreshTokenExpiration property of the AuthModel
                // to the expiration time of the refresh token (refreshToken.ExpiresOn).
                RefreshTokenExpiration = refreshToken.ExpiresOn
            };
        }



        #endregion



         public   async Task< IEnumerable<ApplicationUser>> GetAllusers()
        {


            return  _userManager.Users.ToList();
        }



        #region GetTokenAsync

        /// <summary>
        /// Authenticates a user and provides an access token.
        /// </summary>
        /// <param name="model">The authentication request information.</param>
        /// <returns>An AuthModel containing authentication details.</returns>
        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            // Create a new AuthModel instance to store authentication details
            var authModel = new AuthModel();

            // Find the user by their email
            var user = await _userManager.FindByEmailAsync(model.Email);

            // Check if the user is null or if the password is incorrect
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            // Generate a JWT token for the authenticated user
            var jwtSecurityToken = await CreateJwtToken(user);

            // Get the roles associated with the user
            var rolesList = await _userManager.GetRolesAsync(user);

            // Update the AuthModel with authentication details
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();

            // Check if the user has an active refresh token
            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                authModel.RefreshToken = activeRefreshToken.Token;
                authModel.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                // Generate a new refresh token and update the user
                var refreshToken = GenerateRefreshToken();
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }

            // Return the AuthModel containing authentication details
            return authModel;
        }

        #endregion


        #region AddRoleAsync
        /// <summary>
        /// Adds a role to a user asynchronously.
        /// </summary>
        /// <param name="model">The role addition information.</param>
        /// <returns>A string indicating the result of the role addition.</returns>
        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            // Find the user by their ID
            var user = await _userManager.FindByIdAsync(model.UserId);

            // Check if the user or role is invalid
            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
            {
                return "Invalid user ID or Role";
            }

            // Check if the user is already assigned to the role
            if (await _userManager.IsInRoleAsync(user, model.Role))
            {
                return "User already assigned to this role";
            }

            // Attempt to add the user to the specified role
            var result = await _userManager.AddToRoleAsync(user, model.Role);

            // Return the result of the role addition
            return result.Succeeded ? string.Empty : "Something went wrong";
        }

        #endregion
        #region CreateJwtToken
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            // Retrieve user claims and roles using the UserManager
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            // Create role claims from the roles
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            // Combine user claims, role claims, and additional claims
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("uid", user.Id)
    }
            .Union(userClaims)
            .Union(roleClaims);

            // Create a symmetric security key from the JWT secret key
            var symmetricSecurityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));

            // Create signing credentials using the symmetric security key
            var signingCredentials =
                new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            // Create a new JWT security token
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }





        #endregion
        #region  RefreshTokenAsync


        /// <summary>
        /// Refreshes a user's access token using a refresh token.
        /// </summary>
        /// <param name="token">The refresh token.</param>
        /// <returns>An AuthModel containing updated authentication details.</returns>
        public async Task<AuthModel> RefreshTokenAsync(string token)
        {
            // Create a new AuthModel instance to store authentication details
            var authModel = new AuthModel();

            // Find the user associated with the provided refresh token
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            // If no user is found with the given refresh token, set an error message and return the AuthModel
            if (user == null)
            {
                authModel.Message = "Invalid token";
                return authModel;
            }

            // Find the specific refresh token associated with the user and the provided token
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            // Check if the refresh token is active
            if (!refreshToken.IsActive)
            {
                authModel.Message = "Inactive token";
                return authModel;
            }

            // Revoke the current refresh token by marking its RevokedOn property with the current time
            refreshToken.RevokedOn = DateTime.UtcNow;

            // Generate a new refresh token and associate it with the user
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            // Generate a new JWT token for the user
            var jwtToken = await CreateJwtToken(user);

            // Update the AuthModel with the new authentication details
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            var roles = await _userManager.GetRolesAsync(user);
            authModel.Roles = roles.ToList();
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

            // Return the updated AuthModel containing authentication details
            return authModel;
        }
        #endregion
















        #region RevokeTokenAsync

        /// <summary>
        /// Revokes a user's refresh token, rendering it inactive.
        /// </summary>
        /// <param name="token">The refresh token to be revoked.</param>
        /// <returns>True if the token was successfully revoked, otherwise false.</returns>
        public async Task<bool> RevokeTokenAsync(string token)
        {
            // Find the user associated with the provided refresh token
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            // If no user is found with the given refresh token, return false (token not revoked)
            if (user == null)
            {
                return false;
            }

            // Find the specific refresh token associated with the user and the provided token
            RefreshToken? refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            // Check if the refresh token is active
            if (!refreshToken.IsActive)
            {
                return false;
            }

            // Mark the refresh token as revoked by setting its RevokedOn property to the current time
            refreshToken.RevokedOn = DateTime.UtcNow;

            // Update the user to reflect the revoked refresh token
            await _userManager.UpdateAsync(user);

            // Return true to indicate successful token revocation
            return true;
        }
        #endregion

        #region Refreshtokev
        /// <summary>
        /// Generates a new refresh token with a random value and expiration date.
        /// </summary>
        /// <returns>A new RefreshToken instance.</returns>
        private RefreshToken GenerateRefreshToken()
        {
            // Step 1: Generate a secure random number for token value
            byte[] randomNumber = new byte[32];

            using var randomNumberGenerator = new RNGCryptoServiceProvider();
            randomNumberGenerator.GetBytes(randomNumber);

            // Step 2: Convert the random bytes to a base64-encoded string
            string tokenValue = Convert.ToBase64String(randomNumber);

            // Step 3: Set the expiration date for the token (10 days from now)
            DateTime expirationDate = DateTime.UtcNow.AddDays(10);

            // Step 4: Get the current UTC time for token creation timestamp
            DateTime creationDate = DateTime.UtcNow;

            // Step 5: Create and return the RefreshToken instance
            RefreshToken refreshToken = new RefreshToken
            {
                Token = tokenValue,
                ExpiresOn = expirationDate,
                CreatedOn = creationDate
            };

            return refreshToken;
        }

        #endregion
        

    }
}
#region docmintation 
//GetClaimsAsync(user) and GetRolesAsync(user):
//These lines use the UserManager to retrieve the claims and roles associated with the provided ApplicationUser.

//Role Claims:
//The loop iterates through the user's roles and creates role claims using the "roles" claim type. This is useful for including role information in the JWT.

//claims Array:
//This array represents the claims that will be
//included in the JWT. It includes standard claims like subject (sub),
//JWT ID(jti), email, and a custom claim "uid" (user ID).
//It also combines user claims and role claims into the array using the Union method.

//Symmetric Security Key:
//A symmetric security key is
//created from the JWT secret key. This key is used to sign the JWT, ensuring its authenticity and integrity.

//Signing Credentials:
//Signing credentials are
//created using the symmetric security key and the HmacSha256 algorithm.
//This ensures that the JWT is signed with a secure hash.

//JWT Security Token: A new JwtSecurityToken is created with the specified issuer,
//audience, claims, expiration time, and signing credentials.
//The token's expiration time is set to the current time plus the specified duration.

//By following this process,
//the CreateJwtToken method generates a JWT security
//token containing user claims, role information,
//and other relevant details.
//This token can then be used for authentication and authorization within your application.

//Please adapt this explanation to
//match your code context and integrate it into your documentation or code comments for clear understanding.
#endregion