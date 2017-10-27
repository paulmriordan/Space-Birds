using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections;


public class BestElevationUI : MonoBehaviour
{
    [SerializeField]
    Transform m_objectToTrack = null;

    private float m_best;
    private Text m_text;

    void Start()
    {
        m_best = PlayerPrefs.GetFloat("bestElevation", 0);
        m_text = GetComponent<Text>();
    }
    
    void Update()
    {
        if (m_objectToTrack.transform.position.y > m_best)
        {
            m_best = m_objectToTrack.transform.position.y;
            PlayerPrefs.SetFloat("bestElevation", m_best);
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("Best: ");
        sb.Append(m_best.ToString("0"));
        sb.Append("m");
        m_text.text = sb.ToString();
    }
}
