using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInput : MonoBehaviour
{
    public string useString;
    public bool isActive;
    public void Activate()
    {
        isActive = !isActive;   
        GetComponent<Animator>().SetBool("active", isActive);
    }
}