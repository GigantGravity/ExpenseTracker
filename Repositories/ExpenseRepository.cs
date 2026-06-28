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
    
    public async Task<Ausgabe?> GetByIdAsync(int id)
    {
        return await _context.Ausgaben
            .Include(a => a.Kategorie)
            .Include(a => a.Zahlungsart)
            .FirstOrDefaultAsync(a => a.AusgabeID == id);
    }

    public async Task AddAsync(Ausgabe ausgabe)
    {
        ausgabe.ErstelltAm = DateTime.Now;

        _context.Ausgaben.Add(ausgabe);
        await _context.SaveChangesAsync();
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
}