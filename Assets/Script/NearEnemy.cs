using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearEnemy : MonoBehaviour
{
    public CameraFollow cameraControll;

    public Transform player;
    public List<Enemy> enemies;

    private void LateUpdate()
    {
        Transform target=null;
        float distance=2000;
        int nowPriority = 100;
        foreach(Enemy enemy in enemies)
        {
            float distanceF = Vector3.Distance(player.transform.position, enemy.transform.position);
            Debug.Log(enemy.transform.name + distanceF.ToString());
            if (distanceF <= distance&&distanceF<10&&nowPriority>=enemy.Priority)
            {
                target = enemy.transform;
                distance = distanceF;
                nowPriority = enemy.Priority;
            }
        }
        cameraControll.enemyTarget = target;
    }
}
