using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public enum CoinRariety
    {
        Common=0,
        Uncommon=1,
        Rare=2,
        Mythical=3,
        Legendary=4,
        Special=5
    }
    [SerializeField] int _id;
    [SerializeField] string _name;
    [SerializeField] CoinRariety _rariety;
    [SerializeField] int _neededHashRate;
    [SerializeField] int _initialNeededHashRate;
    [SerializeField] float _stablePrice;
    [SerializeField] float _mainPrice;
    [SerializeField] int _halfingTrashHold;


    public string Name { get => _name; }
    public int NeededHashRate { get => _neededHashRate; set => _neededHashRate = value; }
    public float StablePrice { get => _stablePrice; set => _stablePrice = value; }
    public float MainPrice { get => _mainPrice; set => _mainPrice = value; }
    public int Id { get => _id; set => _id = value; }
    public CoinRariety Rariety { get => _rariety; }
    public int InitialNeededHashRate { get => _initialNeededHashRate; set => _initialNeededHashRate = value; }
    void Start()
    {
        _neededHashRate = _initialNeededHashRate;
        CalculateNeededHashrate();
    }
    public void CalculateNeededHashrate(float minedCoins = 0f)
    {
        if (Id != 0 && Id != 1)
        {
            Debug.Log(minedCoins % _halfingTrashHold + " " + (int)minedCoins / _halfingTrashHold);
            if (minedCoins % _halfingTrashHold == 0)
            {
                int halfingPower = (int)minedCoins / _halfingTrashHold;
                Debug.Log("Powering " + Mathf.Pow(2, halfingPower));
                _neededHashRate = Mathf.FloorToInt(_initialNeededHashRate * Mathf.Pow(2, halfingPower));
                Debug.Log("Halfing " + _neededHashRate);
            }
        }

    }
}
