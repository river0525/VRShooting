using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP
{
    private int amount;
    private readonly int MIN = 0;
    private int MAX;
    private int Amount
    {
        get
        {
            return amount;
        }
        set
        {
            if (value < MIN) amount = MIN;
            if (MAX < value) amount = MAX;
            if (MIN <= value && value <= MAX) amount = value;
        }
    }

    public HP(int amount, int MAX)
    {
        this.MAX = MAX;
        Amount = amount;
    }
    
    public void Add(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException("0–¢–ž‚Ì’l‚Å‚·");
        Amount += amount;
    }

    public void Subtract(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException("0–¢–ž‚Ì’l‚Å‚·");
        Amount -= amount;
    }

    public int Get()
    {
        return Amount;
    }
    public int GetMax()
    {
        return MAX;
    }

    public void FullRecover()
    {
        Amount = MAX;
    }
}
