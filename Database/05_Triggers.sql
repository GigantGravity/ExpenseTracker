USE SWB_DB2_Projekt;
GO

-- =============================================
-- Projekt: ExpenseTracker
-- Autor: abbiit00
-- Datei: 05_Triggers.sql
-- Beschreibung:
-- Erstellt Trigger zur Protokollierung von Änderungen an Ausgaben.
-- =============================================

IF OBJECT_ID('dbo.trg_abbiit00_Ausgabe_Insert', 'TR') IS NOT NULL
DROP TRIGGER dbo.trg_abbiit00_Ausgabe_Insert;
GO

CREATE TRIGGER dbo.trg_abbiit00_Ausgabe_Insert
    ON dbo.abbiit00_Ausgabe
    AFTER INSERT
AS
BEGIN
INSERT INTO dbo.abbiit00_AusgabeLog
(
    AusgabeID,
    Bezeichnung,
    Betrag,
    Datum,
    KategorieID,
    ZahlungsartID,
    Aktion,
    Zeitpunkt,
    Hostname,
    Benutzername
)
SELECT
    i.AusgabeID,
    i.Bezeichnung,
    i.Betrag,
    i.Datum,
    i.KategorieID,
    i.ZahlungsartID,
    'INSERT',
    GETDATE(),
    HOST_NAME(),
    SUSER_NAME()
FROM inserted i;
END;
GO


IF OBJECT_ID('dbo.trg_abbiit00_Ausgabe_Update', 'TR') IS NOT NULL
DROP TRIGGER dbo.trg_abbiit00_Ausgabe_Update;
GO

CREATE TRIGGER dbo.trg_abbiit00_Ausgabe_Update
    ON dbo.abbiit00_Ausgabe
    AFTER UPDATE
              AS
BEGIN
INSERT INTO dbo.abbiit00_AusgabeLog
(
    AusgabeID,
    Bezeichnung,
    Betrag,
    Datum,
    KategorieID,
    ZahlungsartID,
    Aktion,
    Zeitpunkt,
    Hostname,
    Benutzername
)
SELECT
    i.AusgabeID,
    i.Bezeichnung,
    i.Betrag,
    i.Datum,
    i.KategorieID,
    i.ZahlungsartID,
    'UPDATE',
    GETDATE(),
    HOST_NAME(),
    SUSER_NAME()
FROM inserted i;
END;
GO


IF OBJECT_ID('dbo.trg_abbiit00_Ausgabe_Delete', 'TR') IS NOT NULL
DROP TRIGGER dbo.trg_abbiit00_Ausgabe_Delete;
GO

CREATE TRIGGER dbo.trg_abbiit00_Ausgabe_Delete
    ON dbo.abbiit00_Ausgabe
    AFTER DELETE
AS
BEGIN
INSERT INTO dbo.abbiit00_AusgabeLog
(
    AusgabeID,
    Bezeichnung,
    Betrag,
    Datum,
    KategorieID,
    ZahlungsartID,
    Aktion,
    Zeitpunkt,
    Hostname,
    Benutzername
)
SELECT
    d.AusgabeID,
    d.Bezeichnung,
    d.Betrag,
    d.Datum,
    d.KategorieID,
    d.ZahlungsartID,
    'DELETE',
    GETDATE(),
    HOST_NAME(),
    SUSER_NAME()
FROM deleted d;
END;
GO