using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScript : MonoBehaviour
{
    public Vector3 theThreeAxes = new Vector3(1, 1, 1);
    private Vector3 privateVar;
    Vector3 newVar;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = theThreeAxes;
        Debug.Log("Hello World, the position is: " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
