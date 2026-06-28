using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly ExpenseTrackerContext _context;

    public ExpenseRepository(ExpenseTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<Ausgabe>> GetAllAsync()
    {
        return await _context.Ausgaben
            .Include(a => a.Kategorie)
            .Include(a => a.Zahlungsart)
            .OrderByDescending(a => a.Datum)
            .ToListAsync();
    }
}