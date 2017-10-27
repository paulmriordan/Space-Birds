using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections;


public class ElevationUI : MonoBehaviour
{
    [SerializeField]
    Transform m_objectToTrack;

    private Text m_text;

    void Start()
    {
        m_text = GetComponent<Text>();
    }
    
    void Update()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Elevation: ");
        sb.Append(m_objectToTrack.transform.position.y.ToString("0"));
        sb.Append("m");
        m_text.text = sb.ToString();
    }
}
