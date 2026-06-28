USE SWB_DB2_Projekt;
GO

-- =============================================
-- Projekt: ExpenseTracker
-- Autor: abbiit00
-- Datei: 03_StoredProcedures.sql
-- Beschreibung:
-- Erstellt Stored Procedures für den ExpenseTracker.
-- =============================================

IF OBJECT_ID('dbo.sp_abbiit00_AusgabeErstellen', 'P') IS NOT NULL
DROP PROCEDURE dbo.sp_abbiit00_AusgabeErstellen;
GO

CREATE PROCEDURE dbo.sp_abbiit00_AusgabeErstellen
    @Bezeichnung NVARCHAR(100),
    @Betrag DECIMAL(10,2),
    @Datum DATE,
    @Beschreibung NVARCHAR(255) = NULL,
    @KategorieID INT,
    @ZahlungsartID INT
AS
BEGIN
    SET NOCOUNT ON;

INSERT INTO dbo.abbiit00_Ausgabe
(
    Bezeichnung,
    Betrag,
    Datum,
    Beschreibung,
    KategorieID,
    ZahlungsartID
)
VALUES
    (
        @Bezeichnung,
        @Betrag,
        @Datum,
        @Beschreibung,
        @KategorieID,
        @ZahlungsartID
    );
END;
GO