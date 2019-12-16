using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundButton : EnvironmentButton
{

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    if (isActivated())
        //    {
        //        deactivate();
        //    } else
        //    {
        //        activate();
        //    }
        //}
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        //Debug.Log("COLLIDING");
        if (!isActivated())
        {
            activate();
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (isActivated())
        {
            deactivate();
        }
    }
}
