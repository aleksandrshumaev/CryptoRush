using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    float[] _farmTools;
    float[] _coinInventory;
    float[] _cyproWallet;
    float[] _totalMinedCoinsAmmount;
    int _year, _month, _day,_hours, _minutes, _seconds;
    int _currentCoinId;
    int[] _commonBoxTimer;
    int[] _unCommonBoxTimer;
    int[] _rareBoxTimer;
    int[] _mythicalBoxTimer;
    int[] _legendaryBoxTimer;
    int[,] _boxTimers;


    public int CurrentCoinId { get => _currentCoinId; }

    public PlayerData(int coinPoolCount, int farmToolPoolCount,Player player,Coin coin,List<DateTime> boxesTimer)
    {
        _currentCoinId = coin.Id;
        _farmTools = GetFarmTools(farmToolPoolCount,player);
        _coinInventory = GetCoinInventory(coinPoolCount,player);
        _cyproWallet = GetCryptoWallet(coinPoolCount, player);
        _totalMinedCoinsAmmount = GetTotalMinedCoinsAmount(coinPoolCount, player);
        SetTime();
        SetBoxesTimers(boxesTimer);

    }
    void SetBoxesTimers(List<DateTime> boxesTimer)
    {
        _boxTimers = new int[boxesTimer.Count,5];
        Debug.Log($"Boxes count {boxesTimer.Count}");
        int boxTimerIndex = 0;
        foreach (DateTime boxTimer in boxesTimer)
        {
            
            Debug.Log($"Boxe index {boxTimerIndex}");
            _boxTimers[boxTimerIndex, 0] = boxTimer.Year;
            _boxTimers[boxTimerIndex, 1] = boxTimer.Month;
            _boxTimers[boxTimerIndex, 2] = boxTimer.Day;
            _boxTimers[boxTimerIndex, 3] = boxTimer.Hour;
            _boxTimers[boxTimerIndex, 4] = boxTimer.Minute;
            boxTimerIndex++;
        }

    }
    void SetTime()
    {
        DateTime time = DateTime.Now;
        _year=time.Year;
        _month=time.Month;
        _day=time.Day;
        _hours=time.Hour;
        _minutes=time.Minute;
        _seconds=time.Second;
    }
    float[] GetDefaultArray(int arrayLengths)
    {
        float[] array = new float[arrayLengths];
        for (int i = 0; i < arrayLengths; i++)
            array[i] = -1f;
        return array;
    }
    float[] SetArrayValues(Dictionary<int,int> dataMap,float[] arrayToFill)
    {
        Debug.Log("Player Scins Count: "+dataMap.Keys.Count+" Coin pool ccount: "+arrayToFill.Length);
        foreach (int id in dataMap.Keys)
        {
            arrayToFill[id] = dataMap[id];
        }
        return arrayToFill;
    }
    float[] SetArrayValues(Dictionary<int, float> dataMap, float[] arrayToFill)
    {
        foreach (int id in dataMap.Keys)
        {
            arrayToFill[id] = dataMap[id];
        }
        return arrayToFill;
    }
    float[] GetFarmTools(int farmToolPoolCount, Player player)
    {
        float[] farmToolsToSave=GetDefaultArray(farmToolPoolCount);
        Dictionary<int, int> farmTools = player.Farm;
        return SetArrayValues(farmTools,farmToolsToSave);
    }
    float[] GetCryptoWallet(int coinPoolCount, Player player)
    {
        float[] walletToSave = GetDefaultArray(coinPoolCount);
        Dictionary<int, float> wallet = player.CryptoWallet;
        return SetArrayValues(wallet,walletToSave);
    }
    float[] GetCoinInventory(int coinPoolCount, Player player)
    {
        Debug.Log("Coin Scins Inventory " + coinPoolCount);
        float[] inventoryToSave = GetDefaultArray(coinPoolCount);
        Dictionary<int, int> inventory = player.CoinSkinInventory;
        return SetArrayValues(inventory,inventoryToSave);
    }
    float[] GetTotalMinedCoinsAmount(int coinPoolCount, Player player)
    {
        float[] totalMinedCoinstToSave = GetDefaultArray(coinPoolCount);
        Dictionary<int, float> totalMinedCoins = player.MinedCoinsTotalAmounts;
        return SetArrayValues(totalMinedCoins,totalMinedCoinstToSave);
    }
    Dictionary<int, float> GetDataMap(float[] array)
    {
        Dictionary<int, float> data = new Dictionary<int, float>();
        for(int i = 0; i < array.Length; i++)
        {
            if((int)array[i]!=-1)
            {
                data.Add(i, array[i]);
            }
        }
        return data;
    }
    Dictionary<int, int> GetDataMapInt(float[] array)
    {
        Dictionary<int, int> data = new Dictionary<int,int>();
        for (int i = 0; i < array.Length; i++)
        {
            if ((int)array[i] != -1)
            {
                data.Add(i, (int)array[i]);
            }
        }
        return data;
    }
    public Dictionary<int,float> ExtractCryptoWalet()
    {
        return GetDataMap(_cyproWallet);
    }
    public Dictionary<int,float> ExtractTotalMinedCoinsAmount()
    {
        return GetDataMap(_totalMinedCoinsAmmount);
    }
    public Dictionary<int, int> ExtractFarmTools()
    {
        return GetDataMapInt(_farmTools);
    }
    public Dictionary<int, int> ExtractSkinInventory()
    {
        return GetDataMapInt(_coinInventory);
    }
    public DateTime GetTime()
    {
        return new DateTime(_year,_month,_day,_hours,_minutes,_seconds);
    }
    public List<DateTime> GetBoxesTimes()
    {
        List<DateTime> boxes = new List<DateTime>();
        Debug.Log(_boxTimers.Length);
        for(int i=0; i<_boxTimers.GetLength(0);i++)
        {
            Debug.Log($"Box Id {i} Year {_boxTimers[i, 0]} Month {_boxTimers[i, 1]} Day {_boxTimers[i, 2]} Hours {_boxTimers[i, 3]} Minutes {_boxTimers[i, 4]}");
            DateTime time=new DateTime(_boxTimers[i,0], _boxTimers[i, 1], _boxTimers[i, 2], _boxTimers[i, 3],_boxTimers[i, 4],0);
            boxes.Add(time);
        }
        return boxes;
    }
}
