using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : Interactable
{

    public GameController controller;

    override public void interact()
    {
        var d = this.GetComponent<Door>();
        if (d.isActivated())
        {
            // win
            controller.winLevel();
        }
        else
        {
            controller.updateStatus("You cannot leave yet.");
        }
    }
}
