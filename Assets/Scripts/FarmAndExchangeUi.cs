using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmAndExchangeUi : GameUi
{
    [SerializeField] GameObject _farmPanel;
    [SerializeField] GameObject _exchangePanel;
    [SerializeField] GameObject _darkWebPanel;
    [SerializeField] GameObject _collectionPanel;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }
    public void ShowFarmPanel()
    {
        if (Game.State == Game.GameState.GettingReward)
        {
            return;
        }
        HideAllPanels();
        _farmPanel.SetActive(true);
    }
    public void ShowExchangePanel()
    {
        if (Game.State == Game.GameState.GettingReward)
        {
            return;
        }
        HideAllPanels();
        _exchangePanel.SetActive(true);
        _exchangePanel.GetComponent<ExchangeUi>().SetActive();
    }
    public void ShowDarWebPanel()
    {
        if (Game.State == Game.GameState.GettingReward)
        {
            return;
        }
        HideAllPanels();
        _darkWebPanel.SetActive(true);
    }
    public void ShowCollectionPanel()
    {
        if (Game.State == Game.GameState.GettingReward)
        {
            return;
        }
        HideAllPanels();
        _collectionPanel.SetActive(true);
        _collectionPanel.GetComponent<CoinCollectionUI>().SetActive();
    }
    void HideAllPanels()
    {
        _farmPanel.SetActive(false);
        _exchangePanel.SetActive(false);
        _darkWebPanel.SetActive(false);
        _collectionPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
