﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapJarbud : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y + 18);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
