using BookingTourHutech.Models;

namespace BookingTourHutech.Repository
{
	public interface ICategoryTourRepository
	{
		Task<IEnumerable<CategoryTour>> GetAllAsync();
		Task<CategoryTour> GetByIdAsync(int id);
		Task AddAsync(CategoryTour category);
		Task UpdateAsync(CategoryTour category);
		Task DeleteAsync(int id);
	}
}
