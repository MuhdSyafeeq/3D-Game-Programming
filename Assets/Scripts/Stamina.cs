using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    Slider staminaBar;
    // Start is called before the first frame update
    void Start()
    {
        staminaBar = GetComponent<Slider>();
        staminaBar.value = staminaBar.maxValue;
    }

    public void refillStamina()
    {
        staminaBar.value = staminaBar.maxValue;
    }

    public void setStamina(float amount)
    {
        staminaBar.value = amount;
    }

    public void addStamina(float amount)
    {
        staminaBar.value += amount;
    }

    public void reduceStamina(float amount)
    {
        staminaBar.value -= amount;
    }

    public bool checkStamina()
    {
        if (staminaBar.value > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
