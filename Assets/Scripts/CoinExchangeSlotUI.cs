using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinExchangeSlotUI : GameUi
{
    [SerializeField] TextMeshProUGUI _sellForStableText;
    [SerializeField] TextMeshProUGUI _sellForMainText;
    [SerializeField] TextMeshProUGUI _sellForText;
    [SerializeField] Image _tradableCoinImage;
    [SerializeField] Image _stableCoinImage;
    [SerializeField] Image _mainCoinImage;
    Coin _tradableCoin;
    Coin _stableCoin;
    Coin _mainCoin;
    int _tradableCoinID;
    // Start is called before the first frame update
    public void SellCoinForStableCoin()
    {
        if (Game.State == Game.GameState.GettingReward)
        {
            return;
        }
        float stablePrice=_tradableCoin.StablePrice;
        if(Game.GetPlayer().RemoveCoin(_tradableCoin, 1))
        {
            Game.GetPlayer().AddCoin(_stableCoin, stablePrice);
        }
        UpdateUi();
    }
    void SetImages()
    {
        _tradableCoinImage.sprite = Game.Ui.ImagesPoolUi.GetCoinImage(_tradableCoin.Id);
        _stableCoinImage.sprite = Game.Ui.ImagesPoolUi.GetCoinImage(_stableCoin.Id);
        _mainCoinImage.sprite = Game.Ui.ImagesPoolUi.GetCoinImage(_mainCoin.Id);
    }
    public void SellCoinForMainCoin()
    {
        if (Game.State == Game.GameState.GettingReward)
        {
            return;
        }
        float mainPrice = _tradableCoin.StablePrice / _mainCoin.StablePrice;
        if (Game.GetPlayer().RemoveCoin(_tradableCoin, 1))
        {
            Game.GetPlayer().AddCoin(_mainCoin, mainPrice);
        }
        UpdateUi();
    }
    public override void Start()
    {
        base.Start();
        _stableCoin = Game.CoinPool.GetCoinFromId(0);
        _mainCoin = Game.CoinPool.GetCoinFromId(1);
        SetTradableCoin(_tradableCoinID);
        SetImages();
        SetTextLines();
    }
    public override void SetTextLines()
    {
        _sellForText.text = GetTranslation("UI_SellFor");
    }
    public void SetTradableCoinId(int id)
    {
        _tradableCoinID = id;
    }
    public void SetTradableCoin(int id)
    {
        _tradableCoin = Game.CoinPool.GetCoinFromId(id);
        UpdateUi();

        //ADD
        //_tradableCoinImage = Image;
    }
    void UpdateUi()
    {
        _sellForStableText.text = _tradableCoin.StablePrice.ToString();
        _sellForMainText.text =(_tradableCoin.StablePrice/ _mainCoin.StablePrice).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
