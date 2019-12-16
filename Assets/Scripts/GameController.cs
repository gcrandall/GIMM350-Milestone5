using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    //public GameObject player;
    public int maxResources = 100;

    public GameObject leftHandUI;
    public Text resourcesUsedText;
    public Text resourcesLeftText;
    public GameObject resourcesBar;
    public Text itemText;
    public Text tooltipText;
    public Text statusText;
    public Text winText;

    public GameObject[] items;

    private bool rightTriggerPressed;
    private bool leftTriggerPressed;

    private int resourcesUsed = 0;
    private int currentItem = 0;

    // Start is called before the first frame update
    void Start()
    {
        leftTriggerPressed = false;
        rightTriggerPressed = false;
        winText.text = "";
        updateItemText();
        updateResourcesUI();
    }

    // Update is called once per frame
    void Update()
    {
        cycleItems();
    }

    private void cycleItems()
    {
        // If left trigger is pressed
        if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0.8f && leftTriggerPressed == false)
        {
            leftTriggerPressed = true;
            currentItem--;
            if (currentItem < 0)
            {
                currentItem = items.Length - 1;
            }
            currentItem = currentItem % items.Length;
            updateItemText();
        }
        else if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) < 0.1f)
        {
            leftTriggerPressed = false;
        }

        // If right trigger is pressed
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.8f && rightTriggerPressed == false)
        {
            rightTriggerPressed = true;
            currentItem++;
            currentItem = currentItem % items.Length;
            updateItemText();
        } else if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < 0.1f)
        {
            rightTriggerPressed = false;
        }
    }

    public GameObject getCurrentItem()
    {
        return items[currentItem];
    }

    public bool canSpawn(GameObject item)
    {
        int itemCost = item.GetComponent<SpawnableItem>().cost;
        if (resourcesUsed + itemCost <= maxResources)
        {
            return true;
        }
        else
        {
            //Debug.Log(resourcesUsed + " used out of " + maxResources);
            return false;
        }
    }

    public void useResources(GameObject item)
    {
        int itemCost = item.GetComponent<SpawnableItem>().cost;
        resourcesUsed += itemCost;
        updateResourcesUI();
    }

    public void freeResources(GameObject item)
    {
        int itemCost = item.GetComponent<SpawnableItem>().cost;
        resourcesUsed -= itemCost;
        updateResourcesUI();
    }

    public void updateResourcesUI()
    {
        float maxWidth = leftHandUI.GetComponent<RectTransform>().rect.width;
        float newWidth = ((float) resourcesUsed / (float) maxResources) * maxWidth;
        RectTransform rt = resourcesBar.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(newWidth, rt.sizeDelta.y);
        resourcesUsedText.text = resourcesUsed.ToString();
        resourcesLeftText.text = (maxResources - resourcesUsed).ToString();
    }

    public void updateItemText()
    {
        itemText.text = items[currentItem].name;
    }

    public void updateStatus(string message)
    {
        statusText.text = message;
    }

    public void updateTooltip(string message)
    {
        tooltipText.text = message;
    }

    private PlayTime CreateTimePlayed()
    {
        PlayTime t = new PlayTime();
        t.seconds = Time.realtimeSinceStartup;

        return t;
    }

    public double SaveTime()
    {
        double lastTime = LoadTime();
        PlayTime t = CreateTimePlayed();

        if (lastTime > 0 && lastTime > t.seconds)
        {
            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/time.save");
            bf.Serialize(file, t);
            file.Close();
        }

        return t.seconds;
    }

    public double LoadTime()
    {
        double lastTime = -1;
        // 1
        if (File.Exists(Application.persistentDataPath + "/time.save"))
        {

            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/time.save", FileMode.Open);
            PlayTime savedTime = (PlayTime)bf.Deserialize(file);
            file.Close();

            lastTime = savedTime.seconds;
        }
        return lastTime;
    }

    public void winLevel()
    {
        double thisTime = SaveTime();
        double bestTime = LoadTime();
        //winText.text = "win";
        winText.text = "Level completed in " + thisTime.ToString("F3") + " seconds";
        winText.text += "\n";
        winText.text += "Best time: " + bestTime.ToString("F3") + " seconds";
        updateStatus("");
    }

}
