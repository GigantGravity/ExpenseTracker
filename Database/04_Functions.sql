USE SWB_DB2_Projekt;
GO

-- =============================================
-- Projekt: ExpenseTracker
-- Autor: abbiit00
-- Datei: 04_Functions.sql
-- Beschreibung:
-- Berechnet die Gesamtausgaben eines Monats.
-- =============================================

IF OBJECT_ID('dbo.fn_abbiit00_Monatsausgaben', 'FN') IS NOT NULL
DROP FUNCTION dbo.fn_abbiit00_Monatsausgaben;
GO

CREATE FUNCTION dbo.fn_abbiit00_Monatsausgaben
(
    @Jahr INT,
    @Monat INT
)
    RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @Gesamt DECIMAL(10,2);

SELECT @Gesamt = ISNULL(SUM(Betrag), 0)
FROM dbo.abbiit00_Ausgabe
WHERE YEAR(Datum) = @Jahr
  AND MONTH(Datum) = @Monat;

RETURN @Gesamt;
END;
GO