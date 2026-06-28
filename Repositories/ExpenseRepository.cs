using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

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
    
    public async Task<Ausgabe?> GetByIdAsync(int id)
    {
        return await _context.Ausgaben
            .Include(a => a.Kategorie)
            .Include(a => a.Zahlungsart)
            .FirstOrDefaultAsync(a => a.AusgabeID == id);
    }

    public async Task AddAsync(Ausgabe ausgabe)
    {
        var parameters = new[]
        {
            new SqlParameter("@Bezeichnung", ausgabe.Bezeichnung),
            new SqlParameter("@Betrag", ausgabe.Betrag),
            new SqlParameter("@Datum", ausgabe.Datum),
            new SqlParameter("@Beschreibung", (object?)ausgabe.Beschreibung ?? DBNull.Value),
            new SqlParameter("@KategorieID", ausgabe.KategorieID),
            new SqlParameter("@ZahlungsartID", ausgabe.ZahlungsartID)
        };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC dbo.sp_abbiit00_AusgabeErstellen @Bezeichnung, @Betrag, @Datum, @Beschreibung, @KategorieID, @ZahlungsartID",
            parameters);
    }
    
    public async Task UpdateAsync(Ausgabe ausgabe)
    {
        var existingAusgabe = await _context.Ausgaben
            .FirstOrDefaultAsync(a => a.AusgabeID == ausgabe.AusgabeID);

        if (existingAusgabe == null)
        {
            return;
        }

        existingAusgabe.Bezeichnung = ausgabe.Bezeichnung;
        existingAusgabe.Betrag = ausgabe.Betrag;
        existingAusgabe.Datum = ausgabe.Datum;
        existingAusgabe.Beschreibung = ausgabe.Beschreibung;
        existingAusgabe.KategorieID = ausgabe.KategorieID;
        existingAusgabe.ZahlungsartID = ausgabe.ZahlungsartID;

        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var ausgabe = await _context.Ausgaben
            .FirstOrDefaultAsync(a => a.AusgabeID == id);

        if (ausgabe == null)
        {
            return;
        }

        _context.Ausgaben.Remove(ausgabe);
        await _context.SaveChangesAsync();
    }
    
    public async Task<decimal> GetTotalAmountAsync()
    {
        return await _context.Ausgaben
            .SumAsync(a => a.Betrag);
    }

    public async Task<decimal> GetTotalAmountForCurrentMonthAsync()
    {
        var today = DateTime.Today;
        var startOfMonth = new DateTime(today.Year, today.Month, 1);
        var startOfNextMonth = startOfMonth.AddMonths(1);

        return await _context.Ausgaben
            .Where(a => a.Datum >= startOfMonth && a.Datum < startOfNextMonth)
            .SumAsync(a => a.Betrag);
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Ausgaben
            .CountAsync();
    }

    public async Task<decimal> GetAverageAmountAsync()
    {
        if (!await _context.Ausgaben.AnyAsync())
        {
            return 0;
        }

        return await _context.Ausgaben
            .AverageAsync(a => a.Betrag);
    }

    public async Task<List<Ausgabe>> GetLatestAsync(int count)
    {
        return await _context.Ausgaben
            .Include(a => a.Kategorie)
            .Include(a => a.Zahlungsart)
            .OrderByDescending(a => a.Datum)
            .ThenByDescending(a => a.ErstelltAm)
            .Take(count)
            .ToListAsync();
    }
    
    public async Task<Ausgabe?> GetLargestExpenseAsync()
    {
        return await _context.Ausgaben
            .Include(a => a.Kategorie)
            .Include(a => a.Zahlungsart)
            .OrderByDescending(a => a.Betrag)
            .FirstOrDefaultAsync();
    }
    
    public async Task<(Kategorie? Kategorie, int Anzahl)> GetMostUsedCategoryAsync()
    {
        var result = await _context.Ausgaben
            .GroupBy(a => a.KategorieID)
            .Select(g => new
            {
                KategorieID = g.Key,
                Anzahl = g.Count()
            })
            .OrderByDescending(g => g.Anzahl)
            .FirstOrDefaultAsync();

        if (result == null)
        {
            return (null, 0);
        }

        var kategorie = await _context.Kategorien
            .FirstOrDefaultAsync(k => k.KategorieID == result.KategorieID);

        return (kategorie, result.Anzahl);
    }
}