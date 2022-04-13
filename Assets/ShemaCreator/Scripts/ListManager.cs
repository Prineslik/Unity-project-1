using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    [SerializeField] public List<ListClass> shema;

    [Header("Стартовый объект")]
    [SerializeField] private GameObject startElement;

    public void AddNode(GameObject Element, Quaternion RotationElem)
    {
        shema.Add(new ListClass(Element,RotationElem));
    }

    public void DeleteLastNode()
    {
        Debug.Log("DELETE CLASS - "+ shema[shema.Count - 1].element.name);
        shema.RemoveAt(shema.Count - 1);
       
    }

    public ListClass lastElement()
    {
        return shema[shema.Count - 1];
    }

    public Quaternion lastElemRotation()
    {
        return shema[shema.Count-1].rotationElement;
    }
}
