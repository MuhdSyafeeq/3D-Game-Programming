using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private bool isNearChest = false, pressOnce = false, openAnim = false;
    [SerializeField] Transform current_cover;
    [SerializeField] List<Item> RandomItem;

    [SerializeField] int[] coinLimiter;
    [SerializeField] Currency _currency;
    [SerializeField] Animator p_anime;

    [SerializeField] AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isNearChest = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isNearChest = false;
            //openAnim = false;
            //pressOnce = false;

            //current_cover.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    private void LateUpdate()
    {
        if (openAnim)
        {
            if(current_cover.rotation.x <= 0 && current_cover.rotation.x >= -0.5)
            {
                //Debug.Log(current_cover.rotation.x);
                audio.Play();
                current_cover.Rotate(-(Time.deltaTime * 120), 0, 0);
            }
        }

        if(Input.GetKeyDown(KeyCode.E) && !(Input.GetKeyUp(KeyCode.E)) && isNearChest)
        {
            if (!pressOnce)
            {
                if(p_anime == null) { p_anime = MoveCharacter.instance.GetComponentInChildren<Animator>(); }
                if(audio == null) { audio = GetComponent<AudioSource>(); }
                pressOnce = true;
                openAnim = true;
                p_anime.Play("Search", -1, 0f);
                getItem();
            }
        }
    }

    public void getItem()
    {
        int numArr = Random.Range(0, 8);
        Debug.Log(RandomItem[numArr].name);
        audio.PlayOneShot(audio.clip);
        if (RandomItem[numArr].name == "Coin")
        {
            if(_currency == null) { _currency = PlayerCamera.instance.GetComponentInChildren<Canvas>().GetComponentInChildren<Currency>(); }
            
            int randomCoin = Random.Range(0, 3);
            _currency.addCoin(coinLimiter[randomCoin]);
        }
        else if(RandomItem[numArr].name != "Coin")
        {
            Inventory.instance.inventories.Add(RandomItem[numArr]);
        }
    }
}
