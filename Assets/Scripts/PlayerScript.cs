using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public GameObject gameController;

    public int playerRange = 2;

    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject controlsDisplay;

    private GameController controller;
    private GameObject currentItem;
    private GameObject highlightedObject;
    private bool leftHandPointing;
    private bool rightHandPointing;

    private LineRenderer lineRenderer;
    private Ray pointer;

    private bool controlsDismissed;

    // Use this for initialization
    void Start()
    {
        controller = gameController.GetComponent<GameController>();
        leftHandPointing = false;
        rightHandPointing = false;
        if (lineRenderer == null)
        {
            Debug.LogWarning("Assign a line renderer in the inspector!");
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineRenderer.receiveShadows = false;
            lineRenderer.widthMultiplier = 0.0f;
            lineRenderer.material.color = Color.white;
        }
        highlightedObject = null;
        controlsDismissed = false;

        //https://forums.oculusvr.com/developer/discussion/48156/trying-to-get-a-finger-transform
        //OvrAvatar ovAdv = transform.GetComponent<OvrAvatar>();
        //ovAdv.AssetsDoneLoading.AddListener(Find);
    }

    // Update is called once per frame
    void Update()
    {
        // Determine if hands are pointing
        if (OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger) == false)
        {
            rightHandPointing = true;
        } else
        {
            rightHandPointing = false;
        }
        if (OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger) == false)
        {
            leftHandPointing = true;
        } else
        {
            leftHandPointing = false;
        }

        if (rightHandPointing)
        {
            GameObject objectPointedAt = handRaycastObject(rightHand);
            // Only make changes if the object pointed at is different from the previous highlightedObject
            if (!GameObject.ReferenceEquals(highlightedObject, objectPointedAt))
            {
                // Clear highlightedObject variable if it isn't null
                if (highlightedObject != null)
                {
                    highlightedObject.GetComponent<Selectable>().revertHighlight();
                    highlightedObject = null;
                }
                // If the raycast finds an object and it's selectable, make it the new highlighted object
                if (objectPointedAt != null)
                {
                    if (objectPointedAt.GetComponent<Selectable>() != null)
                    {
                        highlightedObject = objectPointedAt;
                        var selectableComponent = highlightedObject.GetComponent<Selectable>();
                        selectableComponent.highlight();
                        controller.updateTooltip(selectableComponent.highlightMessage);
                    }
                } else
                {
                    controller.updateTooltip("");
                }
            }
        } else
        {
            // Clear highlightedObject variable if it isn't null
            if (highlightedObject != null)
            {
                highlightedObject.GetComponent<Selectable>().revertHighlight();
                highlightedObject = null;
            }
            hideRaycast();
            controller.updateTooltip("");
        }

        // Delete objects
        if (OVRInput.GetUp(OVRInput.RawButton.B) == true)
        {
            if (highlightedObject != null)
            {
                if (highlightedObject.GetComponent<SpawnableItem>() != null)
                {
                    deleteObject(highlightedObject);
                }
                else
                {
                    controller.updateStatus("Cannot delete this object");
                }
            }
        }
        
        // Interact with or spawn objects
        if (OVRInput.GetUp(OVRInput.RawButton.A) == true)
        {
            if (controlsDismissed == false)
            {
                controlsDisplay.SetActive(false);
                controlsDismissed = true;
            }
            else
            {
                if (highlightedObject != null)
                {
                    if (highlightedObject.GetComponent<Selectable>().interactable)
                    {
                        highlightedObject.GetComponent<Selectable>().interact();
                    }
                }
                else
                {
                    spawnObject(rightHand);
                }
            }
        }
        if (OVRInput.GetUp(OVRInput.RawButton.X) == true)
        {
            spawnObject(leftHand);
        }

        /*if (OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger) == false)
        {
            if (highlightedObject != null)
            {
                highlightedObject.GetComponent<Selectable>().revertHighlight();
                highlightedObject = null;
            }
            GameObject objectPointedAt = handRaycast(rightHand, 2);
            Selectable selectableComponent = objectPointedAt.GetComponent<Selectable>();
            if (selectableComponent != null)
            {
                highlightedObject = objectPointedAt;
                selectableComponent.highlight();
            }

            if (OVRInput.GetUp(OVRInput.RawButton.B) == true)
            {
                if (highlightedObject != null)
                {
                    if (highlightedObject.GetComponent<SpawnableItem>() != null)
                    {
                        deleteObject(highlightedObject);
                    }
                }
            }
        } else
        {
            lineRenderer.widthMultiplier = 0.0f;
            if (highlightedObject != null)
            {
                highlightedObject.GetComponent<Selectable>().revertHighlight();
                highlightedObject = null;
            }
        }*/

        // Left hand
        //Debug.Log(OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger));
        // Right hand
        //Debug.Log(OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger));

        /*if (OVRInput.Get(OVRInput.Touch.LIndexTrigger) == true)
        {
            Debug.Log("pointing");
        }*/

        /*
        //Delete
        if (Input.GetKeyDown(KeyCode.R))
        {
            deleteObject();
        }

        //Grab
        if (Input.GetMouseButtonDown(0))
        {
            grabObject();
        }

        //Freeze Object
        if (Input.GetMouseButtonDown(1))
        {
            freezeObject();
        }

        //Interact with Object
        if (Input.GetKeyDown(KeyCode.F))
        {
            interactObject();
        }


        //If you have an item in your hands, move it with the camera
        if (itemInHands != null)
        {
            //Vector3 newPos = this.transform.position + (this.transform.forward * 3);
            Vector3 newPos = Camera.main.transform.position + (Camera.main.transform.forward * 3);
            //itemInHands.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
            itemInHands.transform.SetPositionAndRotation(newPos, Camera.main.transform.rotation);
        }
        */
    }

    /*
    void interactObject()
    {
        if (itemInHands == null)
        {
            GameObject o = playerRaycast(playerRange);
            InteractiveItem i;
            if (o != null)
            {
                i = o.GetComponent<InteractiveItem>();
                Debug.Log(o + " " + i);
                if (o != null && i != null)
                {
                    Debug.Log("interacted with " + i.name);
                    i.Interact();
                }
            }
        }
        else
        {
            itemInHands = null;
        }
    }
    */

    /*
    void grabObject()
    {
        if (itemInHands == null)
        {
            GameObject o = playerRaycast(playerRange);
            if (o != null && o.GetComponent<SpawnableItem>() != null && o.GetComponent<SpawnableItem>().grabbable)
            {
                Debug.Log("grabbed " + o.name);
                itemInHands = o;
            }
        }
        else
        {
            itemInHands = null;
        }
    }
    */

    /*
    void freezeObject()
    {
        GameObject o;
        if (itemInHands != null)
        {
            o = itemInHands;
        }
        else
        {
            o = playerRaycast(playerRange);
        }
        Rigidbody rb = o.GetComponent<Rigidbody>();
        SpawnableItem si = o.GetComponent<SpawnableItem>();
        if (rb != null && si != null)
        {
            if (o == itemInHands)
            {
                itemInHands = null;
            }
            if (rb.isKinematic == false)
            {
                rb.isKinematic = true;
            }
            else
            {
                rb.isKinematic = false;
            }
        }
    }
    */

    void spawnObject(GameObject hand)
    {
        GameObject item = getCurrentItem();
        if (controller.canSpawn(item))
        {
            Vector3 newPos = hand.transform.position + (hand.transform.forward * 1);
            controller.updateStatus("spawned " + item.GetComponent<SpawnableItem>().name);
            Instantiate(item, newPos, hand.transform.rotation);
            controller.useResources(item);
        }
        else
        {
            controller.updateStatus("Need more resources!");
        }
    }

    void deleteObject(GameObject o)
    {
        SpawnableItem sp = o.GetComponent<SpawnableItem>();
        if (o != null && sp != null)
        {
            Debug.Log("deleted " + o.name);
            controller.freeResources(o);
            controller.updateStatus("deleted " + sp.name);
            Destroy(o);
        }
    }

    GameObject getCurrentItem()
    {
        return controller.getCurrentItem();
    }

    void createHandRaycast(GameObject hand)
    {
        var rayX = hand.transform.position.x;
        var rayY = hand.transform.position.y;
        var rayZ = hand.transform.position.z;
        var indexFingerPos = new Vector3(rayX, rayY, rayZ);

        pointer = new Ray(indexFingerPos, hand.transform.forward);
        lineRenderer.widthMultiplier = 0.01f;
        lineRenderer.SetPosition(0, pointer.origin);
        lineRenderer.SetPosition(1, pointer.origin + pointer.direction * playerRange);
    }

    GameObject handRaycastObject(GameObject hand)
    {
        createHandRaycast(hand);
        RaycastHit hit;
        if (Physics.Raycast(pointer, out hit, playerRange))
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    GameObject handRaycastPoint(GameObject hand)
    {
        createHandRaycast(hand);
        return null;
    }

    private void hideRaycast()
    {
        lineRenderer.widthMultiplier = 0;
    }

    //r = range
    //GameObject handRaycast(GameObject hand, int r)
    //{
    //    var rayX = hand.transform.position.x;
    //    var rayY = hand.transform.position.y;
    //    var rayZ = hand.transform.position.z;
    //    var indexFingerPos = new Vector3(rayX, rayY, rayZ);

    //    Ray pointer = new Ray(indexFingerPos, hand.transform.forward);
    //    lineRenderer.widthMultiplier = 0.01f;
    //    lineRenderer.SetPosition(0, pointer.origin);
    //    lineRenderer.SetPosition(1, pointer.origin + pointer.direction * r);

    //    RaycastHit hit;
    //    if (Physics.Raycast(pointer, out hit, r))
    //    {
    //        return hit.collider.gameObject;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}

    //r = range
    /*
    GameObject playerRaycast(int r)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, r))
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
    */

    void isItemInHand(GameObject hand)
    {

    }

    void getObjectInHand(GameObject hand)
    {
        /*for (int i = 0; i < hand.GetChildCount(); i++)
        {
            GameObject Go = this.gameobject.transform.GetChild(i);
        }*/
    }
}
