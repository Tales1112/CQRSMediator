using CQRSMediator.Domain.Entities;
using CQRSMediator.Domain.Interfaces;
using CQRSMediator.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSMediator.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        protected readonly ApplicationDbContext db;

        public CustomerRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public void Add(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await db.Customers.ToListAsync();
        }

        public async Task<Customer> GetByEmail(string email)
        {
            return await db.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Customer> GetById(int id)
        {
            return await db.Customers.FindAsync(id);
        }

        public void Remove(Customer customer)
        {
            db.Customers.Remove(customer);
            db.SaveChanges();
        }

        public void Update(Customer customer)
        {
            db.Customers.Update(customer);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
