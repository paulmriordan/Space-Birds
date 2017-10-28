using UnityEngine;
using UnityEditor;

public interface IFireable
{
    void Fire(Vector3 from, Vector2 velocity);
}