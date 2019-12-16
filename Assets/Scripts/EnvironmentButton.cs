using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentButton : MonoBehaviour
{

    public GameObject buttonModel;
    public GameObject buttonLight;

    public Material normalMaterial;
    public Material activatedMaterial;

    private MeshRenderer buttonRenderer;
    private bool activated;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        buttonRenderer = buttonModel.GetComponent<MeshRenderer>();
    }

    protected void activate()
    {
        activated = true;
        buttonLight.SetActive(activated);
        // Set activated material
        setMaterial(activatedMaterial);
    }

    protected void deactivate()
    {
        activated = false;
        buttonLight.SetActive(activated);
        // Set normal material
        setMaterial(normalMaterial);
    }

    public bool isActivated()
    {
        return activated;
    }

    protected void setMaterial(Material m)
    {
        Material[] materials = buttonRenderer.materials;
        materials[0] = m;
        buttonRenderer.materials = materials;
    }
}
