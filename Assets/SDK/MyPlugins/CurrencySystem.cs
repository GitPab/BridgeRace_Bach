using System;
using UnityEngine;

public enum CurrencyType
{
    GOLD = 0,
    GEM = 1,
    QUEST_POINT = 2,
    MANA = 3
}

public class OnCurrencyChange : IEventParameterBase
{
    public CurrencyType type;
    public int amount;
    public int total;
}

public static class CurrencySystem
{
    public static void AddCurrency(CurrencyType type, int amount)
    {
        var current = GetCurrency(type);
        current += amount;
        current = Mathf.Clamp(current, 0, int.MaxValue);
        PlayerPrefs.SetInt(type.ToString(), current);

        EventManager.Instance.TriggerEvent(new OnCurrencyChange
        {
            type = type,
            amount = amount,
            total = current
        });
    }

    public static int GetCurrency(CurrencyType type)
    {
        return PlayerPrefs.GetInt(type.ToString(), 0);
    }

    public static void WithDrawal(CurrencyType type, int amount, Action<bool> OnWithdrawCompleted)
    {
        int current = GetCurrency(type);
        if (current < amount)
        {
            if (OnWithdrawCompleted != null)
                OnWithdrawCompleted(false);

            return;
        }

        AddCurrency(type, -amount);

        if (OnWithdrawCompleted != null)
            OnWithdrawCompleted(true);
    }

    public static bool IsEnoughCurrency(CurrencyType type, int amount)
    {
        return GetCurrency(type) >= amount;
    }
}