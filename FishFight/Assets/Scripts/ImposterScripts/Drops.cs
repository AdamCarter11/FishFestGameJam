using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    [SerializeField] float lifetime = 5f;

    public void StartDestroyTime()
    {
        Destroy(this.gameObject, lifetime);
    }
}
