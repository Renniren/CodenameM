using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour
{
    public enum PlayerState
    {
        InShield,
        InAttack,
        Idle,
        Dead,
        ShieldbreakStunned,
        Stunned,
        Asleep,
        Walking,
        Running

    }

    public enum facing
    {right, left }


    public int BelongingPlayer;
    public int Damage;
    public int Stamina;
    public facing Direction;


    
    public static List<PlayerCharacter> Players = new List<PlayerCharacter>();

    public bool Invincible
    {
        get
        {
            return IFrames != 0;
        }
    }

    [Header("Player State Information")]
    [Tooltip("Is the player currently in a helpless/freefall state?")] public bool Helpless;
    [Tooltip("How many frames of hitstun this character currently has. Hitstun prevents the character from doing anything aside from moving.")] public int Hitstun = 0;
    [Tooltip("How many frames of invincibility this character currently has.")] public int IFrames = 0;
    [Tooltip("How many frames of invincibility are given from airdodging.")] public int AirdodgeIframes = 20;

    [Header("Character Settings")]
    [Tooltip("This is how many jabs the character has. This value must be one greater than how many the character actually has.")]public int MaxJabCombo = 1;
    [Tooltip("This is the amount of force applied to the character when the player performs an airdodge.")]public float AirdodgeVelocity = 11;
    public float SprintSpeed = 125;
    public float JabResetTime = 0.1f;
    public bool EnableMultiJabs = true;

    public Character character;
    public Rigidbody body;
    public PlayerController cont;

    [Header("Debug Settings")]
    public Color HitstunnedColor;
    public Color InvincibleColor;
    public Color NormalColor;
    public MeshRenderer HitboxDebugRenderer;

    int jabcombo;
    float res;
    bool landed;
    public Vector2 LStick, RStick;


    // Start is called before the first frame update
    void Start()
    {

        character = GetComponentInChildren<Character>();
    }

    public void SetLeftJoystick(InputAction.CallbackContext context)
    {
        if (BelongingPlayer == -1) return;
        LStick = context.ReadValue<Vector2>();
    }
    public void SetRightJoystick(InputAction.CallbackContext context)
    {
        if (BelongingPlayer == -1) return;
        RStick = context.ReadValue<Vector2>();
    }
    float deadzone = 0.7f;

    void TryCStickAttacks()
    {
        if (RStick == Vector2.zero || Helpless) return;
        if (RStick.y < -deadzone)
        {
            character.anims.Play("DAir");
            return;
        }

        if (RStick.y > deadzone)
        {
            character.anims.Play("UAir");
            return;
        }

        if (Direction == facing.left)
        {
            if (RStick.x > deadzone)
            {
                character.anims.Play("BAir");
                return;
            }

            if (RStick.x < deadzone)
            {
                character.anims.Play("FAir");
                return;
            }
        }
        if (Direction == facing.right)
        {
            if (RStick.x < deadzone)
            {
                character.anims.Play("BAir");
                return;
            }

            if (RStick.x > deadzone)
            {
                character.anims.Play("FAir");
                return;
            }
        }
    }

    public void LightAttack()
    {
        if (Hitstun != 0 || Helpless) return;
        IFrames = 0;
        res = 0;
        if (cont.IsGrounded)
        {
            if (EnableMultiJabs) character.anims.Play("jab" + jabcombo.ToString());
            if (!EnableMultiJabs) character.anims.Play("jab1");

            if (jabcombo != MaxJabCombo) jabcombo++;
            if (jabcombo >= MaxJabCombo) jabcombo = 0;

        }
        else
        {

            if (LStick.y < -deadzone)
            {
                character.anims.Play("DAir");
                return;
            }

            if (LStick.y > deadzone)
            {
                character.anims.Play("UAir");
                return;
            }

            if (Direction == facing.left)
            {
                if (LStick.x > deadzone)
                {
                    character.anims.Play("BAir");
                    return;
                }

                if (LStick.x < deadzone)
                {
                    character.anims.Play("FAir");
                    return;
                }
            }
            if (Direction == facing.right)
            {
                if (LStick.x < deadzone)
                {
                    character.anims.Play("BAir");
                    return;
                }

                if (LStick.x > deadzone)
                {
                    character.anims.Play("FAir");
                    return;
                }
            }
        }
    }

    public void LightAttack(InputAction.CallbackContext context)
    {
        if (BelongingPlayer == -1) return;
        if (!context.performed) return;
        if (Hitstun != 0 || Helpless) return;
        IFrames = 0;
        res = 0;
        if(cont.IsGrounded)
        {
            if (EnableMultiJabs) character.anims.Play("jab" + jabcombo.ToString());
            if (!EnableMultiJabs) character.anims.Play("jab1");

            if (jabcombo < MaxJabCombo) jabcombo++;
            if (jabcombo >= MaxJabCombo) jabcombo = 1;

        }
        else
        {
            if (LStick.magnitude < deadzone)
            {
                character.anims.Play("NAir");
                return;
            }

            if (LStick.y < -deadzone)
            {
                character.anims.Play("DAir");
                return;
            }

            if (LStick.y > deadzone)
            {
                character.anims.Play("UAir");
                return;
            }

            if (Direction == facing.left)
            {
                if (LStick.x > deadzone)
                {
                    character.anims.Play("BAir");
                    return;
                }

                if (LStick.x < deadzone)
                {
                    character.anims.Play("FAir");
                    return;
                }
            }
            if (Direction == facing.right)
            {
                if (LStick.x < deadzone)
                {
                    character.anims.Play("BAir");
                    return;
                }

                if (LStick.x > deadzone)
                {
                    character.anims.Play("FAir");
                    return;
                }
            }
        }
    }

    public void PlayJumpAnimation(InputAction.CallbackContext context)
    {
        if (BelongingPlayer == -1) return;
        if (cont.Jumps == 0 || !context.performed) return;
        character.anims.Play("Jump");
    }

    public void PlayJumpAnimation()
    {
        if (cont.Jumps == 0) return;
        character.anims.Play("Jump");
    }

    public void TryAirDodge(InputAction.CallbackContext context)
    {
        if (BelongingPlayer == -1) return;
        if (Helpless || !context.performed) return;
        character.anims.Play("Airdodge");
        if (cont.moveUnfiltered.x > 0) Direction = facing.right;
        if (cont.moveUnfiltered.x < 0) Direction = facing.left;
        body.velocity = (cont.moveUnfiltered + transform.up * 0.3f) * AirdodgeVelocity;
        IFrames = AirdodgeIframes;
        Helpless = true;    
        landed = false;
    }

    public void TryAirDodge()
    {
        if (Helpless) return;
        character.anims.Play("Airdodge");
        if (cont.moveUnfiltered.x > 0) Direction = facing.right;
        if (cont.moveUnfiltered.x < 0) Direction = facing.left;
        body.velocity = (cont.moveUnfiltered + transform.up * 0.3f) * AirdodgeVelocity;
        IFrames = AirdodgeIframes;
        Helpless = true;
        landed = false;
    }

    public void ApplyKnockback(Vector3 dir, float initialKnockback)
    {
        body.AddForce((dir + transform.up * 0.3f) * initialKnockback * Damage, ForceMode.VelocityChange);
    }

    public void SetCharacterModelRotation(InputAction.CallbackContext context)
    {
        if (BelongingPlayer == -1) return;
        if (!cont.IsGrounded) return;
        if (context.ReadValue<Vector2>().x > 0) Direction = facing.right;
        if (context.ReadValue<Vector2>().x < 0) Direction = facing.left;

    }

    public void SetCharacterModelRotation()
    {
        if (!cont.IsGrounded) return;
        if (LStick.x < 0) Direction = facing.right;
        if (LStick.x > 0) Direction = facing.left;

    }

    public void SetCharacterModelRotationInAir()
    {
        if (LStick.x < 0) Direction = facing.right;
        if (LStick.x > 0) Direction = facing.left;
    }

    public void ApplyDamage(int damage)
    {
        Damage += damage;
    }

    public void ResetState()
    {
        Damage = 0;
        IFrames = 90;
        body.velocity = Vector3.zero;
        Helpless = false;
    }

    private void FixedUpdate()
    {
        Hitstun--;
        IFrames--;
        Hitstun = Mathf.Clamp(Hitstun, 0, 9999);
        IFrames = Mathf.Clamp(IFrames, 0, 9999);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Players.Contains(this)) Players.Add(this);
        if(IFrames != 0)
        {
            if (HitboxDebugRenderer) HitboxDebugRenderer.material.color = InvincibleColor;
        }
        else
        {
            if(HitboxDebugRenderer) HitboxDebugRenderer.material.color = NormalColor;
        }

        switch(Direction)
        {
            case facing.right:
                character.transform.eulerAngles = new Vector3(0, 0, 0);
                break;

            case facing.left:
                character.transform.eulerAngles = new Vector3(0, 180, 0);
                break;


        }

        res += Time.deltaTime;

        IFrames = Mathf.Clamp(IFrames, 0, 9999);
        Hitstun = Mathf.Clamp(Hitstun, 0, 9999);

        if (res >= JabResetTime) jabcombo = 1;

        jabcombo = Mathf.Clamp(jabcombo, 1, MaxJabCombo + 1);
        character.anims.SetFloat("MoveScale", Mathf.Abs(cont.moveUnfiltered.x));
        if (cont.IsGrounded)
        {
            
            if(!landed)
            {
                character.anims.Play("Land");   
                landed = true;
            }
            Helpless = false;
        }
        else
        {
            TryCStickAttacks();
            if(landed)
            {

                landed = false;
            }
        }
        //if (Mathf.Abs(cont.moveUnfiltered.y) > 0.001f && !cont.IsGrounded) body.AddForce(-Vector3.up * 15 * body.mass);
    }
}
