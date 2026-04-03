using System;
using Bank;

Console.WriteLine("=== Symulacja Banku ===");

Console.WriteLine("\n--- Konto Standardowe ---");
Konto zwykleKonto = new Konto("Jan Kowalski", 500m);
Console.WriteLine($"Utworzono konto dla: {zwykleKonto.Nazwa}, Bilans: {zwykleKonto.Bilans} PLN");

zwykleKonto.Wplata(200m);
Console.WriteLine($"Po wpłacie 200 PLN, Bilans: {zwykleKonto.Bilans} PLN");

try
{
    zwykleKonto.Wyplata(800m);
}
catch (Exception ex)
{
    Console.WriteLine($"Próba wypłaty 800 PLN zakończona błędem: {ex.Message}");
}

Console.WriteLine("\n--- Konto Plus (Dziedziczenie) ---");
KontoPlus kontoPlus = new KontoPlus("Anna Nowak", 300m, 500m);
Console.WriteLine($"Utworzono KontoPlus dla: {kontoPlus.Nazwa}, Dostępne środki (z limitem): {kontoPlus.Bilans} PLN");

kontoPlus.Wyplata(400m);
Console.WriteLine($"Po wypłacie 400 PLN. Bilans konta: {kontoPlus.Bilans} PLN. Zablokowane: {kontoPlus.Zablokowane}");

try
{
    kontoPlus.Wyplata(50m);
}
catch (Exception ex)
{
     Console.WriteLine($"Próba kolejnej wypłaty zakończona błędem: {ex.Message}");
}

kontoPlus.Wplata(150m);
Console.WriteLine($"Po wpłacie 150 PLN. Bilans konta: {kontoPlus.Bilans} PLN. Zablokowane: {kontoPlus.Zablokowane}");

Console.WriteLine("\n--- Konto Limit (Delegacja) ---");
KontoLimit kontoLimit = new KontoLimit("Piotr Wiśniewski", 200m, 300m);
Console.WriteLine($"Utworzono KontoLimit dla: {kontoLimit.Nazwa}, Dostępne środki (z limitem): {kontoLimit.Bilans} PLN");

kontoLimit.Wyplata(450m);
Console.WriteLine($"Po wypłacie 450 PLN. Bilans konta: {kontoLimit.Bilans} PLN. Zablokowane: {kontoLimit.Zablokowane}");

kontoLimit.Wplata(300m);
Console.WriteLine($"Po wpłacie 300 PLN. Bilans konta: {kontoLimit.Bilans} PLN. Zablokowane: {kontoLimit.Zablokowane}");

Console.WriteLine("\n=== Symulacja zakończona ===");