using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary> 
/// Place this on a tiling sprite.
/// Sprite height should extend so the sprite covers the 2 x screen height to ensure no gaps appear
/// </summary>
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
        if (ShouldRepositionBackground())
        {
            RepositionBackground();
        }
    }

    bool ShouldRepositionBackground()
    {
        // Reposition if camera is > one background tile from centre of background texture.
        float currOffset = m_camera.position.y - GetCentreYOfBackgroundTexture();
        return Mathf.Abs(currOffset) > m_backgroundHeight;
    }

    float GetCentreYOfBackgroundTexture()
    {
        return (this.transform.position.y + m_backgroundHeight * 0.5f);
    }

    void RepositionBackground()
    {
        // Move background along y in tile steps, so that wrapping is seamless.
        float numTilesY = Mathf.FloorToInt(m_camera.position.y / m_backgroundHeight);
        Vector3 pos = this.transform.position;
        pos.y = numTilesY * m_backgroundHeight;
        transform.position = pos;
    }
}