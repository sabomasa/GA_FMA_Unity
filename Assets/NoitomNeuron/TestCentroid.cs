﻿using UnityEngine;
using System.Collections;

public class TestCentroid : MonoBehaviour
{
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = transform.FindChild("CenterOfMass").position;
    }
}
