using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMakerManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject cilindr;
    [SerializeField] private GameObject capsule;
    [SerializeField] private GameObject startSlot;


    private float hCube;
    private float hCilindr;
    private float hCapsule;

    //void Start()
    //{
    //    hCube = cube.GetComponent<BoxCollider>().bounds.size.y;
    //    hCilindr = cilindr.GetComponent<CapsuleCollider>().bounds.size.y;
    //    hCapsule = capsule.GetComponent<CapsuleCollider>().bounds.size.y;

    //    Debug.Log("¬€—Œ“¿  ”¡¿ = " + hCube);
    //    Debug.Log("¬€—Œ“¿ ÷»À»Õƒ–¿ = " + hCilindr);
    //    Debug.Log("¬€—Œ“¿  ¿œ—”À€ = " + hCapsule);

    //    Vector3 cubePos = new Vector3(0, 0, 0);
    //    Quaternion cubeRot = new Quaternion(0, 0, 0, 0);
    //    Transform cubeT = Instantiate(cube).transform;

    //    Vector3 cilindrPos = new Vector3(0, (hCube + hCilindr) / 2, 0);
    //    Quaternion cilindrRot = new Quaternion(0, 0, 0, 0);
    //    Transform cilindrT = Instantiate(cilindr, cubeT).transform;

    //    cilindrT.transform.Translate(0,(cilindrT.GetComponent<CapsuleCollider>().bounds.size.y + cubeT.GetComponent<BoxCollider>().bounds.size.y)/2,0);

    //    Vector3 capsulePos = new Vector3((cilindr.GetComponent<CapsuleCollider>().bounds.size.x + capsule.GetComponent<CapsuleCollider>().bounds.size.x) / 2, hCilindr / 2, 0);
    //    Quaternion capsuleRot = new Quaternion(90, 0, 0, 0);
    //    Transform capsuleT = Instantiate(capsule, cilindrT).transform;


    //    capsuleT.transform.Translate((cilindrT.GetComponent<CapsuleCollider>().bounds.size.x + capsuleT.GetComponent<CapsuleCollider>().bounds.size.x) / 2, 0, 0);
    //}


}
