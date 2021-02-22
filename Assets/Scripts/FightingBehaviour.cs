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
    [SerializeField] Item Items;
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
            Invoke("dropItem", 2f);
            Destroy(this.gameObject, 2f);
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
    }

    void LaunchAttack()
    {
        playerHealth.reduceHealth(damage);
        isAttacking = false;
    }

    void dropItem()
    {
        var _Items = Instantiate(
            Items.itemObj,
            new Vector3(
                this.transform.position.x,
                this.transform.position.y + 0.75f,
                this.transform.position.z
            ),
            Quaternion.identity
        );
        _Items.transform.localScale = new Vector3(5, 5, 5);
        _Items.AddComponent<ItemPickup>();
        _Items.GetComponent<ItemPickup>().item = Items;
        _Items.AddComponent<SphereCollider>();
        _Items.GetComponent<SphereCollider>().radius = 1.5f;
        _Items.GetComponent<SphereCollider>().isTrigger = true;
    }
}
