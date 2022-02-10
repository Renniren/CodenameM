using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSoundManager : MonoBehaviour
{
    public GameObject outputObject;

    public AudioClip[] EnhancerSounds;
    public AudioClip[] KOSounds;
    public AudioClip[] LightHitSounds;
    public AudioClip[] HeavyHitSounds;
    public AudioClip[] SuperHeavyHitSounds;
    public AudioClip[] ExtremelyHeavySounds;

    public static HitSoundManager Instance { get; private set; }

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayRandomExtremelyHeavy()
    {
        AudioSource output;
        Instantiate(outputObject, transform).TryGetComponent(out output);


        int pick = Random.Range(0, ExtremelyHeavySounds.Length);
        output.clip = ExtremelyHeavySounds[pick];
        output.Play();
    }

    public void PlayRandomEnhancer()
    {
        AudioSource output;
        Instantiate(outputObject, transform).TryGetComponent(out output);
        

        int pick = Random.Range(0, EnhancerSounds.Length);
        output.clip = EnhancerSounds[pick];
        output.Play();
    }
    public void PlayRandomKO()
    {
        AudioSource output;
        Instantiate(outputObject, transform).TryGetComponent(out output);

        int pick = Random.Range(0, KOSounds.Length);
        output.clip = KOSounds[pick];
        output.Play();
    }

    public void PlayRandomLightSound()
    {
        AudioSource output;
        Instantiate(outputObject, transform).TryGetComponent(out output);

        int pick = Random.Range(0, LightHitSounds.Length);
        output.clip = LightHitSounds[pick];
        output.Play();
    }

    public void PlayRandomSuperHeavySound()
    {
        AudioSource output;
        Instantiate(outputObject, transform).TryGetComponent(out output);

        int pick = Random.Range(0, SuperHeavyHitSounds.Length);
        output.clip = SuperHeavyHitSounds[pick];
        output.Play();
    }

    public void PlayRandomHeavySound()
    {
        AudioSource output;
        Instantiate(outputObject, transform).TryGetComponent(out output);

        int pick = Random.Range(0, HeavyHitSounds.Length);
        output.clip = HeavyHitSounds[pick];
        output.Play();
    }



    public void PlaySound(string name)
    {
        foreach(AudioClip clip in LightHitSounds)
        {
            if(clip.name == name)
            {
                AudioSource output;
                Instantiate(outputObject, transform).TryGetComponent(out output);

                output.clip = clip;
                output.Play();
                break;
            }
        }

        foreach (AudioClip clip in HeavyHitSounds)
        {
            if (clip.name == name)
            {
                AudioSource output;
                Instantiate(outputObject, transform).TryGetComponent(out output);

                output.clip = clip;
                output.Play();
                break;
            }
        }

        foreach (AudioClip clip in SuperHeavyHitSounds)
        {
            if (clip.name == name)
            {
                AudioSource output;
                Instantiate(outputObject, transform).TryGetComponent(out output);

                output.clip = clip;
                output.Play();
                break;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
