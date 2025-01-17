﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlMovement : MonoBehaviour
{
    public float moveSpeed;

    private bool isReadyToWalk;
    private Rigidbody2D rb; 
    private int state;
    private int direction;
    private Animator animator;
    private bool m_has_saphire;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        isReadyToWalk = true;
        // 0 - idle, 1 - walk
        state = 0;
        direction = -1;
        m_has_saphire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReadyToWalk)
        {
            state = newState();

            switch (state)
            {
                // idle
                case 0:
                    rb.velocity = new Vector2(0, 0);
                    animator.SetInteger("state", 0);
                    break;

                // move
                case 1:
                    if (direction > 0)
                        animator.SetFloat("moveDirection", 1.0f);
                    if (direction < 0)
                        animator.SetFloat("moveDirection", 0.0f);
                    rb.velocity = new Vector2(direction * moveSpeed, 0);
                    animator.SetInteger("state", 1);
                    break;

                default:
                    break;
            }

            // Start movement
            StartCoroutine("walking");
        }
    }

    IEnumerator walking()
    {
        isReadyToWalk = false;
        yield return new WaitForSeconds(2.0f);
        isReadyToWalk = true;
        direction *= -1;
    }

    int newState()
    {
        return Random.Range(0, 2);
    }

    public void SetHasSaphire(bool state)
    {
        m_has_saphire = state;
    }
}
