using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoSingleton<BulletPool> {

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
	
    public Bullet Create()
    {
        var inst = m_bulletPool.Allocate();
        inst.OnTimeout += CleanupBullet;
        inst.gameObject.SetActive(true);
        return inst;
    }

    void CleanupBullet(Bullet bulletInstance)
    {
        bulletInstance.OnTimeout -= CleanupBullet;
        bulletInstance.gameObject.SetActive(false);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        m_bulletPool.Deallocate(bulletInstance);
    }

}
