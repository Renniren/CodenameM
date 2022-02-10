using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public struct CharacterData
{
    public string CharacterName;
    public Image Icon;
    public GameObject Prefab;
}

public class Character : MonoBehaviour
{
    public Animator anims;
    public string CharacterName = "Generic Man";
    public string PlayerName = "Ren";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void CastHurtsphere (Vector3 where, int damage, float initKnockback, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(where, radius);
        PlayerCharacter character;

        foreach (Collider col in colliders)
        {
            col.TryGetComponent(out character);
            if (character)
            {
                if (character.Invincible) return;
                character.ApplyDamage(damage);
                character.ApplyKnockback(character.transform.position - where, initKnockback);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
