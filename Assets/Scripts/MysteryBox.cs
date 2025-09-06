using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    Game _game;
    
    // Start is called before the first frame update
    void Start()
    {
        _game=GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool OpenBox(int id)
    {
        int dice = UnityEngine.Random.Range(0, 100);
        Coin stablecoin = _game.CoinPool.GetCoinFromId(0);
        Coin coinScin = _game.CoinPool.GetRandomCoin(id);
        int stableCoinAmount = (id + 1) * 100 + dice;

        if(dice<70)
        {
            _game.GetPlayer().AddCoin(stablecoin, stableCoinAmount);
            _game.Ui.ShopUi.ShowBoxRewardPanel(stablecoin,stableCoinAmount);
            Debug.Log("StableCoin " + stableCoinAmount.ToString());
            return false;
        }
        else
        {
            if (SteamManager.Initialized)
            {
                _game.GetRewardFromBox(id);
                return true;
            }
            else
            {
                _game.GetPlayer().AddCoin(stablecoin, stableCoinAmount);
                _game.Ui.ShopUi.ShowBoxRewardPanel(stablecoin, stableCoinAmount);
                return false;
            }
        }

    }
}
