using BookingTourHutech.Models;

namespace BookingTourHutech.Repository
{
	public interface ISuplierRepository
	{
		Task<IEnumerable<Suplier>> GetAllAsync();
		Task<Suplier> GetByIdAsync(int id);
		Task AddAsync(Suplier product);
		Task UpdateAsync(Suplier product);
		Task DeleteAsync(int id);
	}
}
