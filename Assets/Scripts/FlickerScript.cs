using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickerScript : MonoBehaviour
{
    public Text startText;
    private int counter =0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(counter==90)
        {
            startText.enabled = false;
        }
        if(counter==180)
        {
            startText.enabled = true;
            counter = 0;
        }
        counter++;
    }
}
