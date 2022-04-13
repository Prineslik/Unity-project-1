using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenuManager : MonoBehaviour
{
    [Header("Меню управления роботом")]
    public GameObject controlMenu;
    [Space]
    [Header("Перемещение")]
    public Vector3 posOpen;
    public Vector3 posClose;
    public float moveSpeed;

    private bool menuChek;

    private void Start()
    {
        controlMenu.transform.localPosition = posClose;
        Debug.Log("Position - "+controlMenu.transform.position);

        //controlMenu.transform.position = posClose;
        menuChek = false;
    }

    public void closeOpenMenu()
    {
        Debug.Log("MOVEEE");
        if (menuChek)
        {
            //while (controlMenu.transform.position != posClose)
            //{
            //controlMenu.transform.position = Vector3.MoveTowards(controlMenu.transform.position, posClose, moveSpeed * Time.deltaTime);
            menuChek = false;
            StartCoroutine(MoveTarget(controlMenu.transform, posClose, moveSpeed));
            //}
            
        }
        else
        {
            //while (controlMenu.transform.position != posOpen)
            //{
                //controlMenu.transform.position = Vector3.MoveTowards(controlMenu.transform.position, posOpen, moveSpeed * Time.deltaTime);
            menuChek = true;
            StartCoroutine(MoveTarget(controlMenu.transform, posOpen, moveSpeed));
            //}
        }
    }

    public void ifClose()
    {
        if (!menuChek)
        {
            menuChek = true;
            StartCoroutine(MoveTarget(controlMenu.transform, posOpen, moveSpeed));
        }
    }

    public void ifOpen()
    {
        if (menuChek)
        {
            menuChek = false;
            StartCoroutine(MoveTarget(controlMenu.transform, posClose, moveSpeed));
        }
    }

    public static IEnumerator MoveTarget(Transform obj, Vector3 target, float speed)
    {
        while (obj.localPosition != target)
        {
            //Debug.Log("ITERATION");
            obj.localPosition = Vector3.MoveTowards(obj.localPosition,target, speed * Time.deltaTime);
            yield return null;
        }
    }
}
