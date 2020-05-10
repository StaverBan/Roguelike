using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string rotateTo="R";
    public Animator playerAnim;
    public float speed;
    public Vector2 direction;

    public void Update()
    {
      //  direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (direction.x > 0) rotateTo = "R";
        if (direction.x < 0) rotateTo = "L";
        if (direction.sqrMagnitude > 0) playerAnim.Play("Run" + rotateTo);
        else playerAnim.Play("Stand" + rotateTo);

        transform.Translate(direction*Time.deltaTime* speed);
    }
}
