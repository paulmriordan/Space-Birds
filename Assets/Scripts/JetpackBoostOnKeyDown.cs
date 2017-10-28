using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JetpackBoostOnKeyDown : MonoBehaviour {

    [SerializeField]
    float ForceUp = 2.0f;

    [SerializeField]
    KeyCode m_keycode = KeyCode.Space;

    Rigidbody2D m_rigidBody;
    
	void Start () {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {
        if (Input.GetKey(m_keycode))
            m_rigidBody.AddForce(Vector2.up * ForceUp);
	}
}
