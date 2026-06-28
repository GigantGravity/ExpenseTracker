USE SWB_DB2_Projekt;
GO

-- =============================================
-- Projekt: ExpenseTracker
-- Autor: abbiit00
-- Datei: 02_InsertDemoData.sql
-- Beschreibung:
-- Fügt Beispieldaten in die Tabellen ein.
-- =============================================

-- =============================================
-- Kategorien
-- =============================================
INSERT INTO dbo.abbiit00_Kategorie (Name, Icon)
VALUES
('Lebensmittel', 'cart'),
('Miete', 'house'),
('Freizeit', 'controller'),
('Auto', 'car-front'),
('Shopping', 'bag'),
('Gesundheit', 'heart-pulse'),
('Restaurant', 'cup-hot'),
('Haushalt', 'house-gear');
GO

-- =============================================
-- Zahlungsarten
-- =============================================
INSERT INTO dbo.abbiit00_Zahlungsart (Name)
VALUES
('Bar'),
('EC-Karte'),
('Kreditkarte'),
('PayPal'),
('Überweisung');
GO

-- =============================================
-- Ausgaben
-- =============================================
INSERT INTO dbo.abbiit00_Ausgabe
(Bezeichnung, Betrag, Datum, Beschreibung, KategorieID, ZahlungsartID)
VALUES
(
    'Edeka',
    48.35,
    '2026-04-02',
    'Wocheneinkauf',
    (SELECT KategorieID FROM dbo.abbiit00_Kategorie WHERE Name = 'Lebensmittel'),
    (SELECT ZahlungsartID FROM dbo.abbiit00_Zahlungsart WHERE Name = 'EC-Karte')
),
(
    'Lidl',
    26.80,
    '2026-04-05',
    'Lebensmittel',
    (SELECT KategorieID FROM dbo.abbiit00_Kategorie WHERE Name = 'Lebensmittel'),
    (SELECT ZahlungsartID FROM dbo.abbiit00_Zahlungsart WHERE Name = 'EC-Karte')
),
(
    'Netflix',
    13.99,
    '2026-04-01',
    'Monatsabo',
    (SELECT KategorieID FROM dbo.abbiit00_Kategorie WHERE Name = 'Freizeit'),
    (SELECT ZahlungsartID FROM dbo.abbiit00_Zahlungsart WHERE Name = 'PayPal')
),
(
    'Shell',
    72.40,
    '2026-04-08',
    'Tanken',
    (SELECT KategorieID FROM dbo.abbiit00_Kategorie WHERE Name = 'Auto'),
    (SELECT ZahlungsartID FROM dbo.abbiit00_Zahlungsart WHERE Name = 'Kreditkarte')
),
(
    'Miete April',
    850.00,
    '2026-04-01',
    'Monatsmiete',
    (SELECT KategorieID FROM dbo.abbiit00_Kategorie WHERE Name = 'Miete'),
    (SELECT ZahlungsartID FROM dbo.abbiit00_Zahlungsart WHERE Name = 'Überweisung')
),
(
    'Amazon',
    39.99,
    '2026-04-10',
    'Online-Einkauf',
    (SELECT KategorieID FROM dbo.abbiit00_Kategorie WHERE Name = 'Shopping'),
    (SELECT ZahlungsartID FROM dbo.abbiit00_Zahlungsart WHERE Name = 'Kreditkarte')
),
(
    'dm',
    18.75,
    '2026-04-11',
    'Drogerie',
    (SELECT KategorieID FROM dbo.abbiit00_Kategorie WHERE Name = 'Gesundheit'),
    (SELECT ZahlungsartID FROM dbo.abbiit00_Zahlungsart WHERE Name = 'EC-Karte')
),
(
    'Vapiano',
    27.50,
    '2026-04-12',
    'Restaurantbesuch',
    (SELECT KategorieID FROM dbo.abbiit00_Kategorie WHERE Name = 'Restaurant'),
    (SELECT ZahlungsartID FROM dbo.abbiit00_Zahlungsart WHERE Name = 'Bar')
),
(
    'IKEA',
    64.90,
    '2026-04-15',
    'Haushaltsartikel',
    (SELECT KategorieID FROM dbo.abbiit00_Kategorie WHERE Name = 'Haushalt'),
    (SELECT ZahlungsartID FROM dbo.abbiit00_Zahlungsart WHERE Name = 'EC-Karte')
),
(
    'OBI',
    22.30,
    '2026-04-17',
    'Werkzeug und Zubehör',
    (SELECT KategorieID FROM dbo.abbiit00_Kategorie WHERE Name = 'Haushalt'),
    (SELECT ZahlungsartID FROM dbo.abbiit00_Zahlungsart WHERE Name = 'Bar')
),
(
    'Spotify',
    10.99,
    '2026-04-18',
    'Monatsabo',
    (SELECT KategorieID FROM dbo.abbiit00_Kategorie WHERE Name = 'Freizeit'),
    (SELECT ZahlungsartID FROM dbo.abbiit00_Zahlungsart WHERE Name = 'PayPal')
),
(
    'Aral',
    58.10,
    '2026-04-20',
    'Tanken',
    (SELECT KategorieID FROM dbo.abbiit00_Kategorie WHERE Name = 'Auto'),
    (SELECT ZahlungsartID FROM dbo.abbiit00_Zahlungsart WHERE Name = 'EC-Karte')
);
GO