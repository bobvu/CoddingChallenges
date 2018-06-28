using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodingChallenges.Domains.Employees;

namespace CodingChallenges.DataAcess.DbContexts
{

    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly SqlContext _context;
        private readonly ILogger _logger;
        //private readonly IAccountService _accountManager;

        public DatabaseInitializer(SqlContext context, ILogger<DatabaseInitializer> logger)
        {
            _context = context;
            _logger = logger;
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
        }
    }
}
