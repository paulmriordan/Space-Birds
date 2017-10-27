using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour
{
    [SerializeField]
    PlayerControl m_player;

    [SerializeField]
    TrackObject m_playerCameraTracker;

    [SerializeField]
    EnemySpawner m_enemySpawner;

    // Use this for initialization
    void Start()
    {
        m_player.OnPlayerKilled += m_enemySpawner.ResetAllEnemies;
        m_player.OnPlayerKilled += m_playerCameraTracker.MoveToTargetInstantly;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
