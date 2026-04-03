using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Bank;

namespace Bank.Tests;

[TestClass]
public class KontoPlusTests
{
    [TestMethod]
    public void Constructor_SetsProperties()
    {
        var konto = new KontoPlus("Test", 100m, 50m);
        Assert.AreEqual(150m, konto.Bilans); 
        Assert.AreEqual(50m, konto.Limit);
    }

    [TestMethod]
    public void Wyplata_WithinBalance_DoesNotUseLimit()
    {
        var konto = new KontoPlus("Test", 100m, 50m);
        konto.Wyplata(40m);
        Assert.AreEqual(110m, konto.Bilans); 
        Assert.IsFalse(konto.Zablokowane);
    }

    [TestMethod]
    public void Wyplata_UsesLimit_BlocksAccount()
    {
        var konto = new KontoPlus("Test", 100m, 50m);
        konto.Wyplata(120m); 
        Assert.AreEqual(-20m, konto.Bilans);
        Assert.IsTrue(konto.Zablokowane);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Wyplata_ExceedsLimit_ThrowsException()
    {
        var konto = new KontoPlus("Test", 100m, 50m);
        konto.Wyplata(160m);
    }

    [TestMethod]
    public void Wplata_CoversDebt_Unblocks()
    {
        var konto = new KontoPlus("Test", 100m, 50m);
        konto.Wyplata(120m); 
        konto.Wplata(30m); 
        Assert.IsFalse(konto.Zablokowane);
        Assert.AreEqual(60m, konto.Bilans); 
    }

    [TestMethod]
    public void Wplata_PartialDebt_RemainsBlocked()
    {
        var konto = new KontoPlus("Test", 100m, 50m);
        konto.Wyplata(120m); 
        konto.Wplata(10m); 
        Assert.IsTrue(konto.Zablokowane);
        Assert.AreEqual(-10m, konto.Bilans);
    }
}