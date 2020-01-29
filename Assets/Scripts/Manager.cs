using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int currentScreen = 0;
    public int lastScreen = 0;

    public GameObject[] screens;
    public bool vuforiaCard;
    public GameObject imageTarget;
    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentScreen <= 2)
        {
            //if (vuforiaCard) { vuforiaCard = false; imageTarget.SetActive(false); }
        }
        else
        {
            //if (!vuforiaCard) { vuforiaCard = true; imageTarget.SetActive(true); }
        }
    }

    public void ChangeScreen(int newScreen)
    {
        lastScreen = currentScreen;
        screens[currentScreen].SetActive(false);
        screens[newScreen].SetActive(true);
        currentScreen = newScreen;
    }
}
