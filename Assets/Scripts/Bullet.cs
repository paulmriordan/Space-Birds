using System.Collections;
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
        m_rigidbody.transform.position = from;
        StartCoroutine(CoroutineHelper.ExecuteAfterFrame(() => { m_rigidbody.AddForce(velocity, ForceMode2D.Impulse); }));
        StartCoroutine(CoroutineHelper.ExecuteAfterTime(lifeTime, () =>
        {
            if (OnTimeout != null)
                OnTimeout(this);
        }));
    }
}
