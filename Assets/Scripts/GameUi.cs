using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUi : MonoBehaviour
{
    Game _game;
    public Game Game { get => _game; }

    // Start is called before the first frame update
    public virtual void Start()
    {
        _game=GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        _game.OnLanguageChanged+=SetTextLines;
    }
    public string GetTranslation(string rawText)
    {
        return _game.TranslationData.GetTranslatedText(rawText);
    }
    public virtual void SetTextLines()
    {

    }
    private void OnDestroy()
    {
        if(_game != null)
            _game.OnLanguageChanged -= SetTextLines;
    }

}
