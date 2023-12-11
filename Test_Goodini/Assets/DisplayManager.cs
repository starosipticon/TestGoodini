using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int displayCount = Display.displays.Length;
        
        for (int i = 1; i< displayCount; i++)
        {
            Display.displays[i].Activate();
        }
    }
}
