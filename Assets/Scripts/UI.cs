using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject[] Outer;

    [SerializeField] Renderer InnerMaterial, OuterMaterial;
    [SerializeField] Material Inners, Outers;

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.W))
        {
            Outer[0].GetComponent<Renderer>().material = Inners;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Outer[1].GetComponent<Renderer>().material = Inners;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Outer[2].GetComponent<Renderer>().material = Inners;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Outer[3].GetComponent<Renderer>().material = Inners;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            Outer[0].GetComponent<Renderer>().material = Outers;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            Outer[1].GetComponent<Renderer>().material = Outers;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Outer[2].GetComponent<Renderer>().material = Outers;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Outer[3].GetComponent<Renderer>().material = Outers;
        }
    }
}
