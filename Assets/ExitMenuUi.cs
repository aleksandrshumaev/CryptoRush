using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitMenuUi : GameUi
{
    [SerializeField] TextMeshProUGUI _exitGameText;
    [SerializeField] TextMeshProUGUI _exitButtonText;
    [SerializeField] TextMeshProUGUI _cancleButtonText;
    public override void Start()
    {
        base.Start();
        SetTextLines();
    }
    public override void SetTextLines()
    {
        _exitGameText.text = GetTranslation("UI_ExitGameText");
        _exitButtonText.text = GetTranslation("UI_ExitButton");
        _cancleButtonText.text = GetTranslation("UI_CancleButton");
    }
    public void OnExitButton()
    {
        Application.Quit();
    }
/*    private void OnEnable()
    {
        SetTextLines();
    }*/
    public void OnCanlceButton()
    {
        this.gameObject.SetActive(false);
    }
}
