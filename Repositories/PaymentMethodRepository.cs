using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repositories;

public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly ExpenseTrackerContext _context;

    public PaymentMethodRepository(ExpenseTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<Zahlungsart>> GetAllAsync()
    {
        return await _context.Zahlungsarten
            .OrderBy(z => z.Name)
            .ToListAsync();
    }
}