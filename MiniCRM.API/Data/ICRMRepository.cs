using System.Collections.Generic;
using System.Threading.Tasks;
using MiniCRM.API.Models;

namespace MiniCRM.API.Data
{
    public interface ICRMRepository
    {
         void Add<T>(T entity) where T:class;
         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();
         Task<IEnumerable<Customer>> GetCustomers();
         Task<Customer> GetCustomer(int customerId);
    }
}