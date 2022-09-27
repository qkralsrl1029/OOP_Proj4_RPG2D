using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneScript : MonoBehaviour
{

    private void Start()
    {
        AudioManager.instance.PlayBGM("End");
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime*50);
    }
}
