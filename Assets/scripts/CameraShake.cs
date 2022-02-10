using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static float ShakeAmount;
    public static float RollShakeAmount;
    public float MaxShake = 3;
    public float RecoveryTime = 0.05f;
    public float RollRecoveryTime = 0.08f;
    Vector3 def;
    Vector3 defrot;
    // Start is called before the first frame update
    void Start()
    {
        def = transform.localPosition;
        defrot = transform.localEulerAngles;
    }

    

    // Update is called once per frame
    void Update()
    {
        ShakeAmount = Mathf.Lerp(ShakeAmount, 0, RecoveryTime);
        RollShakeAmount = Mathf.Lerp(RollShakeAmount, 0, RollRecoveryTime);

        transform.localEulerAngles = def + transform.InverseTransformDirection(Random.onUnitSphere) * RollShakeAmount;
        transform.localPosition = def + transform.InverseTransformDirection(Random.onUnitSphere) * ShakeAmount;
    }
}
