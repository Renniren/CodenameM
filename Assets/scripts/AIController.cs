using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public PlayerCharacter ThisCharacter;
    public PlayerCharacter Target;

    public float TimeBetweenJumps = 0.5f;
    public float ReactionTime = 0.2f;

    float CurrentReactionTime, CurrentTimeToJump;

    // Start is called before the first frame update
    void Start()
    {
        CurrentReactionTime = ReactionTime;
    }

    void Move(Vector2 input)
    {
        ThisCharacter.cont.moveUnfiltered = new Vector3(input.x, 0, input.y);
        ThisCharacter.cont.move = new Vector3(input.x, 0, input.y);

    }

    void ResetStick()
    {
        ThisCharacter.cont.moveUnfiltered = new Vector3(0, 0, 0);
        ThisCharacter.cont.move = new Vector3(0, 0, 0);
    }

    Vector3 TargetPosition;

    // Update is called once per frame
    void Update()
    {
        CurrentReactionTime += Time.deltaTime;
        CurrentTimeToJump += Time.deltaTime;

        for(int i = 0; i < PlayerCharacter.Players.Count; i++)
        {
            if (ThisCharacter == PlayerCharacter.Players[i]) continue;

            if(Vector3.Distance(transform.position, PlayerCharacter.Players[i].transform.position) < 9)
            {
                Target = PlayerCharacter.Players[i];
            }
        }


        Vector3 dir = TargetPosition - transform.position;
        if (Vector3.Dot(transform.right, dir) < 0)
        {
            ThisCharacter.Direction = PlayerCharacter.facing.left;
        }
        if (Vector3.Dot(transform.right, dir) > 0)
        {
            ThisCharacter.Direction = PlayerCharacter.facing.right;
        }

        if(!Physics.Linecast(transform.position, -transform.up * 10, LayerMask.GetMask("Default")))
        {
            ThisCharacter.LStick = new Vector2(dir.x, dir.y);
            ThisCharacter.TryAirDodge();
        }

        if(ReactionTime <= CurrentReactionTime)
        {
            TargetPosition = Target.transform.position;

            if (Vector3.Distance(transform.position, TargetPosition) > 2.2f)
            {
                if (Vector3.Dot(transform.right, dir) < 0) // left
                {
                    Move(new Vector2(-1, 0));
                    ThisCharacter.LStick = new Vector2(1, 0);
                }
                if (Vector3.Dot(transform.right, dir) > 0) // right 
                {
                    Move(new Vector2(1, 0));
                    ThisCharacter.LStick = new Vector2(-1, 0);
                }
            }
            else
            {
                ThisCharacter.LStick = new Vector2(dir.x, dir.y);
                ThisCharacter.LightAttack();
                ResetStick();
            }

            if (!Target.cont.IsGrounded)
            {
                Move(new Vector2(Mathf.Clamp(dir.x,-1,1), Mathf.Clamp(dir.x, -1, 1)));
                ThisCharacter.LStick = new Vector2(Mathf.Clamp(dir.x, -1, 1), Mathf.Clamp(dir.y, -1, 1));
                if (CurrentTimeToJump >= TimeBetweenJumps && ThisCharacter.IFrames == 0)
                {
                    ThisCharacter.cont.TryJump();
                    CurrentTimeToJump = 0;
                }
            }

            CurrentReactionTime = 0;
        }
        
        

    }
}
