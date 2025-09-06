using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUi : GameUi
{
    [SerializeField] GameObject _rewardPanel;
    [SerializeField] TextMeshProUGUI _rewardDescription;
    [SerializeField] Image _glowing;
    [SerializeField] Image _coinImage;
    [SerializeField] List<Color> _glowingColors=new();
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        _rewardPanel.SetActive(false);
    }

    public void ShowBoxRewardPanel(Coin coin,int amount=-1)
    {
        _rewardPanel.SetActive(true);
        Game.SetGettingRewardState();
        _coinImage.sprite = Game.Ui.ImagesPoolUi.GetCoinImage(coin.Id);
        _glowing.color=_glowingColors[(int)coin.Rariety];
        string description = GetTranslation("Ui_GettingReward") + " " + GetTranslation( coin.Name);
        if(amount > 0)
        {
            description += $": {amount}!";
        }
        else 
        {
            description += "!";
        }
        _rewardDescription.text=description;
    }
    public void HideBoxRewardPanel()
    {
        _rewardPanel.SetActive(false);
        Game.SetNormalState();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
