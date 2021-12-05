using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniCRM.API.Models;

namespace MiniCRM.API.Data
{
    public class CRMRepository : ICRMRepository
    {
        private readonly DataContext _context;

        public CRMRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
             _context.Remove(entity);
        }

        public async Task<Customer> GetCustomer(int customerId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.id == customerId);
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var Customers = await _context.Customers.ToListAsync();
            return Customers;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}