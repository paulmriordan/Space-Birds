using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoSingleton<BulletFactory>
{
    public BulletPool m_enemyBullets;
    public BulletPool m_playerBullets;

    public enum E_BulletType
    {
        player,
        enemy
    };

    public IFireable Create(E_BulletType type)
    {
        switch(type)
        {
            default:
            case E_BulletType.enemy: return m_enemyBullets.Create();
            case E_BulletType.player: return m_playerBullets.Create();;
        }
    }
}
