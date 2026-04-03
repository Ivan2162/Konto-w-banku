using System;

namespace Bank;

public class KontoLimit
{
    private Konto _konto;
    private decimal _limit;
    private bool _limitWykorzystany = false;
    private decimal _zadluzenie = 0;

    public string Nazwa => _konto.Nazwa;
    public decimal Bilans => _limitWykorzystany ? -_zadluzenie : _konto.Bilans + _limit;
    public bool Zablokowane => _konto.Zablokowane;

    public decimal Limit
    {
        get => _limit;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _limit = value;
        }
    }

    public KontoLimit(string klient, decimal bilansNaStart = 0, decimal limit = 0)
    {
        _konto = new Konto(klient, bilansNaStart);
        Limit = limit;
    }

    public void Wplata(decimal kwota)
    {
        if (_konto.Zablokowane && !_limitWykorzystany)
            throw new InvalidOperationException("Konto jest zablokowane.");

        if (kwota <= 0)
            throw new ArgumentOutOfRangeException(nameof(kwota));

        if (_limitWykorzystany)
        {
            if (kwota > _zadluzenie)
            {
                decimal reszta = kwota - _zadluzenie;
                _zadluzenie = 0;
                _limitWykorzystany = false;
                _konto.OdblokujKonto();
                _konto.Wplata(reszta);
            }
            else
            {
                _zadluzenie -= kwota;
            }
        }
        else
        {
            _konto.Wplata(kwota);
        }
    }

    public void Wyplata(decimal kwota)
    {
        if (_konto.Zablokowane)
            throw new InvalidOperationException("Konto jest zablokowane.");
            
        if (kwota <= 0)
            throw new ArgumentOutOfRangeException(nameof(kwota));

        if (kwota > _konto.Bilans)
        {
            if (_limitWykorzystany)
                throw new InvalidOperationException("Limit wykorzystany.");

            if (kwota > _konto.Bilans + _limit)
                throw new InvalidOperationException("Brak środków.");

            _zadluzenie = kwota - _konto.Bilans;
            
            if (_konto.Bilans > 0)
                _konto.Wyplata(_konto.Bilans); 
                
            _limitWykorzystany = true;
            _konto.BlokujKonto();
        }
        else
        {
            _konto.Wyplata(kwota);
        }
    }

    public void BlokujKonto() => _konto.BlokujKonto();
    public void OdblokujKonto() => _konto.OdblokujKonto();
}