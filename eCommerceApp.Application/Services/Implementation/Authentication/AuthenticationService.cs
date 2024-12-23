using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.Services.Interfaces.Authentication;
using eCommerceApp.Application.Services.Interfaces.Logging;
using eCommerceApp.Application.Validations;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Domain.Models.Identity;
using FluentValidation;

namespace eCommerceApp.Application.Services.Implementation.Authentication
{
    public class AuthenticationService(ITokenManagement tokenManagement, IAppLogger<AuthenticationService> logger,
        IUserManagement userManagement, IRoleManagement rolemanagement,
        IMapper mapper, IValidator<CreateUser> createUserValidator, IValidator<LogInUser> loginUserValidator
       , IValidationService validationSevice) : IAuthenticationService
    {
        public async Task<AuthResponse> CreateUser(CreateUser user)
        {

            var validationResult = await validationSevice.ValidateAsync(user, createUserValidator);
            if (!validationResult.Success) return validationResult;

            var appUser = mapper.Map<AppUser>(user);
            appUser.UserName = user.Email;
            appUser.PasswordHash = user.Password;

            var result = await userManagement.CreateUser(appUser);
            if (!result)
                return new AuthResponse { Message = "Email Address might be already in use or unknown error occurred" };

            var _user = await userManagement.GetUserByEmail(user.Email);
            var users = await userManagement.GetAllUsers();
            //assigning role
            bool assignedResult = await rolemanagement.AddUserToRole(_user!, users!.Count() > 1 ? "User" : "Admin");

            if (!assignedResult)
            {
                //remove user
                int removeUserResult = await userManagement.RemoveUserByEmail(_user!.Email!);
                if (removeUserResult <= 0)
                {
                    //error occurred while rolling back changes
                    //log the error
                    logger.LogError(
                        new Exception($"User with Email as {_user.Email} failed to be removed as a result of role assigning issue"),
                        "User could not be assigned Role");
                    return new AuthResponse { Message = "Error occurred in creating account" };

                }
            }
            var roles = await rolemanagement.GetUserRoles(_user!.Email!);
            var claims = await userManagement.GetUserClaims(_user!.Email!);
            string jwtToken = tokenManagement.GenerateToken(claims);
            var refreshToken = tokenManagement.GetRefreshToken();
            var saveTokenResult = await tokenManagement.AddRefreshToken(_user.Id, refreshToken);
            return saveTokenResult <= 0 ?
               new AuthResponse { Message = "Internal error occurred while authenticating" } :
            new AuthResponse {
                Success = true,
                Message = "Account created!",
                Roles = roles ,
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration =refreshToken.ExpiredOn
            };

            //verify Email
        }

        public async Task<AuthResponse> LoginUser(LogInUser user, string? inputRefreshToken)
        {
            var validationResult = await validationSevice.ValidateAsync(user, loginUserValidator);
            if (!validationResult.Success)
                return new AuthResponse { Message = validationResult.Message };

            var mappedModel = mapper.Map<AppUser>(user);
            mappedModel.PasswordHash = user.Password;

            bool loginResult = await userManagement.LoginUser(mappedModel);
            if (!loginResult)
                return new AuthResponse { Message = "Email not found or invalid credentials" };

            var _user = await userManagement.GetUserByEmail(user.Email);
            var roles = await rolemanagement.GetUserRoles(_user!.Email!);
            var claims = await userManagement.GetUserClaims(_user!.Email!);
            string jwtToken = tokenManagement.GenerateToken(claims);
            var refreshToken = tokenManagement.GetRefreshToken();

            int saveTokenResult;
            bool userTokenCheck = await tokenManagement.ValidateRefreshToken(inputRefreshToken);
            if (userTokenCheck)
            {
                saveTokenResult = await tokenManagement.UpdateRefreshToken(_user.Id, refreshToken.Token);
            }
            else
            {
                saveTokenResult = await tokenManagement.AddRefreshToken(_user.Id, refreshToken);

            }
            return saveTokenResult <= 0 ?
                new AuthResponse { Message = "Internal error occurred while authenticating" } :
                new AuthResponse {
                    Success = true,
                    Email = user.Email,
                    Roles = roles,
                    Token = jwtToken,
                    RefreshToken = refreshToken.Token,
                    RefreshTokenExpiration = refreshToken.ExpiredOn
                };
        }

        public async Task<AuthResponse> ReviveToken(string refreshToken)
        {
            bool validateTokenRequest = await tokenManagement.ValidateRefreshToken(refreshToken);
            if (!validateTokenRequest)
                return new AuthResponse { Message = "Invalid token" };

            string userId = await tokenManagement.GetUserIdByRefreshToken(refreshToken);
            AppUser? user = await userManagement.GetUserById(userId);
            var claims = await userManagement.GetUserClaims(user!.Email!);
            string newJwtToken = tokenManagement.GenerateToken(claims);
            var newRefreshToken = tokenManagement.GetRefreshToken();

            await tokenManagement.UpdateRefreshToken(userId, newRefreshToken.Token);
            return new AuthResponse {
                Success = true,
                Token = newJwtToken,
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.ExpiredOn
                
            };
        }
    }
}
