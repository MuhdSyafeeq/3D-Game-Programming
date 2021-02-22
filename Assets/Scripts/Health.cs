using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    Slider healthBar;
    [SerializeField] int maxHealth;
    //int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void setHealth(float amount)
    {
        healthBar.value = amount;
    }

    public void addHealth(float amount)
    {
        healthBar.value += amount;
    }

    public void reduceHealth(float amount)
    {
        healthBar.value -= amount;
    }

    public bool checkStamina()
    {
        if (healthBar.value > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
