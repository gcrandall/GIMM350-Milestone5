using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Selectable
{
    public GameObject frameModel;
    public GameObject lights;
    public Material activatedFrameMaterial;
    public Material deactivatedFrameMaterial;

    private string deactivatedMessage;
    public string activatedMessage;

    private MeshRenderer frameRenderer;

    private bool activated;

    private void Start()
    {
        base.Start();
        activated = false;
        lights.SetActive(false);
        frameRenderer = frameModel.GetComponent<MeshRenderer>();
        deactivatedMessage = highlightMessage;
    }

    public void activate()
    {
        activated = true;
        highlightMessage = activatedMessage;
        lights.SetActive(true);
        setFrameMaterial(activatedFrameMaterial);
    }

    public void deactivate()
    {
        activated = false;
        highlightMessage = deactivatedMessage;
        lights.SetActive(false);
        setFrameMaterial(deactivatedFrameMaterial);
    }

    private void setFrameMaterial(Material m)
    {
        Material[] materials = frameRenderer.materials;
        materials[0] = m;
        frameRenderer.materials = materials;
    }

    public bool isActivated()
    {
        return activated;
    }
}
