using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public static List<Entity> ActiveGameEntities = new List<Entity>();

    private void Start()
    {
        Debug.Log(gameObject);
    }

    private void OnEnable()
    {
        if(!ActiveGameEntities.Contains(this))
        {
            ActiveGameEntities.Add(this);
        }
    }

    private void OnDisable()
    {
        if (ActiveGameEntities.Contains(this))
        {
            ActiveGameEntities.Remove(this);
        }
    }
}