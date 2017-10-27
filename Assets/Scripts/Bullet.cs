﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    [SerializeField]
    float lifeTime = 3.0f;

    public event Action<Bullet> OnTimeout;

    private Rigidbody2D m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();    
    }

    public void Fire(Vector3 from, Vector2 velocity)
    {
        transform.position = from;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x));
        StartCoroutine(CoroutineHelper.ExecuteAfterFrame(() => { m_rigidbody.AddForce(velocity, ForceMode2D.Impulse); }));

        // Cleanup bullet after life time expires
        StartCoroutine(CoroutineHelper.ExecuteAfterTime(lifeTime, () =>
        {
            if (OnTimeout != null)
                OnTimeout(this);
        }));
    }
}
