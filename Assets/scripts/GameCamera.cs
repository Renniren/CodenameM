using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Camera cam;
    public float LerpSpeed = 0.2f, AdditiveLerpSpeed = 0.04f, FOVLerpSpeed = 0.09f;
    public float offset = 1;
    public float Rotation = 15;
    public float XAddPercent = 1.3f, YAddPercent = 0.8f;
    public float MaxOffsetAdd = 20;
    float additive;
    float DefaultFOV;

    Vector3 center;
    Vector3 to;

    // Start is called before the first frame update
    void Start()
    {
        DefaultFOV = cam.fieldOfView;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(center, 1);
        Gizmos.DrawWireSphere(to, 1);
    }

    List<Vector3> positions;
    // Update is called once per frame
    void Update()
    {
        foreach(Entity ent in Entity.ActiveGameEntities)
        {
            center += ent.transform.position;
            if(Entity.ActiveGameEntities.Count > 1) center /= 1.9f;
        }
        additive = Mathf.Lerp(additive, center.magnitude, AdditiveLerpSpeed);
        additive = Mathf.Clamp(additive, 0, MaxOffsetAdd);

        transform.eulerAngles = new Vector3(Rotation - additive * XAddPercent, 0 + additive * YAddPercent, 0);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, DefaultFOV - additive, FOVLerpSpeed);

        to = center + transform.forward * (offset - additive);
        transform.position = Vector3.Lerp(transform.position, to, LerpSpeed);
    }
}
