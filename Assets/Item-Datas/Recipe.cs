using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/Recipe")]
public class Recipe : ScriptableObject
{
    new public string name = "New Recipe";

    [SerializeField] public Item[] itemObj;

    
    [SerializeField] public Item FinalProduct;
    [SerializeField] public Sprite icon = null;
}
