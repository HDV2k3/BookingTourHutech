using BookingTourHutech.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingTourHutech.Repository
{
	public class EFTourRepository: ITourRepository
	{
		private readonly BookingTourDbContext _context;
		public EFTourRepository(BookingTourDbContext context)
		{
			_context = context;
		}
        public async Task<IEnumerable<Tour>> GetAllAsync()
        {
            return await _context.Tours.Include(p => p.CategoryTourIdNavigation)
                                       .ToListAsync();
        }
        public async Task<Tour> GetByIdAsync(int id)
		{
           
            return await _context.Tours.Include(p => p.CategoryTourIdNavigation).FirstOrDefaultAsync(p => p.TourId == id);
        }
		public async Task AddAsync(Tour product)
		{
			_context.Tours.AddAsync(product);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(Tour product)
		{
			_context.Tours.Update(product);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(int id)
		{
			var product = await _context.Tours.FindAsync(id);
			_context.Tours.Remove(product);
			await _context.SaveChangesAsync();
		}
        public async Task<IEnumerable<Tour>> SearchAsync(string keyword)
        {
            return await _context.Tours
                .Include(p => p.CategoryTourIdNavigation)
                .Where(t => t.TourName.Contains(keyword) || t.TourDescription.Contains(keyword))
                .ToListAsync();
        }   
      
    }
}
