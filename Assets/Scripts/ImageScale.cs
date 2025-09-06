using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScale : GameUi
{
    Image _image;
    RectTransform _rt;
    //GameObject _thisGM;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        _image = GetComponent<Image>();
        _rt=this.GetComponent<RectTransform>();
        SetSquareScale();
        //Game.Ui.OnGridScaleUpdated += SetSquareScale;
        //_thisGM = this.gameObject;
    }
    public void SetSquareScale()
    {

        //float width=_image.sprite.rect.width;
        //float height=_image.sprite.rect.height;
        float width = _rt.rect.width;
        float height = _rt.rect.height;
        Vector2 position = _rt.rect.position;
        //Debug.Log($"{name} initialSize is {position}");
        if (width > height)
        {
            width = height;
        }
        else
        {
            height = width;
        }
        //Debug.Log($"{name} Size is X: {width} and Y: {height}");
        _rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        _rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        //_rt.sizeDelta= new Vector2(width, height);
        //_image.rect.Set(position.x, position.y, width, height);
    }
    private void OnDestroy()
    {
      // Game.Ui.OnGridScaleUpdated -= SetSquareScale;
    }
    // Update is called once per frame
    void Update()
    {
        //SetSquareScale();
/*        if(_rt.rect.width!= _rt.rect.height)
        {
            SetSquareScale();
        }*/
    }
}
