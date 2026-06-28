using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ExpenseTrackerContext _context;

    public CategoryRepository(ExpenseTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<Kategorie>> GetAllAsync()
    {
        return await _context.Kategorien
            .OrderBy(k => k.Name)
            .ToListAsync();
    }
}