using ExpenseTracker.Models;

namespace ExpenseTracker.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<List<Kategorie>> GetAllAsync();
}