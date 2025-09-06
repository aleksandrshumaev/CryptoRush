using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui : GameUi
{
    public delegate void OnUiUpdate();
    public delegate void OnGridScaleUpdate();
    [SerializeField] CoinUi _coinUi;
    [SerializeField] BalanceUi _balanceUi;
    [SerializeField] ShopUi _shopUi;
    [SerializeField] HashInfoUi _hashInfoUi;
    [SerializeField] ImagesPoolUI _imagesPoolUi;
    [SerializeField] FarmAndExchangeUi _farmAndExchangeUi;
    [SerializeField] GameObject _languageSettingsUiPanel;
    [SerializeField] GameObject _exitUiPanel;

    OnUiUpdate _onUiUpdated;
    OnGridScaleUpdate _onGridScaleUpdated;
    public CoinUi CoinUi { get => _coinUi; set => _coinUi = value; }
    public BalanceUi BalanceUi { get => _balanceUi; set => _balanceUi = value; }
    public ShopUi ShopUi { get => _shopUi;}
    public HashInfoUi HashInfoUi { get => _hashInfoUi; set => _hashInfoUi = value; }
    public OnUiUpdate OnUiUpdated { get => _onUiUpdated; set => _onUiUpdated = value; }
    public ImagesPoolUI ImagesPoolUi { get => _imagesPoolUi;}
    public OnGridScaleUpdate OnGridScaleUpdated { get => _onGridScaleUpdated; set => _onGridScaleUpdated = value; }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public void GameStarted()
    {
        _farmAndExchangeUi.ShowExchangePanel();
    }


    public void UpdateUi()
    {
        _balanceUi.UpdateUi();
        _hashInfoUi.UpdateUI();
        if(_onUiUpdated != null)
        {
            _onUiUpdated.Invoke();
        }
    }
    public void GridScaleChanged()
    {
        if(_onGridScaleUpdated != null)
            _onGridScaleUpdated.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnShowLanguageSettingsButton()
    {
        _languageSettingsUiPanel.SetActive(true);
    }
    public void OnExitButon()
    {
        _exitUiPanel.SetActive(true);
    }
}
