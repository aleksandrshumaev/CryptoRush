using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectionUI : GameUi
{
    [SerializeField] GridComponentsScale _gridComponentsScale;
    public void SetActive()
    {
        base.Start();
        //Game.GetInvenrotyFromSteam();
        _gridComponentsScale.ClearGridContent();
        Debug.Log("Coins In Inventory:"+Game.GetPlayer().CoinSkinInventory.Count);
        if(Game.GetPlayer().CoinSkinInventory.Count>0)
        {
            foreach (int coinId in Game.GetPlayer().CoinSkinInventory.Keys)
            {
                _gridComponentsScale.AddGridContent().GetComponent<CoinCollectionSlotUI>().SetCoin(coinId);
            }
            _gridComponentsScale.SetSizes();
        }


    }
}
