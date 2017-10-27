using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour {

    public event Action<Enemy> OnEnemyDead;

    [SerializeField]
    public float KillAfterDistancePassed = 20.0f;

    private Transform m_camera;

    void Start ()
    {
        m_camera = Camera.main.transform;
    }
	
	void Update ()
    {
	    if ((m_camera.transform.position.y - this.transform.position.y) > KillAfterDistancePassed)
        {
            if (OnEnemyDead != null)
                OnEnemyDead(this);
        }

    }
}
