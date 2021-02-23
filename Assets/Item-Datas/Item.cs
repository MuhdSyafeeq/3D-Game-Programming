using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";

    [Header("Item Attribute")]
    [SerializeField] public GameObject itemObj;
    [SerializeField] public Sprite icon = null;
    [SerializeField] public bool isIngredient = false;

    [Header("Shop Attribute")]
    [SerializeField] public float buyPrice = 0;
    [SerializeField] public float sellPrice = 0;
}
