using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] int rotationSpeed = 5;
    [SerializeField] bool isAquiring = false;
    [SerializeField] GameObject theObject;

    void Update()
    {
        if (isAquiring && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"Picking up item {item.name}");
            bool canPickedUp = Inventory.instance.Add(item);

            if(canPickedUp) Destroy(theObject);

        }

        transform.Rotate(
            this.transform.rotation.x,
            rotationSpeed * Time.deltaTime,
            this.transform.rotation.z
            );
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isAquiring = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isAquiring = false;
        }
    }
}
