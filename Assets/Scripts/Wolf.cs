using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    [SerializeField] Animation wAnimation;

    enum State
    {
        Idle,
        Walk,
        Chase,
        Jump,
        Attack,
    }

    [SerializeField] private State Wolve = State.Idle;
    [SerializeField] private float Timer;
    [SerializeField] private bool isChange = false;

    private void Awake()
    {
        Wolve = State.Idle;
        wAnimation.PlayQueued("idle", QueueMode.PlayNow);
        Timer = 75f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer <= 0)
        {
            if (Wolve == State.Idle)
            {
                Wolve = State.Walk;
                Timer = 50f;

                isChange = true;
            }
            else if (Wolve == State.Walk) //Create an Opening of Chase
            {
                Wolve = State.Jump;
                Timer = 2.03f;

                isChange = true;
            }
            else if(Wolve == State.Jump)
            {
                Wolve = State.Chase;
                Timer = 75f;

                isChange = true;
            }
        }
        Timer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if(Wolve == State.Walk && isChange == true)
        {
            wAnimation.PlayQueued("walk", QueueMode.PlayNow);
            Debug.Log(wAnimation.name);
            isChange = false;
        }
        if (Wolve == State.Jump && isChange == true)
        {
            wAnimation.PlayQueued("jump", QueueMode.PlayNow);
            Debug.Log(wAnimation.name);
            isChange = false;
        }
        if (Wolve == State.Chase && isChange == true)
        {
            wAnimation.PlayQueued("run", QueueMode.PlayNow);
            Debug.Log(wAnimation.name);
            isChange = false;
        }
    }
}
