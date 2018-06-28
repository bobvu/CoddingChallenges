using CoddingChallenges.Common;
using CodingChallenges.DataAcess.DbContexts;
using CodingChallenges.Domains.Employees;
using CodingChallenges.Domains.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenges.Services
{
    public interface IDatabaseInitializerService
    {
        Task SeedAsync();
    }
    public class DatabaseInitializerService : IDatabaseInitializerService
    {
        private readonly SqlContext _context;
        private readonly ILogger _logger;
        private readonly IAccountService _accountManager;

        public DatabaseInitializerService(SqlContext context, IAccountService accountManager, ILogger<DatabaseInitializerService> logger)
        {
            _context = context;
            _logger = logger;
            _accountManager = accountManager;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);

            if (!await _context.Employees.AnyAsync())
            {
                _logger.LogInformation("Seeding initial data");

                Employee emp1 = new Employee("Allan", 100, 150, null);
                Employee emp2 = new Employee("Martin", 220, 100, null);
                Employee emp3 = new Employee("Jamie", 150, null, null);
                Employee emp4 = new Employee("Alex", 275, 100, null);
                Employee emp5 = new Employee("Steve", 400, 150, null);
                Employee emp6 = new Employee("David", 190, 400, null);


                _context.Employees.Add(emp1);
                _context.Employees.Add(emp2);
                _context.Employees.Add(emp3);
                _context.Employees.Add(emp4);
                _context.Employees.Add(emp5);
                _context.Employees.Add(emp6);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Seeding initial data completed");
            }
            if (!await _context.Users.AnyAsync())
            {
                _logger.LogInformation("Generating inbuilt accounts");

                const string adminRoleName = "administrator";
                const string userRoleName = "user";

                await EnsureRoleAsync(adminRoleName, "Default administrator", ApplicationPermissions.GetAllPermissionValues());
                await EnsureRoleAsync(userRoleName, "Default user", new string[] { });

                await CreateUserAsync("admin", "tempP@ss123", "Inbuilt Administrator", "admin@itmb.com.au", "+61433426868", new string[] { adminRoleName });
                await CreateUserAsync("user", "tempP@ss123", "Inbuilt Standard User", "user@itmb.com.au", "+61433426868", new string[] { userRoleName });

                _logger.LogInformation("Inbuilt account generation completed");
            }
        }
        private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
        {
            if ((await _accountManager.GetRoleByNameAsync(roleName)) == null)
            {
                Roles applicationRole = new Roles(roleName, description);

                var result = await this._accountManager.CreateRoleAsync(applicationRole, claims);

                if (!result.Item1)
                    throw new Exception($"Seeding \"{description}\" role failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");
            }
        }

        private async Task<User> CreateUserAsync(string userName, string password, string fullName, string email, string phoneNumber, string[] roles)
        {
            User applicationUser = new User
            {
                UserName = userName,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsEnabled = true
            };

            var result = await _accountManager.CreateUserAsync(applicationUser, roles, password);

            if (!result.Item1)
                throw new Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");


            return applicationUser;
        }
    }
}

