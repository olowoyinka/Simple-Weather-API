namespace Weather.Domain.Service
{
    public class UserService : IUserService
    {
        public UserManager<ApplicationUser> UserManager { get; set; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        private readonly AppSettings AppSettings;

        public UserService(UserManager<ApplicationUser> userManager,
                            IRefreshTokenRepository refreshTokenRepository,
                            IOptions<AppSettings> appSettings)
        {
            this.UserManager = userManager;
            this.RefreshTokenRepository = refreshTokenRepository;
            this.AppSettings = appSettings.Value;
        }


        public async Task<(HttpStatusCode status, dynamic response)> Register(RegisterRequestDTO model)
        {
            ApplicationUser getUser = null;

            getUser = await this.UserManager.Users.SingleOrDefaultAsync(s => string.Equals(s.Email, model.email));
            if (getUser is not null)
            {
                return (HttpStatusCode.BadRequest, $"Email {model.email} exist");
            }

            var newUser = new ApplicationUser
            {
               CreateAt = DateTime.Now,
               UserName = model.email,
               FirstName = model.firstName,
               LastName = model.lastName,
               Email = model.email,
            };
            var userRegister = await this.UserManager.CreateAsync(newUser, model.password);
            
            if (!userRegister.Succeeded)
            {
                return (HttpStatusCode.BadRequest, $"Error occur while creating user {model.email}" );
            }

            return (HttpStatusCode.OK, JwtToken(newUser));
        }


        public async Task<(HttpStatusCode status, dynamic response)> Login(LoginRequestDTO model)
        {
            ApplicationUser getUser = null;

            getUser = await this.UserManager.Users.Where(s => string.Equals(s.Email, model.email)).SingleOrDefaultAsync();
            if(getUser is null)
            {
                return (HttpStatusCode.NotFound, $"User {model.email} not found");
            }

            bool checkPassword = await this.UserManager.CheckPasswordAsync(getUser, model.password);
            if (!checkPassword)
            {
                return (HttpStatusCode.BadRequest, $"Incorrect Password");
            }

            return (HttpStatusCode.OK, JwtToken(getUser));
        }


        public async Task<(HttpStatusCode status, dynamic response)> GetUser(string getEmail)
        {
            ApplicationUser getUser = null;

            getUser = await this.UserManager.Users.Where(s => string.Equals(s.Email, getEmail)).SingleOrDefaultAsync();

            if (getUser is null)
            {
                return (HttpStatusCode.NotFound, $"User {getEmail} not found");
            }

            return UserResponse(getUser);
        }


        public async Task<(HttpStatusCode status, dynamic response)> RefreshToken(string getEmail, RefreshTokenRequestDTO model)
        {
            ApplicationUser getUser = null;

            getUser = await this.UserManager.Users.Where(s => string.Equals(s.Email, getEmail)).SingleOrDefaultAsync();
            if(getUser is null)
            {
                return (HttpStatusCode.NotFound, $"User {getEmail} not found");
            }

            var findRefreshToken = this.RefreshTokenRepository.Get(s => string.Equals(s.Id, model.refreshToken) &&
                                                                   string.Equals(s.UserId, getUser.Id))
                                                          .SingleOrDefault();
            
            if (findRefreshToken is null)
            {
                return (HttpStatusCode.BadRequest, "Invalid Refresh token");
            }

            if (findRefreshToken.ExpiryTime >= DateTime.Now.AddMinutes(5))
            {
                return (HttpStatusCode.BadRequest, "The token hasn't expire");
            }

            return (HttpStatusCode.OK, JwtToken(getUser));
        }



        private (HttpStatusCode status, dynamic response) UserResponse(ApplicationUser getUser)
        {
            var userResponse = new UserResponseDTO
            {
                firstName = getUser.FirstName,
                lastName = getUser.LastName,
                email = getUser.Email,
                createAt = getUser.CreateAt.ToString("s")
            };

            return (HttpStatusCode.OK, userResponse);
        }
    

        private DataResponseDTO<LoginResponseDTO> JwtToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Secret));
            double tokenExpiryTime = Convert.ToDouble(AppSettings.ExpireTime);
            var Expires = DateTime.UtcNow.AddHours(tokenExpiryTime);
            var tokenHandler = new JwtSecurityTokenHandler();
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var userRoles = this.UserManager.GetRolesAsync( user ).Result.ToList();
            IdentityOptions identityOptions = new IdentityOptions();

            var claims = new List<Claim>
            {
                new Claim(identityOptions.ClaimsIdentity.UserNameClaimType, user.UserName),
				new Claim(identityOptions.ClaimsIdentity.EmailClaimType, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

			foreach( var role in userRoles )
			{
				claims.Add(new Claim(identityOptions.ClaimsIdentity.RoleClaimType, role ));
			}

            var newRefreshToken = new RefreshToken
            {
                Id = Guid.NewGuid().ToString("N"),
                GeneratedTime = DateTime.Now,
                ExpiryTime = Expires,
                UserId = user.Id
            };
			RefreshTokenRepository.Insert( newRefreshToken );

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: AppSettings.Site,
                audience: AppSettings.Audience,
                claims: claims,
                expires: Expires,
                signingCredentials: creds);

            return new DataResponseDTO<LoginResponseDTO>(new LoginResponseDTO
            {
                token = tokenHandler.WriteToken(token),
                expiryTime = Expires.ToString(),
                refreshToken = newRefreshToken.Id
            });
        }
    }
}