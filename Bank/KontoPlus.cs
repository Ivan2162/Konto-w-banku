using System;

namespace Bank;

public class KontoPlus : Konto
{
    private decimal limit;
    private bool limitWykorzystany = false;

    public decimal Limit
    {
        get => limit;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            limit = value;
        }
    }

    public override decimal Bilans => limitWykorzystany ? bilans : bilans + limit;

    public KontoPlus(string klient, decimal bilansNaStart = 0, decimal limit = 0)
        : base(klient, bilansNaStart)
    {
        Limit = limit;
    }

    public override void Wplata(decimal kwota)
    {
        if (zablokowane && bilans >= 0) 
            throw new InvalidOperationException("Konto jest zablokowane.");
            
        if (kwota <= 0) 
            throw new ArgumentOutOfRangeException(nameof(kwota));

        bilans += kwota;

        if (bilans > 0 && limitWykorzystany)
        {
            limitWykorzystany = false;
            zablokowane = false;
        }
    }

    public override void Wyplata(decimal kwota)
    {
        if (zablokowane) 
            throw new InvalidOperationException("Konto jest zablokowane.");
        if (kwota <= 0) 
            throw new ArgumentOutOfRangeException(nameof(kwota));

        if (kwota > bilans)
        {
            if (limitWykorzystany)
                throw new InvalidOperationException("Limit wykorzystany.");
            
            if (kwota > bilans + limit)
                throw new InvalidOperationException("Brak środków.");

            bilans -= kwota;
            limitWykorzystany = true;
            zablokowane = true;
        }
        else
        {
            bilans -= kwota;
        }
    }
}