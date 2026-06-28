using ExpenseTracker.Models;

namespace ExpenseTracker.Repositories.Interfaces;

public interface IExpenseRepository
{
    Task<List<Ausgabe>> GetAllAsync();

    Task<Ausgabe?> GetByIdAsync(int id);
    
    Task UpdateAsync(Ausgabe ausgabe);

    Task AddAsync(Ausgabe ausgabe);
    
    Task DeleteAsync(int id);
    
    Task<decimal> GetTotalAmountAsync();

    Task<decimal> GetTotalAmountForCurrentMonthAsync();

    Task<int> GetCountAsync();

    Task<decimal> GetAverageAmountAsync();

    Task<List<Ausgabe>> GetLatestAsync(int count);
    
    Task<Ausgabe?> GetLargestExpenseAsync();

    Task<(Kategorie? Kategorie, int Anzahl)> GetMostUsedCategoryAsync();
}