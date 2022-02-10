using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject HitParticles;
    public delegate void onHit(Vector3 where);
    public event onHit OnHit;
    public static GameManager Instance { get; private set; }
    // Start is called before the first frame update

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
        OnHit += SpawnHitParticles;
    }

    public void SpawnHitParticles(Vector3 where)
    {
        Instantiate(HitParticles, where, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
