using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 startPos;
    Quaternion startRot;
    // Start is called before the first frame update
    void Start()
    {
        //startRot = transform.rotation;        NOW ISN`T IN USE 
        startPos = transform.position;
    }
    public void Return()
    {
        transform.position = startPos;
       // transform.rotation = startRot;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0 && transform.position.y>=20f)
        {
            transform.position = transform.position - 0.03f * startPos;
        }
        else if (Input.mouseScrollDelta.y < 0 && transform.position.y <= 200f)
        {
            transform.position = transform.position + 0.03f * startPos;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            Vector3 nextPos = transform.position;
            nextPos.x += 0.1f;
            nextPos.z -= 0.1f;
            transform.position = nextPos;
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            Vector3 nextPos = transform.position;
            nextPos.x -= 0.1f;
            nextPos.z += 0.1f;
            transform.position = nextPos;
        }
        if (Input.GetAxis("Vertical")>0)
        {
            Vector3 nextPos=transform.position;
            nextPos.x += 0.1f;
            nextPos.z += 0.1f;
            transform.position = nextPos;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            Vector3 nextPos = transform.position;
            nextPos.x -= 0.1f;
            nextPos.z -= 0.1f;
            transform.position = nextPos;
        }
    }
}
