using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMCore;

public class BlastZone : MonoBehaviour
{
    public GameObject BlastEffect;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Entities.GetPlayerCharacter(other.gameObject))
        {
            Instantiate(BlastEffect, other.ClosestPoint(other.transform.position), Quaternion.identity).transform.LookAt(Vector3.zero);
            Entities.GetPlayerCharacter(other.gameObject).transform.position = Vector3.zero;
            Entities.GetPlayerCharacter(other.gameObject).ResetState();
            HitSoundManager.Instance.PlayRandomKO();
            CameraShake.ShakeAmount += 3f;
        }
    }
}
