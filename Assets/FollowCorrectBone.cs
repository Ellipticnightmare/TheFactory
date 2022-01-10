using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCorrectBone : MonoBehaviour
{
    public GameObject targetPos;
    public string targetString;
    // Start is called before the first frame update
    void Start()
    {
        targetString = name;
        targetPos = GameObject.FindGameObjectWithTag(targetString);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = targetPos.transform.position;   
    }
}