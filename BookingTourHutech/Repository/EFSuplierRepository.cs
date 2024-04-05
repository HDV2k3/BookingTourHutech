using BookingTourHutech.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingTourHutech.Repository
{
	public class EFSuplierRepository
	{
		private readonly BookingTourDbContext _context;
		public EFSuplierRepository(BookingTourDbContext context)
		{
			_context = context;
		}
		public async Task AddAsync(Suplier nhaCungCap)
		{
			_context.Supliers.Add(nhaCungCap);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(string id)
		{
			var nhacungcap = await _context.Supliers.FindAsync(id);
			_context.Supliers.Remove(nhacungcap);
			await _context.SaveChangesAsync();

		}



		public async Task<IEnumerable<Suplier>> GetAllAsync()
		{
			return await _context.Supliers.ToListAsync();
		}

		public async Task<Suplier> GetByIdAsync(string id)
		{
			return await _context.Supliers.Include(p => p.SuplierId).FirstOrDefaultAsync(p => p.SuplierId == id);
		}

		public async Task UpdateAsync(Suplier nhaCungCap)
		{
			_context.Supliers.Update(nhaCungCap);
			await _context.SaveChangesAsync();
		}
	}
}
