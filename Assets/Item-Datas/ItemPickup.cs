using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] public Item item;
    [SerializeField] int rotationSpeed = 90;
    [SerializeField] bool isAquiring = false;
    [SerializeField] GameObject theObject;
    float maxUp = 0.5f, Step = 0f, Offset;
    Transform keepData;

    private void Awake()
    {
        keepData = this.transform;
        Offset = transform.position.y + maxUp;
    }


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

        Step += 0.01f;
        //Make sure Steps value never gets too out of hand 
        if (Step > 999999) { Step = 1; }
        //Float up and down along the y axis,
        
        this.transform.position = new Vector3(
            this.transform.position.x,
            Mathf.Sin(Step) + Offset,
            this.transform.position.z
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

    void LateUpdate()
    {
        if(theObject == null)
        {
            theObject = this.gameObject;
        }
    }
}
