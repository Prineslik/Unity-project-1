using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollElement : MonoBehaviour
{
    [SerializeField] private Image mainImage;

    private Sprite mainMaterial;
    private Image shemaElement;

    public GameObject shemaManager;

    [SerializeField] public GameObject robotElement { get; set; }

    public Sprite MainMaterial
    {
        get { return mainMaterial; }
        set
        {
            if (value != null)
            {
                mainMaterial = value;
                if (mainImage != null)
                    mainImage.sprite = mainMaterial;
               
            }
        }
    }

    public Image ShemaElement
    {
        get { return shemaElement; }
        set 
        {
            if (value != null)
            {
                shemaElement = value;
            }
        }
    }

    public void OnElementClick()
    {
        Debug.Log("Ёлемент - " + robotElement.name.ToString());
        shemaManager.GetComponent<ShemaManager>().CreateElement(shemaElement, robotElement);
    }
}
