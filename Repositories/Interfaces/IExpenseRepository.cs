using ExpenseTracker.Models;

namespace ExpenseTracker.Repositories.Interfaces;

public interface IExpenseRepository
{
    Task<List<Ausgabe>> GetAllAsync();

    Task<Ausgabe?> GetByIdAsync(int id);

    Task AddAsync(Ausgabe ausgabe);
}