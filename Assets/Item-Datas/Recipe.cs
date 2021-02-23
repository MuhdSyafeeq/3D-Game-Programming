using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/Recipe")]
public class Recipe : ScriptableObject
{
    new public string name = "New Recipe";

    [Header("Ingredient List")]
    [SerializeField] public Item[] itemObj;

    [Header("Combined Ingredient")]
    [SerializeField] public Item FinalProduct;
    [SerializeField] public Sprite icon = null;
}
