﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStartPosition : MonoBehaviour
{
	void Start()
    {
        float hw = Arena.Width * 0.5f;
        float hh = Arena.Height * 0.5f;
        float x = Random.Range( -hw, hw );
        float z = Random.Range(-hh, hh);
        transform.position = new Vector3(x, 1.5f, z);
    }
}
