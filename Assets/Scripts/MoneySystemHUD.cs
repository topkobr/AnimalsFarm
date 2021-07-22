using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystemHUD : MonoBehaviour
{
    #region Singletion
    public static MoneySystemHUD instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogWarning("Found more than one MoneySystemHUD instance");
    }
    #endregion

    [SerializeField] private Text _coinsAmountText;

    private void Start()
    {
        UpdateMoney();
    }

    public void UpdateMoney()
    {
        _coinsAmountText.text = "" + MoneySystem.GetMoney();
    }
}
