using UnityEngine;
public static class MoneySystem
{
    public static int GetMoney()
    {
        return SaveSystem.GetSave().CurrentMoney;
    }

    public static void GiveMoney(int amount)
    {
        Debug.Log("Current money = " + SaveSystem.GetSave().CurrentMoney + " + " + amount);
        SaveSystem.GetSave().CurrentMoney += amount;
        SaveSystem.CommitSave();
        UpdateHud();
    }

    public static bool TakeMoney(int amount)
    {
        if (SaveSystem.GetSave().CurrentMoney - amount >= 0)
        {
            SaveSystem.GetSave().CurrentMoney -= amount;
            SaveSystem.CommitSave();
            UpdateHud();
            return true;
        } else
        {
            return false;
        }
    }

    private static void UpdateHud()
    {
        if (MoneySystemHUD.instance != null)
            MoneySystemHUD.instance.UpdateMoney();
    }
}
