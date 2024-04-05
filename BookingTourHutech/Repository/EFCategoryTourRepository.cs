using BookingTourHutech.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingTourHutech.Repository
{
	public class EFCategoryTourRepository
	{
		private readonly BookingTourDbContext _context;
		public EFCategoryTourRepository(BookingTourDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<CategoryTour>> GetAllAsync()
		{
			return await _context.CategoryTours.Include(p => p.Tours).ToListAsync();
		}
		public async Task<CategoryTour> GetByIdAsync(int id)
		{
			return await _context.CategoryTours.FindAsync(id);
		}
		public async Task AddAsync(CategoryTour category)
		{
			_context.CategoryTours.Add(category);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(CategoryTour category)
		{
			_context.CategoryTours.Update(category);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(int id)
		{
			var category = await _context.CategoryTours.FindAsync(id);
			_context.CategoryTours.Remove(category);
			await _context.SaveChangesAsync();
		}
	}
}
