using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    Slider coinBar;
    [SerializeField] int InitialMoney = 0;
    [SerializeField] Text currentMoney;
    [SerializeField] const int maxMoney = 1000;

    // Start is called before the first frame update
    void Start()
    {
        coinBar = GetComponent<Slider>();
        coinBar.maxValue = maxMoney;
        coinBar.value = InitialMoney;
    }

    private void Update()
    {
        currentMoney.text = coinBar.value.ToString();
    }

    public void setHealth(int amount)
    {
        coinBar.value = amount;
    }

    public void addCoin(int amount)
    {
        coinBar.value += amount;
    }

    public void useCoin(int amount)
    {
        coinBar.value -= amount;
    }

    public int checkBalance()
    {
        return (int)coinBar.value;
    }

    public bool checkCoins()
    {
        if (coinBar.value > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
