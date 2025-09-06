using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamInventoryService : MonoBehaviour
{
    enum InventoryResultType
    {
        InventoryUpload,
        ItemTrigger,
        None
    }
    InventoryResultType _resultType=InventoryResultType.None;
    SteamInventoryResult_t m_SteamInventoryResult;
    SteamItemDetails_t[] m_SteamItemDetails;
    SteamItemDef_t[] m_SteamItemDef;
    uint _steamInvemtoryCount;

    protected Callback<SteamInventoryResultReady_t> m_SteamInventoryResultReady;
    protected Callback<SteamInventoryFullUpdate_t> m_SteamInventoryFullUpdate;
    protected Callback<SteamInventoryDefinitionUpdate_t> m_SteamInventoryDefinitionUpdate;
    protected Callback<SteamInventoryRequestPricesResult_t> m_SteamInventoryRequestPricesResult;

    private CallResult<SteamInventoryEligiblePromoItemDefIDs_t> OnSteamInventoryEligiblePromoItemDefIDsCallResult;
    private CallResult<SteamInventoryStartPurchaseResult_t> OnSteamInventoryStartPurchaseResultCallResult;
    private CallResult<SteamInventoryResult_t> OnSteamInventoryResult_CallResult;

    private void Awake()
    {
        m_SteamInventoryResult = SteamInventoryResult_t.Invalid;
        m_SteamItemDetails = null;
        m_SteamItemDef = null;
        m_SteamInventoryResultReady = Callback<SteamInventoryResultReady_t>.Create(OnSteamInventoryResultReady);
        m_SteamInventoryFullUpdate = Callback<SteamInventoryFullUpdate_t>.Create(OnSteamInventoryFullUpdate);
        m_SteamInventoryDefinitionUpdate = Callback<SteamInventoryDefinitionUpdate_t>.Create(OnSteamInventoryDefinitionUpdate);
        m_SteamInventoryRequestPricesResult = Callback<SteamInventoryRequestPricesResult_t>.Create(OnSteamInventoryRequestPricesResult);

        OnSteamInventoryEligiblePromoItemDefIDsCallResult = CallResult<SteamInventoryEligiblePromoItemDefIDs_t>.Create(OnSteamInventoryEligiblePromoItemDefIDs);
        OnSteamInventoryStartPurchaseResultCallResult = CallResult<SteamInventoryStartPurchaseResult_t>.Create(OnSteamInventoryStartPurchaseResult);

    }
    void DestroyResult()
    {
        if (m_SteamInventoryResult != SteamInventoryResult_t.Invalid)
        {
            SteamInventory.DestroyResult(m_SteamInventoryResult);
            print("SteamInventory.DestroyResult(" + m_SteamInventoryResult + ")");
            m_SteamInventoryResult = SteamInventoryResult_t.Invalid;
        }
    }
    private void OnApplicationQuit()
    {
        DestroyResult();
    }
    public void GetSteamInventroy()
    {
        if(SteamManager.Initialized)
        {
            SteamInventory.GetAllItems(out m_SteamInventoryResult);
            _resultType = InventoryResultType.InventoryUpload;
        }
    }
    void SetInventoryFromSteam()
    {
        Game game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        game.GetPlayer().CoinSkinInventory.Clear();
        game.GetPlayer().AddScinCoinToInevtory(2);
        SteamInventory.GetResultItems(m_SteamInventoryResult, null, ref _steamInvemtoryCount);
        Debug.Log(_steamInvemtoryCount);
        m_SteamItemDetails = new SteamItemDetails_t[_steamInvemtoryCount];
        SteamInventory.GetResultItems(m_SteamInventoryResult, m_SteamItemDetails, ref _steamInvemtoryCount);
        foreach (SteamItemDetails_t item in m_SteamItemDetails)
        {
            Debug.Log(item.m_iDefinition.m_SteamItemDef);
            game.GetPlayer().AddScinCoinToInevtory((int)item.m_iDefinition.m_SteamItemDef);
        }
        _resultType=InventoryResultType.None;
    }
    void OnSteamInventoryResultReady(SteamInventoryResultReady_t pCallback)
    {
        Debug.Log("[" + SteamInventoryResultReady_t.k_iCallback + " - SteamInventoryResultReady] - " + pCallback.m_handle + " -- " + pCallback.m_result);

        m_SteamInventoryResult = pCallback.m_handle;
        if(_resultType==InventoryResultType.ItemTrigger)
        {
            AddItem();
        }
    }

    void OnSteamInventoryFullUpdate(SteamInventoryFullUpdate_t pCallback)
    {
        Debug.Log("[" + SteamInventoryFullUpdate_t.k_iCallback + " - SteamInventoryFullUpdate] - " + pCallback.m_handle);

        m_SteamInventoryResult = pCallback.m_handle;
/*        switch (_resultType)
        {
            case InventoryResultType.None:
                break;
            case InventoryResultType.ItemTrigger:

                break;
            case InventoryResultType.InventoryUpload:
                
                break;
        }*/

        SetInventoryFromSteam();
    }

    void OnSteamInventoryDefinitionUpdate(SteamInventoryDefinitionUpdate_t pCallback)
    {
        Debug.Log("[" + SteamInventoryDefinitionUpdate_t.k_iCallback + " - SteamInventoryDefinitionUpdate]");
    }

    void OnSteamInventoryEligiblePromoItemDefIDs(SteamInventoryEligiblePromoItemDefIDs_t pCallback, bool bIOFailure)
    {
        Debug.Log("[" + SteamInventoryEligiblePromoItemDefIDs_t.k_iCallback + " - SteamInventoryEligiblePromoItemDefIDs] - " + pCallback.m_result + " -- " + pCallback.m_steamID + " -- " + pCallback.m_numEligiblePromoItemDefs + " -- " + pCallback.m_bCachedData);

        uint ItemDefIDsArraySize = (uint)pCallback.m_numEligiblePromoItemDefs;
        SteamItemDef_t[] ItemDefIDs = new SteamItemDef_t[ItemDefIDsArraySize];
        bool ret = SteamInventory.GetEligiblePromoItemDefinitionIDs(pCallback.m_steamID, ItemDefIDs, ref ItemDefIDsArraySize);
        print("SteamInventory.GetEligiblePromoItemDefinitionIDs(pCallback.m_steamID, ItemDefIDs, ref ItemDefIDsArraySize) - " + ret + " -- " + ItemDefIDsArraySize);
    }

    void OnSteamInventoryStartPurchaseResult(SteamInventoryStartPurchaseResult_t pCallback, bool bIOFailure)
    {
        Debug.Log("[" + SteamInventoryStartPurchaseResult_t.k_iCallback + " - SteamInventoryStartPurchaseResult] - " + pCallback.m_result + " -- " + pCallback.m_ulOrderID + " -- " + pCallback.m_ulTransID);
    }
    void OnSteamInventoryRequestPricesResult(SteamInventoryRequestPricesResult_t pCallback)
    {
        Debug.Log("[" + SteamInventoryRequestPricesResult_t.k_iCallback + " - SteamInventoryRequestPricesResult] - " + pCallback.m_result + " -- " + pCallback.m_rgchCurrency);
    }
    public void TriggerDrop(int itemGeneratotId)
    {
        _resultType = InventoryResultType.ItemTrigger;
        SteamItemDef_t itemToAdd = (SteamItemDef_t)itemGeneratotId;
        Debug.Log(itemToAdd.m_SteamItemDef);
        SteamInventory.TriggerItemDrop(out m_SteamInventoryResult, itemToAdd);
    }
    void AddItem()
    {
        Game game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        uint resultLength = 1;
        SteamInventory.GetResultItems(m_SteamInventoryResult, null, ref resultLength);
        Debug.Log(resultLength);
        if (resultLength != 0)
        {
            m_SteamItemDetails = new SteamItemDetails_t[resultLength];
            SteamInventory.GetResultItems(m_SteamInventoryResult, m_SteamItemDetails, ref resultLength);
            Debug.Log(m_SteamItemDetails[0]);
            game.Ui.ShopUi.ShowBoxRewardPanel(game.CoinPool.GetCoinFromId(m_SteamItemDetails[0].m_iDefinition.m_SteamItemDef));
            GetSteamInventroy();
        }
        else
        {
            game.GetPlayer().AddCoin(game.CoinPool.GetCoinFromId(0), 500);
            game.Ui.ShopUi.ShowBoxRewardPanel(game.CoinPool.GetCoinFromId(0),500);

        }
        _resultType =InventoryResultType.None;
    }
}
