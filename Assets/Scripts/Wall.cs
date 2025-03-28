using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour //secondo script visto a lezione
{
    public GameObject block;
    public int width = 10;
    public int height = 4;
    // Codice eseguito tutto assieme una volta startato il gioco
    void Start()
    {
        for(int y = 0; y<height; ++y)
        {
            for (int x = 0; x<width; ++x)
            {
                Instantiate(block, new Vector3(x + 1, y, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Destroy(block);
        }
    }
}
