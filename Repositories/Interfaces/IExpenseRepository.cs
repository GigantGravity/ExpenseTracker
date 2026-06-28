using ExpenseTracker.Models;

namespace ExpenseTracker.Repositories.Interfaces;

public interface IExpenseRepository
{
    Task<List<Ausgabe>> GetAllAsync();

    Task<Ausgabe?> GetByIdAsync(int id);
    
    Task UpdateAsync(Ausgabe ausgabe);

    Task AddAsync(Ausgabe ausgabe);
}