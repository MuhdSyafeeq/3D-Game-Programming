using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    [SerializeField] int initialCoin;
    [SerializeField] Text coinText;
    public static int currentCoin;
    
    // Start is called before the first frame update
    void Start()
    {
        currentCoin = initialCoin;
    }

    private void Update()
    {
        coinText.text = currentCoin.ToString();
    }

    public void AddCoin(int coin)
    {
        currentCoin += coin;
    }

    public void UseCoin(int coin)
    {
        currentCoin -= coin;
    }
}
