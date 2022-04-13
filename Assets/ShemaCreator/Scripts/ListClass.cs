using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListClass : MonoBehaviour
{
    public GameObject element { get; set; }
    public Quaternion rotationElement { get; set; }

    public ListClass() { }

    public ListClass(GameObject Element, Quaternion RotationElement)
    {
        element = Element;
        rotationElement = RotationElement;
    }
}
