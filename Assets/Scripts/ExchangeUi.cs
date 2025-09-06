using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeUi : GameUi
{
    [SerializeField] GridComponentsScale _gridComponentsScale;
    public void SetActive()
    { 
        base.Start();
        _gridComponentsScale.ClearGridContent();
        Debug.Log("ExchangePanelOpened");
        foreach(int coinId in Game.GetPlayer().CryptoWallet.Keys)
        {
            if(coinId != 0&& coinId!=1)
                _gridComponentsScale.AddGridContent().GetComponent<CoinExchangeSlotUI>().SetTradableCoinId(coinId);
        }
        _gridComponentsScale.SetSizes();
    }
}
