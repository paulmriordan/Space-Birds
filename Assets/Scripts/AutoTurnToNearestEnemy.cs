using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurnToNearestEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var nearestEnemy = GetNearestEnemy();
        if (nearestEnemy != null)
        {
            bool left = nearestEnemy.transform.position.x < this.transform.position.x;
            this.transform.rotation = Quaternion.Euler(0, 0, left ? 0 : 180.0f);
            this.transform.localScale = new Vector3(1.0f, left ? 1.0f : -1.0f, 1.0f);
        }
	}

    Enemy GetNearestEnemy()
    {
        float bestDist = float.MaxValue;
        Enemy bestEnemy = null;
        foreach (var enemy in EnemySpawner.Instance.ActiveEnemies())
        {
            var d2 = (this.transform.position - enemy.transform.position).sqrMagnitude;
            if (d2 < bestDist)
            {
                bestEnemy = enemy;
                bestDist = d2;
            }
        }
        return bestEnemy;
    }
}
