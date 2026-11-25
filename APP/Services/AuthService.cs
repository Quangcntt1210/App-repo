using APP.Entities;
using APP.DTOs;
using Microsoft.AspNetCore.Identity;

namespace APP.Services
{
    public class AuthService
    {
        private readonly List<Account> _accounts;

        private readonly PasswordHasher<Account> _passwordHasher = new();

        public AuthService()
        {
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                DisplayName = ".NET admin",
                Role = "User"
            };

            account.PasswordHash = _passwordHasher.HashPassword(
                account,
                "Admin123!"
            );

            _accounts = new List<Account> { account };
        }

        public LoginResponse? Login(LoginRequest request)
        {
            // Tìm tài khoản theo username
            var account = _accounts.FirstOrDefault(a =>
                a.Username.Equals(
                    request.Username,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (account == null)
            {
                return null;
            }

            // Xác minh mật khẩu với PasswordHash
            var result = _passwordHasher.VerifyHashedPassword(
                account,
                account.PasswordHash,
                request.Password
            );

            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            // Chuyển Account thành LoginResponse
            return new LoginResponse
            {
                Id = account.Id,
                Username = account.Username,
                DisplayName = account.DisplayName,
                Role = account.Role
            };
        }
    }
}