using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject[] Outer;

    [SerializeField] Renderer InnerMaterial, OuterMaterial;

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.W))
        {
            Outer[0].GetComponent<Renderer>().material = InnerMaterial.material;
            Debug.Log($"In -> {KeyCode.W}");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Outer[1].GetComponent<Renderer>().material = InnerMaterial.material;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Outer[2].GetComponent<Renderer>().material = InnerMaterial.material;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Outer[3].GetComponent<Renderer>().material = InnerMaterial.material;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            Outer[0].GetComponent<Renderer>().material = OuterMaterial.material;
            Debug.Log($"Out -> {KeyCode.W}");
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            Outer[1].GetComponent<Renderer>().material = OuterMaterial.material;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Outer[2].GetComponent<Renderer>().material = OuterMaterial.material;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Outer[3].GetComponent<Renderer>().material = OuterMaterial.material;
        }
    }
}
