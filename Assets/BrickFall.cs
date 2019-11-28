using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BrickFall : MonoBehaviour
{
    public Text youWonText;
    public GameObject underObject;
    private Transform Location;

    private Text startText;
    private Vector3 StartPos;
    private Quaternion RotStart;
    private Vector3 StartPos2;
    private Quaternion RotStart2;
    // Start is called before the first frame update
    void Start()
    {
        startText = youWonText;
        Location = GetComponent<Transform>();
        StartCoroutine(waiter());
        StartPos = Location.position;
        RotStart = Location.rotation;
        StartPos2 = underObject.GetComponent<Transform>().position;
        RotStart2 = underObject.GetComponent<Transform>().rotation;
    }

    IEnumerator waiter()
    {
        //print(Time.time);
        yield return new WaitForSeconds(5);
        //print(Time.time);
    }


    // Update is called once per frame
    void Update()
    {
        if (Location.position.y < 0.3)
        {
            youWonText.text = "Well done!!!";
            StartCoroutine(waiter());
            youWonText.text = "Try again !!!";
            //Location.position = StartPos;
            //Location.rotation = RotStart;
            StartCoroutine(waiter());
            youWonText = startText;
            //underObject.GetComponent<Transform>().position = StartPos2;
            //underObject.GetComponent<Transform>().rotation = RotStart2;


        }
    }
}
