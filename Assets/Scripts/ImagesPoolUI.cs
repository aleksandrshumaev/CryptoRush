using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagesPoolUI : GameUi
{
    [SerializeField] List<Sprite> _coinsImages = new List<Sprite>();
    [SerializeField] List<Sprite> _toolsImages = new List<Sprite>();
    [SerializeField] List<Sprite> _boxesImages = new List<Sprite>();
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }
    public Sprite GetCoinImage(int coinId)
    {
        //Debug.Log($"Set Image For Coin With Id{coinId}");
        return _coinsImages[coinId];
    }
    public Sprite GetBoxImage(int boxId)
    {
        //Debug.Log($"Set Image For box With Id{boxId}");
        return _boxesImages[boxId];
    }
    public Sprite GetFarmToolImage(int toolId)
    {
        //Debug.Log($"Set Image For tool With Id{toolId}");
        return _toolsImages[toolId];
    }
}
