using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] Canvas ShopUI;
    [SerializeField] Image ConfirmPurchase;
    [SerializeField] Image InsufficientCoinWarning;
    Currency playerCurrency;

    string itemPurchase;
    int coinRequired;
    bool isShop = false;

    // Start is called before the first frame update
    void Start()
    {
        playerCurrency = PlayerCamera.instance.GetComponentInChildren<Canvas>().GetComponentInChildren<Currency>();
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
        itemPurchase = transform.GetChild(1).GetComponent<Text>().text.ToString();
        coinRequired = int.Parse(transform.GetChild(3).GetComponent<Text>().text.ToString());

        ConfirmPurchase.enabled = true;
        ConfirmPurchase.GetComponentInChildren<Text>().text = "Purchasing " + itemPurchase + "?";
    }

    public void Confirmation()
    {
        if (this.name == "YesBtn")
        {
            if (Currency.currentCoin >= coinRequired)
            {
                playerCurrency.UseCoin(coinRequired);
                Debug.Log("Add " + itemPurchase + " to inventory");
            }
            else
            {
                InsufficientCoinWarning.enabled = true;
            }
                
        }
        else
        {
            ConfirmPurchase.enabled = false;
            InsufficientCoinWarning.enabled = false;
        }
    }

    public void CloseShop()
    {
        isShop = false;
        ShopUI.enabled = false;
    }
}
