using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvItemAdd : MonoBehaviour
{
    public Inventory inventory;
    public Item testItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Premuto T");
            inventory.Add(testItem, 1);
        }
    }
}
