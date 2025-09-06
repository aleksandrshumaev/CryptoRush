using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CoinPool : MonoBehaviour
{
    [SerializeField] List<Coin> _coinPool;
    // Start is called before the first frame update
    public Coin GetCoinFromId(int id)
    {
        foreach(Coin coin in _coinPool)
        {
            if(coin.Id == id)
            {
                return coin;
            }
        }
        return null;
    }
    public int GetPoolCount()
    {
        return _coinPool.Count;
    }
    List<Coin> GetCoinsWithRariety(Coin.CoinRariety rariety)
    {
        List<Coin> coinList = new List<Coin>();
        foreach(Coin coin in _coinPool)
        {
            if(coin.Rariety==rariety)
            {
                coinList.Add(coin);
                //Debug.Log(coin.Name);
            }
        }
        return coinList;
    }

    public Coin GetRandomCoin(int id)
    {
        switch (id)
        {
            case 0:
                return GetCommonCoin();
            case 1:
                return GetUnCommonCoin();
            case 2:
                return GetRareCoin();
            case 3:
                return GetMythicalCoin();
            case 4:
                return GetLegendaryCoin();
            default:
                return null;
        }
    }
    public Coin GetCommonCoin()
    {
        List<Coin> coins = GetCoinsWithRariety(Coin.CoinRariety.Common);
        return coins.OrderBy(x => UnityEngine.Random.Range(0, coins.Count)).ToList()[0];

    }
    public Coin GetUnCommonCoin()
    {
        List<Coin> coins = GetCoinsWithRariety(Coin.CoinRariety.Uncommon);
        return (Coin)coins.OrderBy(x => UnityEngine.Random.Range(0, coins.Count)).ToList()[0];

    }
    public Coin GetRareCoin()
    {
        List<Coin> coins = GetCoinsWithRariety(Coin.CoinRariety.Rare);
        return (Coin)coins.OrderBy(x => UnityEngine.Random.Range(0, coins.Count)).ToList()[0];

    }
    public Coin GetMythicalCoin()
    {
        List<Coin> coins = GetCoinsWithRariety(Coin.CoinRariety.Mythical);
        return (Coin)coins.OrderBy(x => UnityEngine.Random.Range(0, coins.Count)).ToList()[0];

    }
    public Coin GetLegendaryCoin()
    {
        List<Coin> coins = GetCoinsWithRariety(Coin.CoinRariety.Legendary);
        return (Coin)coins.OrderBy(x => UnityEngine.Random.Range(0, coins.Count)).ToList()[0];

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}