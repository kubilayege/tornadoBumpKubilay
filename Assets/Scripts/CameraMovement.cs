using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    float offset;
    Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
            offset = (Player.main.transform.position - transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsPlaying() || GameManager.IsIdle())
        {
            rotation = Player.main.transform.forward;
            transform.forward = Vector3.Lerp(transform.forward, rotation, 15 * Time.deltaTime);
            transform.position = Player.main.transform.position - (offset * (rotation/5 - transform.up).normalized);

        }
    }
}
