using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceUi :GameUi
{
    //[SerializeField] Text _currentCoinName;
    [SerializeField] Text _currentCoinAmount;
    //[SerializeField] Text _stableCoinName;
    [SerializeField] Text _stableCoinAmount;
    //[SerializeField] Text _mainCoinName;
    [SerializeField] Text _mainCoinAmount;
    [SerializeField] Coin _stableCoin;
    [SerializeField] Coin _mainCoin;
    [SerializeField] Image _stableCoinImage;
    [SerializeField] Image _mainCoinImage;
    [SerializeField] Image _currentCoinImage;
    Coin _currentCoin;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        _stableCoinImage.sprite=Game.Ui.ImagesPoolUi.GetCoinImage(_stableCoin.Id);
        _mainCoinImage.sprite = Game.Ui.ImagesPoolUi.GetCoinImage(_mainCoin.Id);
        //UpdateUi();
        ChangeCurrentCoinIImage();
    }
    public void ChangeCurrentCoinIImage()
    {
        _currentCoin = Game.CurrentCoin;
        _currentCoinImage.sprite = Game.Ui.ImagesPoolUi.GetCoinImage(_currentCoin.Id);
    }
    public void UpdateUi()
    {
       

        //_currentCoinName.text = currentCoin.Name.ToString();
        _currentCoinAmount.text = Game.GetPlayer().GetCoinAmount(_currentCoin).ToString();
        //_stableCoinName.text=_stableCoin.Name.ToString();
        //_mainCoinName.text=_mainCoin.Name.ToString();
        _stableCoinAmount.text=Game.GetPlayer().GetCoinAmount(_stableCoin).ToString();
        _mainCoinAmount.text=Game.GetPlayer().GetCoinAmount(_mainCoin).ToString("f3");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
