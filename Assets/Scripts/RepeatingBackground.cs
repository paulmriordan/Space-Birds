using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private float m_backgroundHeight;
    private Transform m_camera;

    void Start()
    {
        m_camera = Camera.main.transform;
        m_backgroundHeight = GetComponent<SpriteRenderer>().sprite.bounds.size.y;
    }
    void Update()
    {
        float centre = (this.transform.position.y + m_backgroundHeight * 0.5f);
        float currOffset = m_camera.position.y - centre;
        if (Mathf.Abs(currOffset) > m_backgroundHeight)
        {
            Vector3 pos = this.transform.position;
            pos.y += 2.0f * m_backgroundHeight * (currOffset < 0 ? - 1.0f : 1.0f);
            transform.position = pos;
        }
    }
}