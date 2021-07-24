using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    // rotation of hooks
    [SerializeField] private float min_Z = -55f, max_Z = 55f;
    [SerializeField] private float rotate_Speed = 5f;
    private float rotate_Angle;
    private bool rotate_Right;
    private bool canRotate;
    public float move_Speed = 3f;
    private float initial_Move_Speed;
    [SerializeField] private float min_Y = -2.5f;
    private float initial_Y;
    private bool moveDown;

    // Line renderer
    private RopeRenderer ropeRenderer;

    private void Awake()
    {
        ropeRenderer = GetComponent<RopeRenderer>();
    }
    void Start()
    {
        initial_Y = transform.position.y;
        initial_Move_Speed = move_Speed;
        canRotate = true;
    }

    void Update()
    {
        Rotate();
        GetInput();
        MoveRope();
    }

    void Rotate()
    {
        if (!canRotate)
        {
            return;
        }
        if (rotate_Right)
        {
            rotate_Angle += rotate_Speed * Time.deltaTime;
        }
        else
        {
            rotate_Angle -= rotate_Speed * Time.deltaTime;
        }
        transform.rotation = Quaternion.AngleAxis(rotate_Angle, Vector3.forward);  // Vector3(0,0,1);

        if (rotate_Angle >= max_Z)
        {
            rotate_Right = false;
        }
        else if (rotate_Angle <= min_Z)
        {
            rotate_Right = true;
        }
    }

    void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (canRotate)
            {
                canRotate = false;
                moveDown = true;
                SoundManager.instance.RopeStretch(true);
            }
        }
    }

    void MoveRope()
    {
        if (canRotate)
        {
            return;
        }
        if (!canRotate)
        {
            Vector3 position = transform.position;
            if (moveDown)
            {
                position -= transform.up * Time.deltaTime * move_Speed;
            }
            else
            {
                position += transform.up * Time.deltaTime * move_Speed;
            }
            transform.position = position;

            if (position.y <= min_Y)
            {
                moveDown = false;
            }
            if (position.y >= initial_Y)
            {
                canRotate = true;
                // Deactivate line renderer
                ropeRenderer.RenderLine(position, false);
                // reset move speed
                move_Speed = initial_Move_Speed;
                SoundManager.instance.RopeStretch(false);
            }
            ropeRenderer.RenderLine(transform.position, true);
        }
    }

    public void HookAttachedItem()
    {
        moveDown = false;
    }
}
