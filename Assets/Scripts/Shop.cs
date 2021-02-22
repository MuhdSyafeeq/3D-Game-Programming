using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] Canvas ShopUI;
    [SerializeField] GameObject ConfirmPurchase;
    [SerializeField] GameObject InsufficientCoinWarning;
    [SerializeField] List<Item> ShopList = new List<Item>();
    [SerializeField] private int itemArrNum = -1;
    [SerializeField] Currency playerCurrency;

    string itemPurchase;
    int coinRequired;
    bool isShop = false;

    public void setItemNum(int num)
    {
        itemArrNum = num;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isShop)
        {
            ShopUI.enabled = true;
        }
    }

    public void ClickPurchase()
    {
        //itemPurchase = transform.GetChild(1).GetComponent<Text>().text.ToString();
        //coinRequired = int.Parse(transform.GetChild(3).GetComponent<Text>().text.ToString());

        ConfirmPurchase.SetActive(true);
        //ConfirmPurchase.GetComponentInChildren<Text>().text = "Purchasing " + ShopList[itemArrNum].name + "?"; //itemPurchase
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
        isShop = false;
        ShopUI.enabled = false;
    }
}
