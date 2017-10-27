using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimationController : MonoBehaviour {

    public float FlyingUpTreshold = 1.0f;

    private Animator m_animator;
    private Rigidbody2D m_rigidbody2D;

    private int m_flyingUpKey;
    
	void Start () {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_flyingUpKey = Animator.StringToHash("FlyingUp");
    }
	
	void Update ()
    {
        m_animator.SetBool(m_flyingUpKey, m_rigidbody2D.velocity.y > FlyingUpTreshold); 
	}
}
