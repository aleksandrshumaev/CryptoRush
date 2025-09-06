using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HashInfoUi : GameUi
{
    // Start is called before the first frame update
    [SerializeField]Image _fill;
    [SerializeField] TextMeshProUGUI _farmPowerText;
    [SerializeField] TextMeshProUGUI _farmPowerValueText;
    public override void Start()
    {
        base.Start();
        SetTextLines();
    }
    public override void SetTextLines()
    {
        _farmPowerText.text = GetTranslation("UI_CurrentFarmPower");
        UpdateUI();
    }
    void UpdateHash()
    {
        _fill.fillAmount = Game.CurrentHashrate / Game.CurrentCoin.NeededHashRate;
    }

    public void UpdateUI()
    {
        _farmPowerValueText.text=Game.GetPlayer().HashRatePerSec.ToString("f1")+GetTranslation("UI_PerSec");
        UpdateHash();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
