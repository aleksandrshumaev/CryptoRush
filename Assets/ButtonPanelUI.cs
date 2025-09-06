using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonPanelUI : GameUi
{
    [SerializeField] TextMeshProUGUI _farmButtonText;
    [SerializeField] TextMeshProUGUI _exchangeButtonText;
    [SerializeField] TextMeshProUGUI _shopButtonText;
    [SerializeField] TextMeshProUGUI _collectionButtonText;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        SetTextLines();
    }
    public override void SetTextLines()
    {
        _farmButtonText.text = GetTranslation("UI_Farm");
        _exchangeButtonText.text = GetTranslation("UI_Exchange");
        _shopButtonText.text = GetTranslation("UI_Shop");
        _collectionButtonText.text = GetTranslation("UI_Collection");
    }
}
