using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    private Color GizmosColor;

    // Returns a random Vector3 location inside the spawn area 
    public Vector3 GetRandomSpawnPoint() {
        Vector3 origin = transform.position;
        Vector3 range = transform.localScale / 2.0f;        //we defined the middle of the box to be the origin, so half the width is the range.
        Vector3 randomRange = new Vector3(Random.Range(-range.x, range.x),
                                          Random.Range(-range.y, range.y-1),
                                          Random.Range(-range.z, range.z));
        Vector3 randomCoordinate = origin + randomRange;
        return randomCoordinate;
    }


    // draw a transparent red cube (in scene view only) to represent this area
    void OnDrawGizmos() {
        Color GizmosColor = Color.red;
        GizmosColor.a = 0.2f;
        Gizmos.color = GizmosColor;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
