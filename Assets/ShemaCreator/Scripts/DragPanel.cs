using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragPanel : MonoBehaviour
{
    [SerializeField] GameObject elementPref;
    [SerializeField] Transform contentTransform;
    [SerializeField] List<Sprite> materials;
    [SerializeField] List<Image> shemElements;
    [SerializeField] GameObject shemaManager;
   
    [Space]
    [Header("Части робота")]
    [SerializeField] List<GameObject> robotElement;

    void Start()
    {
        var shemaM = shemaManager.GetComponent<ShemaManager>();
        for (int i = 0; i < materials.Count; i++)
        {
            var elemObject = Instantiate(elementPref, contentTransform);
            var script = elemObject.GetComponent<ScrollElement>();

            script.MainMaterial = materials[i];
            script.ShemaElement = shemElements[i];
            script.shemaManager = shemaManager;
            script.robotElement = robotElement[i];
            Debug.Log(shemElements[i].name);
            shemaM.AddButton(elemObject);
        }
    }
}
