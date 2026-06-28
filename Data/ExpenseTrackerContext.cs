using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data;

public class ExpenseTrackerContext : DbContext
{
    public ExpenseTrackerContext(DbContextOptions<ExpenseTrackerContext> options)
        : base(options)
    {
    }

    public DbSet<Kategorie> Kategorien { get; set; }

    public DbSet<Zahlungsart> Zahlungsarten { get; set; }

    public DbSet<Ausgabe> Ausgaben { get; set; }

    public DbSet<AusgabeLog> AusgabeLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Kategorie>()
            .ToTable("abbiit00_Kategorie")
            .HasKey(k => k.KategorieID);

        modelBuilder.Entity<Zahlungsart>()
            .ToTable("abbiit00_Zahlungsart")
            .HasKey(z => z.ZahlungsartID);

        modelBuilder.Entity<Ausgabe>()
            .ToTable("abbiit00_Ausgabe")
            .HasKey(a => a.AusgabeID);

        modelBuilder.Entity<AusgabeLog>()
            .ToTable("abbiit00_AusgabeLog")
            .HasKey(l => l.LogID);
    }
}