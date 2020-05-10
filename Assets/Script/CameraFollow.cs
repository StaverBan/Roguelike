using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform enemyTarget;

    public float speed;

    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 direction = target.position - enemyTarget.position;

        float lenght = direction.magnitude;
        direction.Normalize();

        Vector3 middlePosition=target.position-direction*lenght/2;
        Vector3 targetPosition;
        float MoveSpeed;

        if (lenght < 10)
        {
            targetPosition = middlePosition + offset;
            MoveSpeed = speed* 2;
        }
        else
        {
            targetPosition = target.position + offset;
            MoveSpeed = speed ;
        }


        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * MoveSpeed);
    }
}
