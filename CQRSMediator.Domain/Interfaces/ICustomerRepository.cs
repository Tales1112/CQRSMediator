using CQRSMediator.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSMediator.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetById(int id);
        Task<Customer> GetByEmail(string email);
        Task<IEnumerable<Customer>> GetAll();

        void Add(Customer customer);
        void Update(Customer customer);
        void Remove(Customer customer);
    }
}
