using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    [SerializeField] Brain brain;
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(brain.foodFilled && brain.algeaFilled && brain.rockFilled)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, .5f);
        }
    }
}
