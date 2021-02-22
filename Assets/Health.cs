using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.value = healthBar.maxValue;
    }

    public void setStamina(float amount)
    {
        healthBar.value = amount;
    }

    public void addStamina(float amount)
    {
        healthBar.value += amount;
    }

    public void reduceStamina(float amount)
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
