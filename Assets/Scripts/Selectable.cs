using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{

    public GameObject model;

    public Material normalMaterial;
    public Material highlightedMaterial;

    public string highlightMessage;

    public bool interactable = false;

    private MeshRenderer modelRenderer;
    private bool highlighted;

    // Start is called before the first frame update
    protected void Start()
    {
        highlighted = false;
        modelRenderer = model.GetComponent<MeshRenderer>();
    }

    public void highlight()
    {
        highlighted = true;
        // Set highlighted material
        setMaterial(highlightedMaterial);
    }

    public void revertHighlight()
    {
        highlighted = false;
        // Set normal material
        setMaterial(normalMaterial);
    }

    protected void setMaterial(Material m)
    {
        Material[] materials = modelRenderer.materials;
        materials[0] = m;
        modelRenderer.materials = materials;
    }

    public bool isHighlighted()
    {
        return highlighted;
    }

    public void interact()
    {
        if (interactable)
        {
            this.GetComponent<Interactable>().interact();
        }
    }
}
