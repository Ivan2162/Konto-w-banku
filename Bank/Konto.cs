using System;

namespace Bank;

public class Konto
{
    protected string klient;
    protected decimal bilans;
    protected bool zablokowane = false;

    public string Nazwa => klient;
    public virtual decimal Bilans => bilans;
    public bool Zablokowane => zablokowane;

    protected Konto() 
    { 
        klient = string.Empty;
    }

    public Konto(string klient, decimal bilansNaStart = 0)
    {
        if (string.IsNullOrWhiteSpace(klient))
            throw new ArgumentException("Nazwa klienta nie może być pusta.", nameof(klient));
            
        this.klient = klient;
        this.bilans = bilansNaStart;
    }

    public virtual void Wplata(decimal kwota)
    {
        if (zablokowane) 
            throw new InvalidOperationException("Konto jest zablokowane.");
        if (kwota <= 0) 
            throw new ArgumentOutOfRangeException(nameof(kwota));
            
        bilans += kwota;
    }

    public virtual void Wyplata(decimal kwota)
    {
        if (zablokowane) 
            throw new InvalidOperationException("Konto jest zablokowane.");
        if (kwota <= 0) 
            throw new ArgumentOutOfRangeException(nameof(kwota));
        if (kwota > bilans) 
            throw new InvalidOperationException("Brak środków.");
            
        bilans -= kwota;
    }

    public void BlokujKonto() => zablokowane = true;
    public void OdblokujKonto() => zablokowane = false;
}