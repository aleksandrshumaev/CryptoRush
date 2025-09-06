using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Steamworks;

public class Game : MonoBehaviour
{
    public enum GameState
    {
        Normal,
        GettingReward
    }
    public delegate void GameSaveLoad();
    delegate void GameLoaded();
    public delegate void LanguageChanged();


    GameSaveLoad _gameSaved;
    GameSaveLoad _gameLoaded;
    LanguageChanged _languageChanged;
    [SerializeField] Coin _currentCoin;
    [SerializeField] Ui _ui;
    [SerializeField] Player _player;
    [SerializeField] CoinPool _coinPool;
    [SerializeField] FarmToolsPool _farmToolPool;
    [SerializeField] MysteryBox _mysteryBox;
    [SerializeField] SaveLoad _saveLoad;
    [SerializeField]TranslationData _translationData;
    [SerializeField] SteamInventoryService _steamInventory;
    List<DateTime> _boxesTime=new();
    GameState _state;
    float _currentHashrate=0;

    //int _neededHashrate;
    public Coin CurrentCoin { get => _currentCoin; }
    public Ui Ui { get => _ui; set => _ui = value; }
    public float CurrentHashrate { get => _currentHashrate; }
    public CoinPool CoinPool { get => _coinPool; }
    public FarmToolsPool FarmToolPool { get => _farmToolPool;}
    public MysteryBox MysteryBox { get => _mysteryBox;}
    public TranslationData TranslationData { get => _translationData; set => _translationData = value; }
    public GameState State { get => _state; set => _state = value; }
    public GameSaveLoad OnGameSaved { get => _gameSaved; set => _gameSaved = value; }
    public GameSaveLoad OnGameLoaded { get => _gameLoaded; set => _gameLoaded = value; }
    public LanguageChanged OnLanguageChanged { get => _languageChanged; set => _languageChanged = value; }

    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/save.cum";
        Debug.Log(path);
        if (File.Exists(path))
        {
            Load();
        }
        else
        {
            ChangeCurrentCoin(2);
        }

        SetNormalState();
        StartMining();
        Ui.GameStarted();
        SteamTest();
    }

    public DateTime LoadBoxOpenTime(int id)
    {
        DateTime timeToReturn = DateTime.Now;
        if (_boxesTime.Count >= id+1 && _boxesTime[id] != null)
            timeToReturn = _boxesTime[id];
        return timeToReturn;
    }

    void SteamTest()
    {
        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();
            Debug.Log(name);
            _steamInventory.GetSteamInventroy();
        }
    }
   /* public void GetInvenrotyFromSteam()
    {
        if(SteamManager.Initialized)
        {
            GetPlayer().CoinSkinInventory.Clear();
            SteamInventoryResult_t inventory=SteamInventoryResult_t.Invalid;
            Steamworks.SteamInventory.GetAllItems(out inventory);
            SteamItemDetails_t[] itemDetales = null;
            uint resultAmount=1;
            SteamInventory.GetResultItems(inventory, itemDetales, ref resultAmount);
            Debug.Log(resultAmount);
            itemDetales = new SteamItemDetails_t[resultAmount];
            SteamInventory.GetResultItems(inventory, itemDetales, ref resultAmount);
            foreach (SteamItemDetails_t item in itemDetales)
            {
                Debug.Log(item.m_iDefinition.m_SteamItemDef);
                GetPlayer().AddScinCoinToInevtory((int)item.m_iDefinition.m_SteamItemDef);
            }
        }    
    }*/
    public void SetNormalState()
    {
        _state = GameState.Normal;
    }
    public void SetGettingRewardState()
    {
        _state=GameState.GettingReward;
    }
    public Player GetPlayer()
    {
        return _player;
    }
    public void Saving()
    {
        if(OnGameSaved != null)
        {
            OnGameSaved.Invoke();
        }
    }
    public void Save()
    {
        _boxesTime.Clear();
        Saving();
        int farmToolPoolCount = _farmToolPool.GetPoolCount();
        int coinPoolCount= _coinPool.GetPoolCount();
        _saveLoad.Save(farmToolPoolCount,coinPoolCount,GetPlayer(),CurrentCoin,_boxesTime);
        Debug.Log("saved");
    }
    public void Loaded()
    {
        Debug.Log("Loaded");
        if (OnGameLoaded != null)
        {
            OnGameLoaded.Invoke();
        }
    }
    public void Load()
    {
        PlayerData data=_saveLoad.Load();
        GetPlayer().LoadPlayerData(data);
        _boxesTime=data.GetBoxesTimes();
        //GetInvenrotyFromSteam();
        ChangeCurrentCoin(data.CurrentCoinId);
        CalculateEarnedHashRateOffline(data);
        Loaded();
    }
    void CalculateEarnedHashRateOffline(PlayerData data)
    {
        DateTime lastExitTime=data.GetTime();
        DateTime currentTime = GetCurentTime();
        IncreaseHashrate((float)currentTime.Subtract(lastExitTime).TotalSeconds* GetPlayer().HashRatePerSec);
    }
    DateTime GetCurentTime()
    {
        return DateTime.Now;
    }
    public void ChangeCurrentCoin(int id)
    {
        if(!GetPlayer().CoinSkinInventory.ContainsKey(id))
        {
            id = 2;
        }
        _currentCoin = _coinPool.GetCoinFromId(id);
        _currentHashrate = 0;
        _ui.BalanceUi.ChangeCurrentCoinIImage();
        _ui.UpdateUi();
        _ui.CoinUi.UpdateUi(CurrentHashrate);
        _ui.CoinUi.SetCoinImage(id);
    }
    void IncreaseHashrate(float value)
    {
        _currentHashrate+=value;
        if (_currentHashrate >= _currentCoin.NeededHashRate)
        {
            value =_currentHashrate - _currentCoin.NeededHashRate;
            _player.AddCoin(_currentCoin, 1f);
            _currentHashrate =0;
            IncreaseHashrate(value);
        }
        _ui.CoinUi.UpdateUi(_currentHashrate);
        Ui.CoinUi.SetNeededHasrateText();
        _ui.UpdateUi();
    }
    public void OnCoinPressed()
    {
        IncreaseHashrate(1f);
    }
    void StartMining()
    {
        StartCoroutine("FarmMiningTick");
    }
    IEnumerator FarmMiningTick()
    {
        yield return new WaitForSeconds(1);
        MiningTick();
    }
    void MiningTick()
    {
        IncreaseHashrate(_player.HashRatePerSec);
        StartCoroutine("FarmMiningTick");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetRewardFromBox(int boxId)
    {
        int itemId=boxId+CoinPool.GetPoolCount();
        _steamInventory.TriggerDrop(itemId);
        /*SteamItemDef_t itemToAdd=new SteamItemDef_t(itemId);
        Debug.Log(itemToAdd.m_SteamItemDef);
        SteamInventoryResult_t result;
        SteamInventory.TriggerItemDrop(out result, itemToAdd);
        SteamItemDetails_t[] details=null;
        UInt32 resultLength = 1 ;
        SteamInventory.GetResultItems(result, details,ref resultLength);
        Debug.Log(resultLength);
        if(resultLength!=0)
        {
            details = new SteamItemDetails_t[resultLength];
            SteamInventory.GetResultItems(result, details, ref resultLength);
            Debug.Log(details[0]);
            Ui.ShopUi.ShowBoxRewardPanel(CoinPool.GetCoinFromId(result.m_SteamInventoryResult));
            _steamInventory.GetSteamInventroy();
        }*/
    }
    public void AddBoxTimer(DateTime timer)
    {
        _boxesTime.Add(timer);
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    public void ChangeLanguage(int buttonId)
    {
        _translationData.ChangeGameLanguage((TranslationData.Languages)buttonId);
    }
}
