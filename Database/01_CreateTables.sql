USE SWB_DB2_Projekt;
GO

-- =============================================
-- Projekt: ExpenseTracker
-- Autor: abbiit00
-- Datei: 01_CreateTables.sql
-- Beschreibung:
-- Erstellt alle Tabellen für den ExpenseTracker.
-- =============================================

-- Alte Tabellen löschen
IF OBJECT_ID('dbo.abbiit00_AusgabeLog', 'U') IS NOT NULL
DROP TABLE dbo.abbiit00_AusgabeLog;
GO

IF OBJECT_ID('dbo.abbiit00_Ausgabe', 'U') IS NOT NULL
DROP TABLE dbo.abbiit00_Ausgabe;
GO

IF OBJECT_ID('dbo.abbiit00_Zahlungsart', 'U') IS NOT NULL
DROP TABLE dbo.abbiit00_Zahlungsart;
GO

IF OBJECT_ID('dbo.abbiit00_Kategorie', 'U') IS NOT NULL
DROP TABLE dbo.abbiit00_Kategorie;
GO


-- =============================================
-- Tabelle: abbiit00_Kategorie
-- =============================================
CREATE TABLE dbo.abbiit00_Kategorie (
                                        KategorieID INT IDENTITY(1,1) PRIMARY KEY,
                                        Name NVARCHAR(100) NOT NULL,
                                        Icon NVARCHAR(50) NULL,

                                        CONSTRAINT UQ_abbiit00_Kategorie_Name
                                            UNIQUE (Name)
);
GO


-- =============================================
-- Tabelle: abbiit00_Zahlungsart
-- =============================================
CREATE TABLE dbo.abbiit00_Zahlungsart (
                                          ZahlungsartID INT IDENTITY(1,1) PRIMARY KEY,
                                          Name NVARCHAR(50) NOT NULL,

                                          CONSTRAINT UQ_abbiit00_Zahlungsart_Name
                                              UNIQUE (Name)
);
GO


-- =============================================
-- Tabelle: abbiit00_Ausgabe
-- =============================================
CREATE TABLE dbo.abbiit00_Ausgabe (
                                      AusgabeID INT IDENTITY(1,1) PRIMARY KEY,
                                      Bezeichnung NVARCHAR(100) NOT NULL,
                                      Betrag DECIMAL(10,2) NOT NULL,
                                      Datum DATE NOT NULL,
                                      Beschreibung NVARCHAR(255) NULL,
                                      ErstelltAm DATETIME NOT NULL DEFAULT GETDATE(),

                                      KategorieID INT NOT NULL,
                                      ZahlungsartID INT NOT NULL,

                                      CONSTRAINT FK_abbiit00_Ausgabe_Kategorie
                                          FOREIGN KEY (KategorieID)
                                              REFERENCES dbo.abbiit00_Kategorie(KategorieID),

                                      CONSTRAINT FK_abbiit00_Ausgabe_Zahlungsart
                                          FOREIGN KEY (ZahlungsartID)
                                              REFERENCES dbo.abbiit00_Zahlungsart(ZahlungsartID),

                                      CONSTRAINT CK_abbiit00_Ausgabe_Betrag
                                          CHECK (Betrag > 0)
);
GO


-- =============================================
-- Tabelle: abbiit00_AusgabeLog
-- =============================================
CREATE TABLE dbo.abbiit00_AusgabeLog (
                                         LogID INT IDENTITY(1,1) PRIMARY KEY,

                                         AusgabeID INT NULL,
                                         Bezeichnung NVARCHAR(100) NULL,
                                         Betrag DECIMAL(10,2) NULL,
                                         Datum DATE NULL,
                                         KategorieID INT NULL,
                                         ZahlungsartID INT NULL,

                                         Aktion NVARCHAR(50) NOT NULL,
                                         Zeitpunkt DATETIME NOT NULL DEFAULT GETDATE(),
                                         Hostname NVARCHAR(100) NULL,
                                         Benutzername NVARCHAR(100) NULL
);
GO