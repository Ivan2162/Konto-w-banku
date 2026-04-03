using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Bank;

namespace Bank.Tests;

[TestClass]
public class KontoTests
{
    [TestMethod]
    public void Constructor_ValidArguments_SetsProperties()
    {
        var konto = new Konto("Jan Kowalski", 100m);
        Assert.AreEqual("Jan Kowalski", konto.Nazwa);
        Assert.AreEqual(100m, konto.Bilans);
        Assert.IsFalse(konto.Zablokowane);
    }

    [TestMethod]
    public void Constructor_EmptyName_ThrowsArgumentException()
    {
        Assert.ThrowsException<ArgumentException>(() => new Konto(""));
    }

    [TestMethod]
    public void Wplata_ValidAmount_IncreasesBalance()
    {
        var konto = new Konto("Test", 100m);
        konto.Wplata(50m);
        Assert.AreEqual(150m, konto.Bilans);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-10)]
    public void Wplata_InvalidAmount_ThrowsArgumentOutOfRangeException(double amount)
    {
        var konto = new Konto("Test");
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => konto.Wplata((decimal)amount));
    }

    [TestMethod]
    public void Wplata_BlockedAccount_ThrowsInvalidOperationException()
    {
        var konto = new Konto("Test");
        konto.BlokujKonto();
        Assert.ThrowsException<InvalidOperationException>(() => konto.Wplata(50m));
    }

    [TestMethod]
    public void Wyplata_ValidAmount_DecreasesBalance()
    {
        var konto = new Konto("Test", 100m);
        konto.Wyplata(40m);
        Assert.AreEqual(60m, konto.Bilans);
    }

    [TestMethod]
    public void Wyplata_AmountGreaterThanBalance_ThrowsInvalidOperationException()
    {
        var konto = new Konto("Test", 100m);
        Assert.ThrowsException<InvalidOperationException>(() => konto.Wyplata(150m));
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-10)]
    public void Wyplata_InvalidAmount_ThrowsArgumentOutOfRangeException(double amount)
    {
        var konto = new Konto("Test", 100m);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => konto.Wyplata((decimal)amount));
    }

    [TestMethod]
    public void Wyplata_BlockedAccount_ThrowsInvalidOperationException()
    {
        var konto = new Konto("Test", 100m);
        konto.BlokujKonto();
        Assert.ThrowsException<InvalidOperationException>(() => konto.Wyplata(50m));
    }

    [TestMethod]
    public void BlokujIOdblokujKonto_ChangesZablokowaneState()
    {
        var konto = new Konto("Test");
        konto.BlokujKonto();
        Assert.IsTrue(konto.Zablokowane);
        konto.OdblokujKonto();
        Assert.IsFalse(konto.Zablokowane);
    }
}