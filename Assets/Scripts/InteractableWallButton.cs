using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWallButton : Interactable
{
    override public void interact() {
        this.GetComponent<WallButton>().toggleState();
    }
}
