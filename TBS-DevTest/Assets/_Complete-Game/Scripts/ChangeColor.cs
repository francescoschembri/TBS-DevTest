using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == gameObject.tag)
        {
            return;
        }
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
        renderer.materials[0].color = Color.white;
    }
}
