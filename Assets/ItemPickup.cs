using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] int rotationSpeed = 90;
    [SerializeField] bool isAquiring = false;
    [SerializeField] GameObject theObject;
    float maxUp = 0.5f, maxDown = -0.5f;

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

        if(this.transform.position.y != this.transform.position.y + maxUp)
        {
            transform.position = new Vector3(
            this.transform.position.x,
            this.transform.position.y + (maxUp * Time.deltaTime),
            this.transform.position.z
            );
        }
        else
        {
            transform.position = new Vector3(
            this.transform.position.x,
            this.transform.position.y + (maxDown * Time.deltaTime),
            this.transform.position.z
            );
        }
        
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

    void LateUpdate()
    {
        if(theObject == null)
        {
            theObject = this.gameObject;
        }
    }
}
