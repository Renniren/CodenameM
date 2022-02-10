using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
public class PlayerController : MonoBehaviour
{
    protected enum lockMovementMode { LockX, LockZ, LockAll, LockNone }

    [SerializeField] protected lockMovementMode LockMovementMode = lockMovementMode.LockNone;

    public int Controller;

    public Vector3 moveUnfiltered;
    public Vector3 move;
    public Rigidbody body;
    public CapsuleCollider BodyCollider;
    public Transform Groundcheck;

    public float GravityMultiplier = 1;
    public float GroundcheckRadius = 0.55f;
    public float GroundedDrag = 20, AerialDrag = 50, MaximumDragVelocity = 50, HighVelocityDrag = 0.2f;
    public float GroundedAcceleration = 6000, AerialAcceleration = 500;
    public float JumpForce = 7;

    public int MaximumJumps = 1;
    public int Jumps;

    public bool IsGrounded;
    public List<int> ExcludedLayers = new List<int>();
    Collider[] colliders;

    void ApplyDrag(float scalar)
    {
        Vector3 vel = body.velocity;
        vel.y = 0;
        body.AddForce(-vel * scalar, ForceMode.Acceleration);
    }

    void DoMovement(float accel)
    {
        move.y = 0;
        if (IsGrounded)
        {
            move.y = 0;
            body.AddForce(transform.InverseTransformDirection(move) * accel, ForceMode.Acceleration);
        }
        else
        {
            move.y = 0;
            body.AddForce(transform.InverseTransformDirection(move) * accel, ForceMode.Acceleration);
        }
    }

    //have camera use an empty object as the forward reference instead of itself to avoid rotational fuckery

    public void SetMovement(InputAction.CallbackContext context)
    {
        //if(context.action.)
        if (Controller == -1) return;
        move.x = context.ReadValue<Vector2>().x;
        move.z = context.ReadValue<Vector2>().y;

        moveUnfiltered.x = context.ReadValue<Vector2>().x;
        moveUnfiltered.y = context.ReadValue<Vector2>().y;
    }


    public void TryJump()
    {
        if (!IsGrounded && Jumps <= 0) return;
        if (!IsGrounded) Jumps--;
        body.velocity = new Vector3(body.velocity.x, JumpForce, body.velocity.z);
    }

    public void TryJump2(InputAction.CallbackContext context)
    {
        if (Controller == -1) return;
        if (!IsGrounded && Jumps <= 0 || !context.performed) return;
        if (!IsGrounded) Jumps--;
        body.velocity = new Vector3(body.velocity.x, JumpForce, body.velocity.z);
    }

    public void TryShortHop(InputAction.CallbackContext context)
    {
        if (Controller == -1) return;
        if (!IsGrounded && Jumps <= 0 || !context.performed) return;
        if (!IsGrounded) Jumps--;
        body.velocity = new Vector3(body.velocity.x, 8, body.velocity.z);
    }

    void FixedUpdate()
    {
        if(IsGrounded)
        {
            Jumps = MaximumJumps;
            if(move != Vector3.zero) DoMovement(GroundedAcceleration);
            ApplyDrag(GroundedDrag);
        }
        else
        {
            body.AddForce(-Vector3.up * GravityMultiplier * body.mass);
            if (move != Vector3.zero) DoMovement(AerialAcceleration);
            if(new Vector3(body.velocity.x, 0 ,body.velocity.z).magnitude >= MaximumDragVelocity)
            {
                ApplyDrag(HighVelocityDrag);
            }
            else
            {
                ApplyDrag(AerialDrag);
            }
        }
    }

    List<Collider> collidersConverted;
    int c;
    void Update()
    {
        move.y = 0;
        colliders = Physics.OverlapSphere(Groundcheck.position, GroundcheckRadius, LayerMask.GetMask("Default"));


        if (colliders.Length >= 1)
        {
            IsGrounded = true;
        }

        if (colliders.Length < 1)
        {
            IsGrounded = false;
        }


        //move.x = Input.GetAxis("Horizontal");
        //move.z = Input.GetAxis("Vertical");

        switch (LockMovementMode)
        {
            case lockMovementMode.LockX:
                move.x = 0;
                break;

            case lockMovementMode.LockZ:
                move.z = 0;
                break;
        }

        //if (Input.GetKeyDown(KeyCode.Space)) TryJump();
    }
}
