using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] GameObject mainCharacter;
    Vector3 moveTrace;
    Vector3 offset = new Vector3(0, 1.5f, -10);
    // Start is called before the first frame update
    void Start()
    {
        moveTrace = mainCharacter.transform.position;
        moveTrace += offset;
    }

    // Update is called once per frame
    void Update()
    {
        moveTrace = mainCharacter.transform.position+offset;
        transform.position = Vector3.Lerp(transform.position, moveTrace, 2f * Time.deltaTime);
        //transform.Translate(0, 0, -10);
    }
}
