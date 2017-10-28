using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour
{
    [SerializeField]
    PlayerCollisionDetection m_player;

    [SerializeField]
    TrackObject m_playerCameraTracker;

    [SerializeField]
    EnemySpawner m_enemySpawner;
    
    void Start()
    {
        m_player.OnPlayerCollision += ResetPlayerPosition;
        m_player.OnPlayerCollision += m_enemySpawner.ResetAllEnemies;
        m_player.OnPlayerCollision += m_playerCameraTracker.MoveToTargetInstantly;
    }
    
    void ResetPlayerPosition()
    {
        m_player.transform.position = new Vector2(transform.position.x, 0);
    }
}
