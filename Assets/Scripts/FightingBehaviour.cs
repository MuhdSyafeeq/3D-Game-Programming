using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingBehaviour : MonoBehaviour
{
    bool canAttack = false;
    [SerializeField] MoveCharacter player;
    [SerializeField] Health playerHealth;

    [SerializeField] int health;
    [SerializeField] int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            LaunchAttack();

            if (player.isAttack)
                ReceivedDamage();
        }

        if (health == 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
            canAttack = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
            canAttack = false;
    }

    void ReceivedDamage()
    {
        health -= 2;
        Debug.Log("Mushroom received damage");
    }

    void LaunchAttack()
    {
        playerHealth.reduceHealth(damage);
        Debug.Log("Mushroom attacking");
    }
}
