using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodjemObj : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Вошел " + collision.transform.name);

        if (collision.transform.name == "Щуп" || collision.transform.name == "Л хват" || collision.transform.name == "П хват")
        {
            this.transform.parent = collision.transform;
            this.transform.localPosition = new Vector3(0,0,0);
            this.transform.GetComponent<Rigidbody>().isKinematic = true;
            //this.transform.GetComponent<>
        }
    }
}
