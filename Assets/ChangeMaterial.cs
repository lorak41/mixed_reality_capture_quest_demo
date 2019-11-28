using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    // Start is called before the first frame update
    public Material newMat;

    void change(Material newMat)
     {
         Renderer[] children;
         children = GetComponentsInChildren<Renderer>();
         foreach (Renderer rend in children)
         {
             var mats = new Material[rend.materials.Length];
             for (var j = 0; j < rend.materials.Length; j++)
             {
                 mats[j] = newMat;
             }
             rend.materials = mats;
         }
     }

    void Start()
    {
        change(newMat);
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
