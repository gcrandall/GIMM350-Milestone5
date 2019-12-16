using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButton : EnvironmentButton
{

    public void toggleState()
    {
        if (isActivated())
        {
            this.GetComponent<Selectable>().normalMaterial = normalMaterial;
            deactivate();
        } else
        {
            this.GetComponent<Selectable>().normalMaterial = activatedMaterial;
            activate();
        }
    }
}
