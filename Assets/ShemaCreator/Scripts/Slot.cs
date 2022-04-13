using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] public GameObject shemaManager;

    private void Start()
    {
        shemaManager = GameObject.Find("ShemaManager");
    }

    public void OnSlotClick()
    {
        shemaManager.GetComponent<ShemaManager>().choseSlot = gameObject;
    }
}
