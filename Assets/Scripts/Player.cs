using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Dictionary<int, float> _cryptoWallet = new();
    [SerializeField] Dictionary<int, int> _farm = new();
    [SerializeField] Dictionary<int,int> _coinSkinInventory=new();
    [SerializeField] Dictionary<int, float> _minedCoinsTotalAmounts = new();
    Game _game;
    float _hashRatePerSec=0;

    public float HashRatePerSec { get => _hashRatePerSec; }
    public Dictionary<int, float> CryptoWallet { get => _cryptoWallet;}
    public Dictionary<int, int> CoinSkinInventory { get => _coinSkinInventory; }
    public Dictionary<int, int> Farm { get => _farm;  }
    public Dictionary<int, float> MinedCoinsTotalAmounts { get => _minedCoinsTotalAmounts; }

    public void Start()
    {
        _game=GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        AddCoin(_game.CurrentCoin, 0);
        AddScinCoinToInevtory(2);
        CalcuateHashPerSec();
    }
    public void AddScinCoinToInevtory(int id)
    {
        if(_coinSkinInventory.ContainsKey(id))
        {
            _coinSkinInventory[id]++;
        }
        else
        {
            _coinSkinInventory.Add(id, 1);
        }
    }
    public void AddCoin(Coin coin,float value)
    {
        int id = coin.Id;
        if(_cryptoWallet.ContainsKey(id))
        {
            _cryptoWallet[id] += value;
        }
        else
        {
            _cryptoWallet.Add(id, value);
        }
        AddCoinToTotal(coin,value);
        _game.Ui.UpdateUi();
    }
    public void AddCoinToTotal(Coin coin, float value)
    {
        int id = coin.Id;
        if (_minedCoinsTotalAmounts.ContainsKey(id))
        {
            _minedCoinsTotalAmounts[id] += value;
        }
        else
        {
            _minedCoinsTotalAmounts.Add(id, value);
        }
        coin.CalculateNeededHashrate(_minedCoinsTotalAmounts[id]);
        _game.Ui.CoinUi.SetNeededHasrateText();
    }
    public float GetCoinAmount(Coin coin)
    {
        if(_cryptoWallet.ContainsKey(coin.Id))
        {
            return _cryptoWallet[coin.Id];
        }
        return 0f;
    }

    public int GetFarmToolAmounntFromID(int toolIdForSlot)
    {
        if(_farm.ContainsKey(toolIdForSlot))
        {
            return _farm[toolIdForSlot];
        }
        return 0;
    }

    public bool RemoveCoin(Coin coin, float value)
    {
        int id = coin.Id;
        if (_cryptoWallet.ContainsKey(id))
        {
            if(_cryptoWallet[id]>=value)
            {
                _cryptoWallet[id]-=value;
                _game.Ui.UpdateUi();
                return true;
            }
        }
        return false;
    }
    public bool RemoveCoin(int id, float value)
    {
        if (_cryptoWallet.ContainsKey(id))
        {
            if (_cryptoWallet[id] >= value)
            {
                _cryptoWallet[id] -= value;
                _game.Ui.UpdateUi();
                return true;
            }
        }
        return false;
    }
    public void AddTool(FarmTool tool, int value)
    {
        int id = tool.Id;
        if (_farm.ContainsKey(id))
        {
            _farm[id] += value;
        }
        else
        {
            _farm.Add(id, value);
        }
        CalcuateHashPerSec();
        _game.Ui.UpdateUi();
    }
    public void CalcuateHashPerSec()
    {
        _hashRatePerSec = 0;
        foreach (int id in _farm.Keys)
        {
            Debug.Log(_farm[id]);
            _hashRatePerSec+= _farm[id] * _game.FarmToolPool.GetToolFromId(id).Efficiensy;
        }
    }
    public void LoadPlayerData(PlayerData data)
    {
        _coinSkinInventory=data.ExtractSkinInventory();
        _farm = data.ExtractFarmTools();
        _cryptoWallet=data.ExtractCryptoWalet();
        _minedCoinsTotalAmounts = data.ExtractTotalMinedCoinsAmount();
        CalcuateHashPerSec();
        if(_minedCoinsTotalAmounts.ContainsKey(_game.CurrentCoin.Id))
            _game.CurrentCoin.CalculateNeededHashrate(_minedCoinsTotalAmounts[_game.CurrentCoin.Id]);
        _game.Ui.CoinUi.SetNeededHasrateText();
        _game.Ui.UpdateUi();
    }
}
