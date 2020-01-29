using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FileSelectInfo : MonoBehaviour
{
    public int videoIndex;
    public string videoName;
    public GameObject display;
    private Manager manager;

    public void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        display = manager.screens[3];
    }

    public void Click()
    {
        if (display.GetComponentInChildren<TextMeshProUGUI>()) Debug.Log(display.GetComponentInChildren<TextMeshProUGUI>().transform.parent.name);
        display.GetComponentInChildren<TextMeshProUGUI>().text = videoName;
        manager.ChangeScreen(3);
    }
}
