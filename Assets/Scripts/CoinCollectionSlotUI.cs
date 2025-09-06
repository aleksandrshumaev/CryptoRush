using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollectionSlotUI : GameUi
{
    int _coinId;
    [SerializeField] Image _coinImage;
    [SerializeField] Image _coinBackGround;
    [SerializeField] List<Color> _rarietyColor;
    public void SetCoin(int id)
    {
        base.Start();
        Coin coin = Game.CoinPool.GetCoinFromId(id);
        _coinId = id;
        _coinImage.sprite = Game.Ui.ImagesPoolUi.GetCoinImage(id);
        _coinBackGround.color = _rarietyColor[(int)coin.Rariety];
    }
    public void OnScinCoinPressed()
    {
        if (Game.State == Game.GameState.GettingReward)
        {
            return;
        }
        Game.ChangeCurrentCoin(_coinId);
    }
}
