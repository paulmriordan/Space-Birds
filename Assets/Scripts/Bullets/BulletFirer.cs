using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFirer : MonoBehaviour {

    [SerializeField]
    float m_bulletTimeMin = 5.0f;
    [SerializeField]
    float m_bulletTimeMax = 15.0f;

    [SerializeField]
    Vector3 m_bulletVelocity = new Vector3(-1.0f, 0, 0);

    [SerializeField]
    BulletFactory.E_BulletType m_type;

    private float m_nextBulletTime;

    private void Start()
    {
        SetNextBulletTime();
    }

    void Update ()
    {
		if (Time.time > m_nextBulletTime)
        {
            FireBullet();
            SetNextBulletTime();
        }
	}

    void FireBullet()
    {
        Bullet bullet = BulletFactory.Instance.Create(m_type);
        
        bullet.Fire(this.transform.position, this.transform.rotation * m_bulletVelocity);
    }

    void SetNextBulletTime()
    {
        m_nextBulletTime += UnityEngine.Random.Range(m_bulletTimeMin, m_bulletTimeMax);
    }
}
