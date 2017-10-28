using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates an IFireable. IFireable is deallocated when IFireable::OnFinished event is fired
/// </summary>
public class BulletPool : MonoBehaviour {

    public GameObject m_bulletPrefab;

    private Pool<Bullet> m_bulletPool;
    
    void Start ()
    {
        m_bulletPool = new Pool<Bullet>(() => 
        {
            var inst = Instantiate(m_bulletPrefab, this.transform).GetComponent<Bullet>();
            return inst;
        });
    }
	
    public IFireable Create()
    {
        var inst = m_bulletPool.Allocate();
        inst.OnFinished += CleanupBullet;
        inst.gameObject.SetActive(true);
        return inst;
    }

    void CleanupBullet(Bullet bulletInstance)
    {
        bulletInstance.OnFinished -= CleanupBullet;
        bulletInstance.gameObject.SetActive(false);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        m_bulletPool.Deallocate(bulletInstance);
    }

}
