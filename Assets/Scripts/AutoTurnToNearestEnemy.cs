using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates this gameobject towards enemy. Flips on y access turn ensure object remains upright
/// </summary>
public class AutoTurnToNearestEnemy : MonoBehaviour {

	void Update ()
    {
        var nearestEnemy = GetNearestEnemy();
        if (nearestEnemy != null)
        {
            bool left = nearestEnemy.transform.position.x < this.transform.position.x;
            this.transform.rotation = Quaternion.Euler(0, 0, left ? 0 : 180.0f);
            this.transform.localScale = new Vector3(1.0f, left ? 1.0f : -1.0f, 1.0f); // flip to remain upright
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
