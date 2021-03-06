﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCollider : MonoBehaviour
{
    public bool axis; // True: left/right axis. False: top/bottom axis

    private Transform shape;
    private Vector3 center;
    private float startDistance;
    private float startScale;
    private float startAngle;
    private float startRotation;

    // Start is called before the first frame update
    void Start()
    {
        shape = gameObject.transform.parent;
        SetStartVars();
    }

    // Update is called once per frame
    void Update()
    {
        center = Camera.main.WorldToScreenPoint(shape.position);
    }

    private void SetStartVars()
    {
        center = Camera.main.WorldToScreenPoint(shape.position);
        startDistance = Vector3.Distance(center, Input.mousePosition);
        startScale = axis ? shape.localScale.x : shape.localScale.y;
        startAngle = Vector3.SignedAngle(Vector3.right, Input.mousePosition - center, Vector3.forward);
        startRotation = shape.eulerAngles.z;
    }

    private void OnMouseDown()
    {
        SetStartVars();
    }

    private void OnMouseDrag()
    {
        if (axis)
        {
            shape.localScale = new Vector3((Vector3.Distance(center, Input.mousePosition) / startDistance) * startScale, shape.localScale.y, shape.localScale.z);
        }
        else
        {
            shape.localScale = new Vector3(shape.localScale.x, (Vector3.Distance(center, Input.mousePosition) / startDistance) * startScale, shape.localScale.z);
        }

        shape.eulerAngles = new Vector3(shape.eulerAngles.x, shape.eulerAngles.y, (Vector3.SignedAngle(Vector3.right, Input.mousePosition - center, Vector3.forward) - startAngle) + startRotation);
    }
}
