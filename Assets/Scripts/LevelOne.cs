using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOne : MonoBehaviour
{
    public Door door;
    public EnvironmentButton cButton;
    public EnvironmentButton wButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cButton.isActivated() && wButton.isActivated())
        {
            if (!door.isActivated())
            {
                door.activate();
            }
        }
        else
        {
            if (door.isActivated())
            {
                door.deactivate();
            }
        }
    }
}
