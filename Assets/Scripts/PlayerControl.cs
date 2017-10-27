using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour {

    [SerializeField]
    float ForceUp = 1.0f;

    public event System.Action OnPlayerKilled;

    Rigidbody2D m_rigidBody;
    
	void Start () {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.Space))
            m_rigidBody.AddForce(Vector2.up * ForceUp);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        ResetToStart();
    }

    void ResetToStart()
    {
        transform.position = new Vector2(transform.position.x, 0);
        if (OnPlayerKilled != null)
        {
            OnPlayerKilled();
        }
    }
}
