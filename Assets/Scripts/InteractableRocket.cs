using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRocket : Interactable
{

    public float thrust;
    public GameObject particles;

    private bool activated;
    private Rigidbody rb;

    private void Start()
    {
        particles.SetActive(false);
        activated = false;
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (activated)
        {
            rb.AddForce(transform.forward * thrust);
            Debug.Log("moving");
        }
    }

    override public void interact()
    {
        if (activated)
        {
            deactivate();
        }
        else
        {
            activate();
        }
    }

    private void activate()
    {
        activated = true;
        particles.SetActive(true);
    }

    private void deactivate()
    {
        activated = false;
        particles.SetActive(false);
    }
}
