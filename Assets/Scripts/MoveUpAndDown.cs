using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveUpAndDown : MonoBehaviour {

    [SerializeField]
    float moveDelayMin = 0.2f;
    [SerializeField]
    float moveDelayMax = 1.0f;
    [SerializeField]
    float moveForceMin = 10.0f;
    [SerializeField]
    float moveForceMax = 30.0f;

    private float m_nextMoveTime;

    private Rigidbody2D m_rigidbody;

	// Use this for initialization
	void Start ()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        SetNextMoveTime();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time > m_nextMoveTime)
        {
            PerformMove();
            SetNextMoveTime();
        }
	}

    void PerformMove()
    {
        var force = Vector3.up * UnityEngine.Random.Range(moveForceMin, moveForceMax) * (UnityEngine.Random.value < 0.5f ? -0.1f : 0.1f);
        m_rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    void SetNextMoveTime()
    {
        m_nextMoveTime = Time.time + UnityEngine.Random.Range(moveDelayMin, moveDelayMax);
    }
}
