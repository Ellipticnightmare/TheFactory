using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public string ObjName;
    public GameObject parent;
    public float scale;
    // Start is called before the first frame update
    void Start()
    {
        name = ObjName;
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(scale, scale, scale);
        transform.position = parent.transform.position;
    }
}