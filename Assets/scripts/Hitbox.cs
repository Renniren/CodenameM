using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Hitbox : MonoBehaviour
{
    public Vector3 KnocbackOffset = new Vector3(1,1,1);
    public string AssignedAttack;
    public int HitstunAmount = 4; 
    public int Damage = 1;
    public float KnockbackScale = 4;
    public Collider HitboxCollider;
    public TrailRenderer trail;
    public UnityEvent WhileActive;
    private PlayerCharacter character, ParentCharacter;

    void Awake()
    {
        if(GetComponent<MeshRenderer>()) GetComponent<MeshRenderer>().enabled = false;
        ParentCharacter = transform.GetComponentInParent<PlayerCharacter>();
        HitboxCollider.isTrigger = true;
    }

    private void Update()
    {
        bool t = false;
        
        foreach (var clip in ParentCharacter.character.anims.GetCurrentAnimatorClipInfo(0))
        {
            if (clip.clip.name == AssignedAttack || clip.clip.name.Contains(AssignedAttack)) t = true;
        }

        if(trail) trail.emitting = t;
        if (t) WhileActive?.Invoke();
        HitboxCollider.enabled = t;    
    }

    void OnTriggerEnter(Collider col)
    {
        col.TryGetComponent(out character);
        if (character)
        {
            if (character.Invincible || character.gameObject == transform.root.gameObject) return;

            HitSoundManager.Instance.PlayRandomLightSound();
            character.ApplyKnockback((character.transform.position - transform.position) + character.transform.InverseTransformDirection(KnocbackOffset), KnockbackScale);
            character.ApplyDamage(Damage);
            character.Hitstun += HitstunAmount;
            character.Helpless = false;
            GameManager.Instance.SpawnHitParticles(col.ClosestPoint(transform.position));
            CameraShake.ShakeAmount += 0.12f + (KnockbackScale / 100) + (character.Damage / 100);
            CameraShake.RollShakeAmount += 0.08f + (KnockbackScale / 100) + (character.Damage / 100);
            if (character.Damage > 60) HitSoundManager.Instance.PlayRandomHeavySound();
            if (character.Damage > 90) HitSoundManager.Instance.PlayRandomSuperHeavySound();
            if (character.Damage > 120 && KnockbackScale + Damage > 5) HitSoundManager.Instance.PlayRandomExtremelyHeavy();
        }
    }
}
