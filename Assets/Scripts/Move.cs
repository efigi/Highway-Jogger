using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float edgeDistance;

    private float sideMovementSpeed = 5.0f;
    private float jumpHeight = 5.0f;
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

    public enum States: int
    {
        Idle = 0,
        Running = 1,
        RunningLeft = 2,
        RunningRight = 3,
        Jumping = 4
    }
    private States state = States.Idle;

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
        state = States.Running;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetPosition = TranslatedPosition(Vector3.zero, direction * speed * Time.deltaTime);

        //add gravity
        

        switch (state)
        {
            case States.Jumping:

                if (jumpHeight > transform.position.y)
                {
                    targetPosition = TranslatedPosition(targetPosition, 0, Mathf.Lerp(0, jumpHeight, jumpHeight * Time.deltaTime), 0);
                    //targetPosition = TranslatedPosition(targetPosition, 10 * Vector3.down * Time.deltaTime);
                }
                break;
            case States.Running:
                if (InputJump())// && controller.isGrounded)
                {
                    animator.SetBool("Jump", true);
                    //targetPosition = TranslatedPosition(targetPosition, 0, Mathf.Lerp(0,jumpHeight, jumpHeight * Time.deltaTime), 0);
                    state = States.Jumping;
                }
                else if (InputLeft() && targetLane != Lanes.Left)
                {

                    targetLane -= 1;
                    moveBuffer = 0;
                    animator.SetBool("Run Left", true);
                    state = States.RunningLeft;
                }
                else if (InputRight() && targetLane != Lanes.Right)
                {
                    targetLane += 1;
                    moveBuffer = 0;
                    animator.SetBool("Run Right", true);
                    state = States.RunningRight;
                }
                break;
            case States.RunningLeft:
            case States.RunningRight:
                if (InTargetLane())
                {
                    currentLane = targetLane;
                    animator.SetBool("Run Right", false);
                    animator.SetBool("Run Left", false);
                    state = States.Running;
                    break;
                }
                else if (HeadingLeft()) //ie heading left
                {
                    targetPosition = TranslatedPosition(targetPosition, laneDistance * sideMovementSpeed * Vector3.left * Time.deltaTime);

                }
                else if (HeadingRight()) //ie heading right
                {
                    targetPosition = TranslatedPosition(targetPosition, laneDistance * sideMovementSpeed * Vector3.right * Time.deltaTime);
                }

                if (InputLeft() && targetLane != Lanes.Left)
                {
                    if (moveBuffer == 0 && NearTargetLane())// HeadingRight())
                    {
                        moveBuffer = -1;
                    }

                }
                else if (InputRight() && targetLane != Lanes.Right)
                {
                    if (moveBuffer == 0 && NearTargetLane())//HeadingLeft())
                    {
                        moveBuffer = 1;
                    }
                }
                break;
            case States.Idle:
                break;
        }
        

        if (transform.position.z >= edgeDistance)
        {
            targetPosition = TranslatedPosition(targetPosition, 0, 0, -edgeDistance);
            //controller.Move((direction * -1 * edgeDistance) + (direction * speed * Time.deltaTime));
            //TODO add movement for obstacles decor and pickups
        }

        controller.Move(targetPosition);
        if (state == States.Jumping && controller.isGrounded)
        {
            state = States.Running;
            animator.SetBool("Jump", false);

        }
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
        return dist < laneDistance/1.5;
    }

    bool InputLeft()
    {
        return Input.GetKeyDown(KeyCode.A) || moveBuffer == -1;
    }

    bool InputRight()
    {
        return Input.GetKeyDown(KeyCode.D) || moveBuffer == 1;
    }

    bool InputJump()
    {
        return Input.GetKeyDown(KeyCode.W);
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