using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private Image[] img;
    [SerializeField] private Image[] Hotkeys;
    [SerializeField] int Hotkey = 1; // Default Hotkey 1

    [SerializeField] Transform _Player;

    public int getHotkey()
    {
        return Hotkey;
    }

    public void getHotkeys(Image[] imges)
    {
        imges = Hotkeys;
    }

    void Start()
    {
        Hotkeys[Hotkey - 1].color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if(MoveCharacter.isPaused == false)
        {
            if (Input.GetKeyDown(key: KeyCode.Q))
            {
                if (Inventory.instance.inventories.Count != 0)
                {
                    if (Hotkey <= Inventory.instance.inventories.Count)
                    {
                        if (Inventory.instance.inventories[Hotkey - 1] != null)
                        {
                            Debug.Log($"System: -> Item: ({Inventory.instance.inventories[Hotkey - 1].name}) dropped.");
                            Item currentItem = Inventory.instance.inventories[Hotkey - 1];

                            var theObj = Instantiate(currentItem.itemObj, _Player.position, Quaternion.identity);
                            if (currentItem.name == "Mushroom") { theObj.transform.localScale = new Vector3(5, 5, 5); }
                            theObj.AddComponent<ItemPickup>();
                            theObj.GetComponent<ItemPickup>().item = Inventory.instance.inventories[Hotkey - 1];
                            theObj.AddComponent<SphereCollider>();
                            theObj.GetComponent<SphereCollider>().radius = 1.5f;
                            theObj.GetComponent<SphereCollider>().isTrigger = true;

                            Inventory.instance.Remove(currentItem);
                            cleanUp();
                        }
                    }
                    else { Debug.Log($"System: -> Are you trying to drop something?"); }

                }
                else { Debug.Log($"System: -> Are you trying to drop something?"); }
            }

            #region HotKeys
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                Hotkey += 1;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                Hotkey -= 1;
            }

            if (Input.GetKeyDown(key: KeyCode.Alpha1))
            {
                Hotkey = 1;
            }
            else if (Input.GetKeyDown(key: KeyCode.Alpha2))
            {
                Hotkey = 2;
            }
            else if (Input.GetKeyDown(key: KeyCode.Alpha3))
            {
                Hotkey = 3;
            }
            else if (Input.GetKeyDown(key: KeyCode.Alpha4))
            {
                Hotkey = 4;
            }
            else if (Input.GetKeyDown(key: KeyCode.Alpha5))
            {
                Hotkey = 5;
            }
            else if (Input.GetKeyDown(key: KeyCode.Alpha6))
            {
                Hotkey = 6;
            }
            else if (Input.GetKeyDown(key: KeyCode.Alpha7))
            {
                Hotkey = 7;
            }
            #endregion
        }
    }

    void cleanUp()
    {
        for (int i = 0; i < 7; i++)
        {
            if(i < Inventory.instance.inventories.Count)
            {
                if (Inventory.instance.inventories[i] != null)
                {
                    img[i].sprite = Inventory.instance.inventories[i].icon;
                }
                else
                {
                    img[i].sprite = null;
                }
            }
            else
            {
                img[i].sprite = null;
            }
        }
    }

    void FixedUpdate()
    {
        if(Hotkey < 1)
        {
            Hotkey = 7;
        }
        if(Hotkey > 7)
        {
            Hotkey = 1;
        }
    }

    void LateUpdate()
    {
        for(int i = 0; i < Hotkeys.Length; i++)
        {
            if (i == Hotkey-1) { Hotkeys[i].color = Color.white; }
            else { Hotkeys[i].color = Color.grey; }
        }

        if (Inventory.instance != null && Inventory.instance.inventories.Count != 0)
        {
            cleanUp();
        }
        else
        {
            foreach (Image @img in img)
            {
                @img.sprite = null;
            }
        }
    }
}
