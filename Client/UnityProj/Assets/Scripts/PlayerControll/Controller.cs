﻿using UnityEngine;

public class Controller : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float JumpPower = 1.5f;
    public float Gravity = 0.8f;

    internal float default_MoveSpeed = 10f;
    private float default_JumpPower = 1.5f;
    private float default_Gravity = 0.8f;

    public CharacterController MyController;
    public MouseLooker MyMouseLooker;
    public CapsuleCollider CapsuleCollider;

    private Vector3 velocity = Vector3.zero;

    void Awake()
    {
#if UNITY_STANDALONE_OSX
        Gravity = 1.6f;
#elif UNITY_STANDALONE
        Gravity = 0.8f;
#endif

        default_MoveSpeed = MoveSpeed;
        default_JumpPower = JumpPower;
        default_Gravity = Gravity;
    }

    void Update()
    {
        if (SuperManMode)
        {
            MoveSpeed = GameManager.Instance.SupermanSpeed;
            Gravity = 0;
        }
        else
        {
            MoveSpeed = default_MoveSpeed;
            JumpPower = default_JumpPower;
            Gravity = default_Gravity;
        }

        velocity.x = Input.GetAxis("Horizontal") * MoveSpeed;
        velocity.z = Input.GetAxis("Vertical") * MoveSpeed;

        if (SuperManMode)
        {
            velocity.y = Input.GetAxis("Jump") * MoveSpeed;
        }
        else
        {
            if (MyController.isGrounded)
            {
                if (AllowJump)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        velocity.y = 0;
                        velocity.y += JumpPower * MoveSpeed;
                    }
                }
            }
            else
            {
                velocity.y -= Gravity;
            }
        }

        velocity = transform.TransformDirection(velocity);
        MyController.Move(velocity * Time.deltaTime);
    }

    public bool SuperManMode = false;

    public bool AllowJump = false;

    public void SetAllowJump()
    {
        AllowJump = true;
    }

    public float ControllerRadiusOnLand = 0.4f;
    public float ControllerRadiusOnRaft = 0.1f;

    public void SetColliderRadiusOnRaft()
    {
        MyController.radius = ControllerRadiusOnRaft;
    }

    public void SetColliderRadiusOnLand()
    {
        MyController.radius = ControllerRadiusOnLand;
    }
}