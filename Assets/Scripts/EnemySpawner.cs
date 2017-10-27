using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    GameObject m_enemyPrefab;

    [SerializeField]
    float m_enemyHzOffset = 1.0f;

    [SerializeField]
    float m_enemySpacingMin = 5.0f;
    float m_enemySpacingMax = 15.0f;

    [SerializeField]
    float m_spawnAheadDistance = 30.0f;

    private Transform m_camera;
    private Pool<Enemy> m_enemyPool;
    private List<Enemy> m_activeEnemies;
    private float m_maxHeightReached;
    private float m_nextSpawnHeight = 0;

    void Start()
    {
        m_camera = Camera.main.transform;
        m_enemyPool = new Pool<Enemy>( () => {return Instantiate(m_enemyPrefab).GetComponent<Enemy>(); } ) { };
        SetNextSpawnPosition();
    }
	
	void Update ()
    {
        if ((m_nextSpawnHeight - m_camera.transform.position.y) < m_spawnAheadDistance)
        {
            SpawnEnemy();
            SetNextSpawnPosition();
        }
	}

    void SetNextSpawnPosition()
    {
        m_nextSpawnHeight += UnityEngine.Random.Range(m_enemySpacingMin, m_enemySpacingMax);
    }
    
    void CleanupEnemy(Enemy enemyInstance)
    {
        enemyInstance.gameObject.SetActive(false);
        m_enemyPool.Deallocate(enemyInstance);
    }

    void SpawnEnemy()
    {
        var instance = m_enemyPool.Allocate();

        instance.gameObject.SetActive(true);

        instance.transform.parent = this.transform;

        bool left = (UnityEngine.Random.value < 0.5f);

        Vector3 pos = Vector3.zero;
        pos.x = (left ? -1.0f : 1.0f) * m_enemyHzOffset;
        pos.y = m_nextSpawnHeight;
        instance.transform.position = pos;

        instance.transform.localScale = new Vector3(left ? -1.0f : 1.0f, 1.0f, 1.0f);

        instance.OnEnemyDead += CleanupEnemy;
    }
}
