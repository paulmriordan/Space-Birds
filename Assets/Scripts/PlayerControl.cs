using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour {

    [SerializeField]
    float ForceUp = 1.0f;

    Rigidbody2D m_rigidBody;
    
	void Start () {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.Space))
            m_rigidBody.AddForce(Vector2.up * ForceUp);
	}
}
