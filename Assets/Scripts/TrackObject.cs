using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour {

    [SerializeField]
    Transform m_objectToTrack;
    [SerializeField]
    float SmoothTime = 1.0f;
    
    private Vector2 m_currVel;

    void FixedUpdate ()
    {
        if (m_objectToTrack != null)
        {
            var currPos = this.transform.position;
            currPos.x = Mathf.SmoothDamp(currPos.x, m_objectToTrack.position.x, ref m_currVel.x, SmoothTime);
            currPos.y = Mathf.SmoothDamp(currPos.y, m_objectToTrack.position.y, ref m_currVel.y, SmoothTime);
            this.transform.position = currPos;
        }
    }
}
