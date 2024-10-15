using Microsoft.EntityFrameworkCore;
using Project_WEBSITE.Models;

namespace Project_WEBSITE.Repositories
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public EFOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.Include(o => o.ApplicationUser).OrderByDescending(p => p.Id).ToListAsync();
        }
        public async Task<Order> GetByIdAsync(int Id)
        {
            return await _context.Orders.Include(o => o.ApplicationUser).Include(o => o.OrderDetails).FirstOrDefaultAsync(p => p.Id == Id);
        }
        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
