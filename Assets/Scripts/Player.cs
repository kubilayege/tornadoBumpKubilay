using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player main;
    public float speed=5f;
    public GameObject TornadoPrefab;
    GameObject tornado;
    Quaternion turnMiddleRamp;
    Quaternion turnEndRamp;
    Quaternion startRot;
    Vector3 startPos;
    public Vector3 startForward;
    Coroutine currentAnim;
    Coroutine currentAlign;
    void Awake()
    {
        main = this;
        startPos = transform.position;
        startRot = transform.rotation;
        //startForward = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsIdle() && Input.GetMouseButtonDown(0) && GameManager.IsNotOnUI())
        {
            GameManager.main.ChangeGameState(GameStates.Playing);
            InitTornado();
        }
        
        if(GameManager.IsPlaying())
        {
            transform.localPosition += transform.forward * speed * Time.deltaTime;
            if (Input.GetMouseButtonUp(0))
                tornado.transform.position = Vector3.zero;
            if(Input.GetMouseButton(0))
            {
                TornadoControl();
            }
        }
        if (GameManager.IsWin() && Input.GetMouseButtonDown(0))
        {
            LevelManager.main.PassLevel(); 
            GameManager.main.ChangeGameState(GameStates.Idle);
        }
        if (GameManager.IsFail() && Input.GetMouseButtonDown(0))
        {
            LevelManager.main.RestartLevel();
            GameManager.main.ChangeGameState(GameStates.Idle);
        }
    }

    private void TornadoControl()
    {
        if (tornado == null)
            InitTornado();
        else
            tornado.transform.position = GetTornadoPos();
    }

    private void InitTornado()
    {
        if (tornado != null)
            Destroy(tornado);

        Vector3 pos = GetTornadoPos();
        if(pos.magnitude != 0)
            tornado = Instantiate(TornadoPrefab, pos, TornadoPrefab.transform.rotation);
    }

    public Vector3 GetTornadoPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit info))
        {
            if (info.transform.CompareTag("Ramp")) {
                return info.point;
            }
        }
        return Vector3.zero;
    }

    IEnumerator TurnAndRotate(Quaternion rotation)
    {
        float turnAnimDuration = speed; 

        while (turnAnimDuration > 0f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed* Time.deltaTime);
            yield return new WaitForSeconds( Time.deltaTime);
            turnAnimDuration -=  Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("badCube"))
        {
            if (currentAnim != null)
                StopCoroutine(currentAnim);
            if (currentAlign != null)
                StopCoroutine(currentAlign);
            GameManager.main.ChangeGameState(GameStates.Fail);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            case "RampMiddle":

                if (currentAnim != null)
                    StopCoroutine(currentAnim);

                other.transform.parent.GetComponent<Ramp>().SpawnNextRamp();
                turnMiddleRamp = Quaternion.Euler(0, transform.eulerAngles.y - 90, 0);
                currentAnim = StartCoroutine(TurnAndRotate(turnMiddleRamp));
                break;
            case "RampEnd":

                if (currentAnim != null)
                    StopCoroutine(currentAnim);

                turnEndRamp = Quaternion.Euler(-26.7f, transform.eulerAngles.y-90, 0);
                currentAnim = StartCoroutine(TurnAndRotate(turnEndRamp));
                break;
            case "Finish":
                if (currentAnim != null)
                    StopCoroutine(currentAnim);
                if (currentAlign != null)
                    StopCoroutine(currentAlign);
                GameManager.main.ChangeGameState(GameStates.Win);

                break;
        }
    }

    IEnumerator AlignVertically(GameObject midRamp)
    {
        float turnAnimDuration = speed;
        while (turnAnimDuration > 0f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,midRamp.transform.position.y+0.75f,transform.position.z), speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
            turnAnimDuration -= Time.deltaTime;
        }
    }
    private void OnTriggerStay(Collider other)
    {


        switch (other.tag)
        {
            case "RampMiddle":

                if (currentAlign != null)
                    StopCoroutine(currentAlign);

                currentAlign = StartCoroutine(AlignVertically(other.gameObject));
                break;
            case "RampEnd":
                transform.position = new Vector3(transform.position.x,other.gameObject.transform.position.y + 1f, transform.position.z);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "RampMiddle":
                if (currentAlign != null)
                    StopCoroutine(currentAlign);

                break;
            case "RampEnd":
                transform.position = new Vector3(transform.position.x, other.gameObject.transform.position.y + 1f, transform.position.z);
                break;
            default:

                break;
        }
    }

    public void ResetTransform()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = startPos;
        transform.SetPositionAndRotation(startPos, Quaternion.Euler(-26.7f, 0, 0));        
        Debug.Log(transform.forward);
        Debug.Log(startForward);
    }
}
