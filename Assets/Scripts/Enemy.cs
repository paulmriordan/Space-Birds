using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour {

    enum E_EnemyState { alive, dissolving, dead};

    public event Action<Enemy> OnEnemyDead;

    [SerializeField]
    public float KillAfterDistancePassed = 20.0f;
    
    [SerializeField]
    float DissolveTime = 0.2f;

    private Transform m_camera;
    private IWeapon[] m_weapons;
    private E_EnemyState m_state = E_EnemyState.alive;

    void Awake ()
    {
        m_camera = Camera.main.transform;
        m_weapons = GetComponentsInChildren<IWeapon>();
    }

    private void OnEnable()
    {
        m_state = E_EnemyState.alive;
        EnableWeapons(true);
    }

    void Update ()
    {
        switch (m_state)
        {
            default:
            case E_EnemyState.alive:
                KillIfPlayerHasPassedMe();
                break;
        }
    }

    void KillIfPlayerHasPassedMe()
    {
        if ((m_camera.transform.position.y - this.transform.position.y) > KillAfterDistancePassed)
        {
            if (OnEnemyDead != null)
                OnEnemyDead(this);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (m_state == E_EnemyState.alive)
        {
            m_state = E_EnemyState.dissolving;
            AddDissolveScript();
            EnableWeapons(false);
        }
    }

    void AddDissolveScript()
    {
        var dissolveScript = gameObject.AddComponent<Dissolve>();
        dissolveScript.StartDissolve(DissolveTime);
        dissolveScript.OnDissolved += Dissolved;
    }

    void EnableWeapons(bool enable)
    {
        for (int i = 0; i < m_weapons.Length; i++)
            m_weapons[i].EnableFiring(enable);
    }

    void Dissolved()
    {
        m_state = E_EnemyState.dead;
        if (OnEnemyDead != null)
            OnEnemyDead(this);
    }
}
