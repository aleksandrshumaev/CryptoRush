using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FarmSlot : GameUi
{
    [SerializeField] int _toolIdForSlot;
    [SerializeField] TextMeshProUGUI _toolNameText;
    [SerializeField] TextMeshProUGUI _toolEfficiencyText;
    [SerializeField] TextMeshProUGUI _toolTotalEfficiencyText;
    [SerializeField] TextMeshProUGUI _toolPriceText;
    [SerializeField] Image _farmToolImage;
    FarmTool _tool;

    public override void Start()
    {
        base.Start();
        _tool=Game.FarmToolPool.GetToolFromId(_toolIdForSlot);
        SetTextLines();
        _farmToolImage.sprite = Game.Ui.ImagesPoolUi.GetFarmToolImage(_toolIdForSlot);
    }
    public void OnByButton()
    {
        if(Game.State==Game.GameState.GettingReward)
        {
            return;
        }
        Coin _stableCoin=Game.CoinPool.GetCoinFromId(0);
        int toolAmount = Game.GetPlayer().GetFarmToolAmounntFromID(_toolIdForSlot);
        if (Game.GetPlayer().RemoveCoin(_stableCoin, _tool.GetActualPrice(toolAmount)))
        Game.GetPlayer().AddTool(_tool, 1);
        UpdateUi(toolAmount+1);
        Game.Ui.UpdateUi();

    }
    public override void SetTextLines()
    {
        int toolAmount = Game.GetPlayer().GetFarmToolAmounntFromID(_toolIdForSlot);
        _toolNameText.text = GetTranslation("UI_" + _tool.Name);
        UpdateUi(toolAmount);
    }
    public void UpdateUi(int toolAmount)
    {
        _toolEfficiencyText.text = _tool.Efficiensy.ToString() + "/" + GetTranslation("UI_PerSec");
        _toolTotalEfficiencyText.text = (_tool.Efficiensy * toolAmount).ToString() + "/" + GetTranslation("UI_PerSec");
        _toolPriceText.text = _tool.GetActualPrice(toolAmount).ToString();

    }
}
