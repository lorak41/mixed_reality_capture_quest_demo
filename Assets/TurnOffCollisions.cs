using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffCollisions : MonoBehaviour
{
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.detectCollisions = false;
    }
}
