using BookingTourHutech.Models;
using System.Data.Entity;

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
			return await _context.Tours.Include(p => p.TourId).ToListAsync();
		}
		public async Task<Tour> GetByIdAsync(int id)
		{
			// return await _context.Products.FindAsync(id);
			// lấy thông tin kèm theo category
			return await _context.Tours.Include(p => p.TourId).FirstOrDefaultAsync(p => p.TourId == id);
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

		Task<IEnumerable<Tour>> ITourRepository.GetAllAsync()
		{
			throw new NotImplementedException();
		}

		Task<Tour> ITourRepository.GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
