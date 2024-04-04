using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// Base class for actions to select
/// </summary>
public class ActionBehavior : MonoBehaviour
{
    public Player playerRef = null;

    // Has to have ref to player
    public void RecieveAction(mACTION_TYPE type, Player opponent)
    {
        if (!playerRef || !opponent)
        {
            Debug.Log("no assigned player");
            return;
        }

        switch (type)
        {
            case (mACTION_TYPE.quick):
                break;
        }
    }

    public virtual int RequestDamage()
    {
        return 0;
    }
    
    public virtual int RequestMitigate()
    {
        return 0;
    }

    public virtual void RequestLimitDraw()
    {
        // 
    }

    public virtual void RequestStun()
    {
        // 
    }

    public virtual void RequestDisable()
    {
        // 
    }

    public virtual void RequestUncounterable()
    {
        //
    }
}

public enum mACTION_TYPE
{
    quick,
    strong,
    parry,
}

