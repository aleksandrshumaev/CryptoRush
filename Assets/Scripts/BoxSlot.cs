using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class BoxSlot : GameUi
{
    [SerializeField] float _cost;
    [SerializeField] int _id;
    [SerializeField] Image _boxImage;
    [SerializeField] Image _mainCoinImage;
    [SerializeField] TextMeshProUGUI _nameOfBox;
    [SerializeField] TextMeshProUGUI _chanseDescriptionOfBox;
    [SerializeField] TextMeshProUGUI _buttonCostText;
    [SerializeField] Image _nextOpenTimerSlider;
    DateTime _nextOpenTime=DateTime.Now;
    int _secondsToNextOpen;
    int _secondsToNextOpenTotal;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        SetTextLines();
        _buttonCostText.text=_cost.ToString();
        Game.OnGameLoaded += OnGameLoaded;
        Game.OnGameSaved += OnGameSaving;
        SetNextOpenTimeAtStart();
        SetImages();
    }
    public override void SetTextLines()
    {
        _nameOfBox.text = GetTranslation("UI_BoxName_" + _id.ToString());
        _chanseDescriptionOfBox.text = GetTranslation("UI_BoxDescription_" + _id.ToString());
    }
    void OnGameLoaded()
    {
        SetNexOpenTime();
    }
    void OnGameSaving()
    {
        Game.AddBoxTimer(_nextOpenTime);
    }
    void SetNextOpenTimeAtStart()
    {
        _nextOpenTime = DateTime.Now.AddMinutes(1);
        _secondsToNextOpenTotal = 60;
        Debug.Log("LESS THAN MINUTE: " + _nextOpenTime.ToString());
        _boxImage.gameObject.SetActive(true);
    }
    void SetNexOpenTime()
    {
        if (LoadNextOpenTime().Subtract(DateTime.Now).TotalSeconds < 60)
        {
            return;
        }
        _nextOpenTime = LoadNextOpenTime();
        _secondsToNextOpenTotal = (int)(GetBoxCoefficient() * 60f * 60f);
        Debug.Log("At Start: " + _nextOpenTime.ToString());
        double seconds = _nextOpenTime.Subtract(DateTime.Now).TotalSeconds;
        _boxImage.gameObject.SetActive(true);
        //StartCountDown();
    }
    private void OnEnable()
    {
        StartCountDown();
    }
    DateTime LoadNextOpenTime()
    {
        
        DateTime openTime=Game.LoadBoxOpenTime(_id); ;
       
        return openTime;
    }
    void SetImages()
    {
        _boxImage.sprite=Game.Ui.ImagesPoolUi.GetBoxImage(_id);
        _mainCoinImage.sprite=Game.Ui.ImagesPoolUi.GetCoinImage(1);
    }
    public void OnBuyBoxButton()
    {
        if (Game.State == Game.GameState.GettingReward)
        {
            return;
        }
        if (Game.GetPlayer().RemoveCoin(1,_cost))
        {
            if(Game.MysteryBox.OpenBox(_id))
                GetNextOpenTime();
        }
        else
        {
            Debug.Log("Not Enough MainCoin");
        }
    }
    float GetBoxCoefficient()
    {
   
        switch (_id)
        {
            case 0:
                return  0.5f;
            case 1:
                return  1f;
            case 2:
                return  2f;
            case 3:
                return 3f;
            case 4:
                return 5f;
            default:
                return 0f;
        }
    }
   
    void GetNextOpenTime()
    {
        double hoursCoefficient=GetBoxCoefficient();
        
        _nextOpenTime=DateTime.Now.AddMinutes((int)(hoursCoefficient * 60f));
        _secondsToNextOpenTotal = (int)(hoursCoefficient * 60f * 60f);
        _nextOpenTimerSlider.gameObject.SetActive(true);
        StartCountDown();
        
    }
    void StartCountDown()
    {
        //_secondsToNextOpenTotal = (int)_nextOpenTime.Subtract(DateTime.Now).TotalSeconds;
        _secondsToNextOpen = (int)_nextOpenTime.Subtract(DateTime.Now).TotalSeconds;
        Debug.Log("Total Seconds: " + _secondsToNextOpenTotal);
        StartCoroutine("SecondsTick");

    }
    void DeductSecundFromNextOpenTimer()
    {
        Debug.Log("Curent Seconds: " + _secondsToNextOpen);
        if (_secondsToNextOpen>0)
        {
            
            _secondsToNextOpen--;
            _nextOpenTimerSlider.fillAmount = (float)_secondsToNextOpen / _secondsToNextOpenTotal;
            Debug.Log("Ratio: " + (float)_secondsToNextOpen / _secondsToNextOpenTotal);
            StartCoroutine("SecondsTick");
        }
        else
        {
            _nextOpenTimerSlider.gameObject.SetActive(false);
        }
    }
    IEnumerator SecondsTick()
    {
        Debug.Log("CoRoutin Started");
        yield return new WaitForSeconds(1);
        //Debug.Log("Second Passed");
        DeductSecundFromNextOpenTimer();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        Game.OnGameLoaded -= OnGameLoaded;
        Game.OnGameSaved -= OnGameSaving;
    }
}
