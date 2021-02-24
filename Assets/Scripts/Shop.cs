using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject ShopUI;
    [SerializeField] GameObject ConfirmPurchase;
    [SerializeField] GameObject InsufficientCoinWarning;
    [SerializeField] List<Item> ShopList = new List<Item>();
    [SerializeField] private int itemArrNum = -1;
    [SerializeField] Currency playerCurrency;

    string itemPurchase;
    int coinRequired;

    public void setItemNum(int num)
    {
        itemArrNum = num;
    }

    // Start is called before the first frame update
    void Start()
    {
        ShopUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShopUI.activeSelf)
        {
            MoveCharacter.isPaused = true;
            Time.timeScale = 0;
        }
    }

    public void ClickPurchase()
    {
        ConfirmPurchase.SetActive(true);
        ConfirmPurchase.GetComponentInChildren<Text>().text = "Purchasing " + ShopList[itemArrNum].name + "?"; //itemPurchase
    }

    public void Confirmation(string @context)
    {
        playerCurrency = PlayerCamera.instance.GetComponentInChildren<Canvas>().GetComponentInChildren<Currency>();
        if (@context == "Yes")
        {
            coinRequired = (int)ShopList[itemArrNum].buyPrice;
            if (playerCurrency.checkBalance() >= coinRequired)
            {
                playerCurrency.useCoin(coinRequired);
                Debug.Log("Add " + itemPurchase + " to inventory");
                Item shopItem = ShopList[itemArrNum];
                Inventory.instance.Add(shopItem);
            }
            else
            {
                InsufficientCoinWarning.SetActive(true);
            }
                
        }
        else
        {
            ConfirmPurchase.SetActive(false);
            InsufficientCoinWarning.SetActive(false);
        }
    }

    public void CloseShop()
    {
        ShopUI.SetActive(false);
        MoveCharacter.isPaused = false;
        Time.timeScale = 1;
    }
}
