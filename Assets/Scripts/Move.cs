using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float edgeDistance;

    private float sideMovementSpeed = 5.0f;
    private int moveBuffer = 0;
    private float laneDistance = 4.5f;

    private Animator animator;

    public enum Lanes: int
    {
        Left = 1,
        Middle = 2,
        Right = 3
    }
    private Lanes currentLane = Lanes.Middle;
    private Lanes targetLane = Lanes.Middle;

    private Vector3 targetPosition;
    private CharacterController controller;

    private void Reset()
    {
        speed = 10.0f;
        sideMovementSpeed = 5.0f;
        currentLane = Lanes.Middle;
        targetLane = Lanes.Middle;
        edgeDistance = 200.0f;
        direction = Vector3.forward;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetPosition = TranslatedPosition(Vector3.zero, direction * speed * Time.deltaTime);
        if (InputLeft() && targetLane != Lanes.Left)
        {
            if (InTargetLane())
            {
                targetLane -= 1;
                moveBuffer = 0;
                animator.SetBool("Run Left", true);
                //Debug.Log(targetLane);

            }
            else if (moveBuffer == 0 && NearTargetLane())// HeadingRight())
            {
                moveBuffer = -1;
            }

        }
        else if (InputRight() && targetLane != Lanes.Right)
        {
            if (InTargetLane())
            {
                targetLane += 1;
                moveBuffer = 0;
                animator.SetBool("Run Right", true);
                //Debug.Log(targetLane);
            }
            else if (moveBuffer == 0 && NearTargetLane())//HeadingLeft())
            {
                moveBuffer = 1;
            }
        }

        if (HeadingLeft()) //ie heading left
        {
            targetPosition = TranslatedPosition(targetPosition, laneDistance * sideMovementSpeed * Vector3.left * Time.deltaTime);

        }
        else if (HeadingRight()) //ie heading right
        {
            targetPosition = TranslatedPosition(targetPosition, laneDistance * sideMovementSpeed * Vector3.right * Time.deltaTime);

        }
        else
        {
            currentLane = targetLane;
            animator.SetBool("Run Right", false);
            animator.SetBool("Run Left", false);
        }

        if (transform.position.z >= edgeDistance)
        {
            targetPosition = TranslatedPosition(targetPosition, 0, 0, -edgeDistance);
            //controller.Move((direction * -1 * edgeDistance) + (direction * speed * Time.deltaTime));
            //TODO add movement for obstacles decor and pickups
        }

        controller.Move(targetPosition);
        //controller.Move(direction * speed * Time.deltaTime);
        
    }

    Vector3 TranslatedPosition(Vector3 initialPosition, float x, float y, float z)
    {
        return new Vector3(initialPosition.x + x, initialPosition.y + y, initialPosition.z + z);
    }

    Vector3 TranslatedPosition(Vector3 initialPosition, Vector3 translation)
    {
        return new Vector3(initialPosition.x + translation.x, initialPosition.y + translation.y, initialPosition.z + translation.z);
    }

    float GetLanePosX(Lanes lane)
    {
        return ((int) lane - 2)* laneDistance;
    }

    bool InTargetLane()
    {
        return GetLanePosX(targetLane) == transform.position.x;
    }

    bool NearTargetLane()
    {
        float dist = Mathf.Abs(GetLanePosX(targetLane) - transform.position.x);
        Debug.Log(dist);
        return dist < laneDistance/2;
    }

    bool InputLeft()
    {
        return Input.GetKeyDown(KeyCode.A) || moveBuffer == -1;
    }

    bool InputRight()
    {
        return Input.GetKeyDown(KeyCode.D) || moveBuffer == 1;
    }

    bool HeadingLeft()
    {
        return GetLanePosX(targetLane) < transform.position.x;
    }

    bool HeadingRight()
    {
        return GetLanePosX(targetLane) > transform.position.x;
    }
}

