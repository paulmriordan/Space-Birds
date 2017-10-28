using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoSingleton<EnemySpawner> {

    [SerializeField]
    GameObject m_enemyPrefab = null;

    [SerializeField]
    float m_enemyHzOffset = 1.0f;

    [SerializeField]
    float m_enemySpacingMin = 5.0f;
    [SerializeField]
    float m_enemySpacingMax = 15.0f;

    [SerializeField]
    float m_spawnAheadDistance = 30.0f;

    private Transform m_camera;
    private Pool<Enemy> m_enemyPool;
    private HashSet<Enemy> m_activeEnemies = new HashSet<Enemy>();
    private float m_nextSpawnHeight = 0;

    void Start()
    {
        m_camera = Camera.main.transform;
        m_enemyPool = new Pool<Enemy>( () => {return Instantiate(m_enemyPrefab).GetComponent<Enemy>(); } ) { };

        SetNextSpawnPosition();
    }
    
    public IEnumerable<Enemy> ActiveEnemies()
    {
        foreach (var enemy in m_activeEnemies)
            yield return enemy;
    }

    public void ResetAllEnemies()
    {
        m_nextSpawnHeight = 0;

        // Hold enemies in new structure while deleting, otherwise, hashset will be invalidated
        List<Enemy> enemies = new List<Enemy>();
        foreach (var enemy in m_activeEnemies)
            enemies.Add(enemy);

        for (int i = 0; i < enemies.Count; i++)
            CleanupEnemy(enemies[i]);

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
        enemyInstance.OnEnemyDead -= CleanupEnemy;
        m_activeEnemies.Remove(enemyInstance);
        m_enemyPool.Deallocate(enemyInstance);
    }

    void SpawnEnemy()
    {
        var instance = m_enemyPool.Allocate();
        instance.gameObject.SetActive(true);
        instance.transform.parent = this.transform;

        bool left = (UnityEngine.Random.value < 0.5f);

        // set position
        Vector3 pos = Vector3.zero;
        pos.x = (left ? -1.0f : 1.0f) * m_enemyHzOffset;
        pos.y = m_nextSpawnHeight;
        instance.transform.position = pos;

        // set rotation
        instance.transform.localRotation = Quaternion.Euler(0, 0, left ? 180.0f : 0);

        instance.OnEnemyDead += CleanupEnemy;

        m_activeEnemies.Add(instance);
    }
}
