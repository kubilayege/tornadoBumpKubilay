using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public float tornadoForce=2f;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("badCube"))
        {
            Vector3 forceDir = other.transform.position - (transform.position + new Vector3(0,0.5f,0));
            if(forceDir.magnitude == 0)
            {
                Debug.Log(forceDir);
            }
            forceDir = forceDir / (forceDir.magnitude* forceDir.magnitude);
            other.attachedRigidbody.useGravity = true;
            other.attachedRigidbody.isKinematic = false;
            other.attachedRigidbody.AddForce(forceDir*tornadoForce, ForceMode.Impulse);
        }
    }
}
