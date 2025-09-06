using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSettingsUi : GameUi
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public void OnLanguageButtonPressed(int buttonId)
    {
        Game.ChangeLanguage(buttonId);
    }
    public void OnConfirmButton()
    {
        this.gameObject.SetActive(false);
    }
}
