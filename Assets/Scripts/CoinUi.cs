using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinUi : GameUi
{

    [SerializeField] TextMeshProUGUI _currentHashrateText;
    [SerializeField] TextMeshProUGUI _neededHashrateText;
    [SerializeField] Image _currentCoinImage;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        SetNeededHasrateText();
        _currentHashrateText.text = Game.CurrentHashrate.ToString("f1");

    }

    public void SetNeededHasrateText()
    {
        _neededHashrateText.text = Game.CurrentCoin.NeededHashRate.ToString();
    }
    public void OnCoinPressed()
    {
        if (Game.State == Game.GameState.GettingReward)
        {
            return;
        }
        Game.OnCoinPressed();
    }
    public void UpdateUi(float currentHashrate)
    {
        _currentHashrateText.text = currentHashrate.ToString("f1");
    }
    public void SetCoinImage(int id)
    {
        _currentCoinImage.sprite = Game.Ui.ImagesPoolUi.GetCoinImage(id);
    }
}
