using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cook : MonoBehaviour
{
    string itemName;
    GameObject item;

    // Start is called before the first frame update

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cooking()
    {
        itemName = GetComponentInChildren<Image>().sprite.name;
        item = Instantiate(GameObject.Find(itemName), new Vector3(0.9f, 0, 0), Quaternion.identity);
    }
}
