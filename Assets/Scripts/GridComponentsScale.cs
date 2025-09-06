using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridComponentsScale : GameUi
{
    public enum TypeOfCell
    {
        Square=1,
        OneToTwo=2,
        OneToFive=5,
        OneToTen=10
    }
    [SerializeField] TypeOfCell _type;
    [SerializeField] GameObject _gridContentPrefab;
    List<GameObject> _childComponents = new();
    GridLayoutGroup _grid;
    RectTransform _gridGameObjectRectTransform;
    int _colums;
    public override void Start()
    {
        
        //SetSizes();
    }
    
    public void SetSizes()
    {
        base.Start();
        _grid = GetComponent<GridLayoutGroup>();
        _gridGameObjectRectTransform = GetComponent<RectTransform>();
        if (_grid.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
        {
            _colums = _grid.constraintCount;
        }
        SetSellSize();
        SetGridGameObjectYSize();
        SetPosition();
       // Game.Ui.OnGridScaleUpdated();
        /*        if (Game.Ui.OnGridScaleUpdated != null)
                {
                    Debug.Log("SCale IMAGE");
                    Game.Ui.OnGridScaleUpdated.Invoke();
                }*/
        Debug.Log("GRIDScale");
    }

    void SetSellSize()
    {
        float xSizeOfGridGameObjectTransform = _gridGameObjectRectTransform.rect.width;
        //float ySizeOfGridGameObjectTransform = _gridGameObjectRectTransform.rect.height;
        float xCellSize = xSizeOfGridGameObjectTransform / _colums;
        float yCellSize = xCellSize /(int) _type;
        _grid.cellSize = new Vector2(xCellSize, yCellSize);
    }
    public GameObject AddGridContent()
    {
        GameObject content = Instantiate(_gridContentPrefab, GetComponent<RectTransform>());
        _childComponents.Add(content);
        return content;
    }
    void SetGridGameObjectYSize()
    {
        int cellsCount=_childComponents.Count;
        int rowsCount=cellsCount/_colums;
        float ySize=rowsCount*_grid.cellSize.y;
        _gridGameObjectRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ySize);
    }
    public void ClearGridContent()
    {
        for(int i=_childComponents.Count-1; i>=0; i--)
        {
            GameObject.Destroy(_childComponents[i]);
        }
        _childComponents.Clear();
    }
    void SetPosition()
    {
        _gridGameObjectRectTransform.anchoredPosition=new Vector2(0,0);
    }
}
