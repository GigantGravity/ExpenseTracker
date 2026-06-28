using ExpenseTracker.Models;

namespace ExpenseTracker.Repositories.Interfaces;

public interface IExpenseRepository
{
    Task<List<Ausgabe>> GetAllAsync();
}