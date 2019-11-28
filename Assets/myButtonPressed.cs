using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class myButtonPressed : MonoBehaviour
{
    // Start is called before the first frame update
    public myButton my_button;
    private Quaternion RotStart;
    private Vector3 startPos;

    void Start()
    {
        startPos = GetComponent<Transform>().position;
        RotStart = GetComponent<Transform>().rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (my_button.Pressed)
        {
            GetComponent<Transform>().position = startPos;
            GetComponent<Transform>().rotation = RotStart;

        }
    }
}
