using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyGameReset : MonoBehaviour
{
    public GameObject leftToSetActive;
    public GameObject rightToSetActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool is_pause_button = OVRInput.Get(OVRInput.Button.Start, OVRInput.Controller.LTouch);
        //bool is_X_pressed = OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.LTouch);
        //bool is_Y_pressed = OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.LTouch);
        //if (is_Y_pressed && is_X_pressed)
        if (is_pause_button)
            SceneManager.LoadScene("SampleScene");

        bool is_A_pressed = OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch);
        bool is_B_pressed = OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.RTouch);

        if (is_A_pressed && is_B_pressed)
        {
            if(!leftToSetActive.activeSelf && !rightToSetActive.activeSelf)
            {
                leftToSetActive.SetActive(true);
                rightToSetActive.SetActive(true);
            }
        }
            

    }
}
