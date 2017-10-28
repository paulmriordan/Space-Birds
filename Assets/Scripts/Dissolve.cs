using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour {

    public event System.Action OnDissolved;

    private Material m_material;
    private float m_startTime = -1.0f;
    private float m_dissolveTime;

    void Start()
    {
        m_material = GetComponent<SpriteRenderer>().material;
    }

    public void StartDissolve(float time)
    {
        m_dissolveTime = time;
        m_startTime = Time.time;
    }

	void Update ()
    {
        if (m_startTime == -1.0f)
            return;

        float prog = Mathf.Clamp01((Time.time - m_startTime) / m_dissolveTime);
        m_material.SetFloat("_DissolveProgress", prog);
        if (prog >= 1.0)
        {
            if (OnDissolved != null)
                OnDissolved();
            Destroy(this);
        }
    }

    private void OnDisable()
    {
        m_material.SetFloat("_DissolveProgress", 0);
    }
}
