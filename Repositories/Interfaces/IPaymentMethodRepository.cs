using ExpenseTracker.Models;

namespace ExpenseTracker.Repositories.Interfaces;

public interface IPaymentMethodRepository
{
    Task<List<Zahlungsart>> GetAllAsync();
}