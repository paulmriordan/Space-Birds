using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour {

    [SerializeField]
    Transform m_objectToTrack;
    [SerializeField]
    Vector3 m_offset;
    [SerializeField]
    float SmoothTime = 1.0f;
    [SerializeField]
    float m_minY;

    private Vector2 m_currVel;

    public void MoveToTargetInstantly()
    {
        m_currVel = Vector2.zero;
        var targetPos = m_objectToTrack.position;
        targetPos.z = this.transform.position.z;
        targetPos += m_offset;
        targetPos.y = Mathf.Max(m_minY, targetPos.y);
        this.transform.position = targetPos;
    }

    void FixedUpdate ()
    {
        if (m_objectToTrack != null)
        {
            var currPos = this.transform.position;
            currPos.x = Mathf.SmoothDamp(currPos.x, m_objectToTrack.position.x + m_offset.x, ref m_currVel.x, SmoothTime);
            currPos.y = Mathf.Max(m_minY, Mathf.SmoothDamp(currPos.y, m_objectToTrack.position.y + m_offset.y, ref m_currVel.y, SmoothTime));
            this.transform.position = currPos;
        }
    }
}
