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
    [SerializeField] Animator animator;
    bool currentAnimating = false;
    bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        animator.speed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            if (!currentAnimating)
            {
                animator.Play("Taunt");
                currentAnimating = true;
            }
                
            if (!isAttacking)
            {
                Invoke("LaunchAttack", 3);
                isAttacking = true;
            }                

            if (player.isAttack)
                ReceivedDamage();
        }

        if (health == 0)
        {
            animator.Play("Death");
            Destroy(this.gameObject, 2);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            canAttack = true;
            transform.LookAt(collider.transform.position);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            canAttack = false;
            currentAnimating = false;
            animator.Play("Idle");
        } 
    }

    void ReceivedDamage()
    {
        health -= 2;
        animator.Play("Receive-Hit");
        Debug.Log("Mushroom received damage");
    }

    void LaunchAttack()
    {
        playerHealth.reduceHealth(damage);
        Debug.Log("Mushroom attacking");
        isAttacking = false;
    }
}
