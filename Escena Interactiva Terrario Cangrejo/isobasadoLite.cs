using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isobasadoLite : MonoBehaviour
{
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            Camera.main.transform.Translate(Vector3.up * (Time.deltaTime * 8f));    
        }
        if(Input.GetKey(KeyCode.S)){
            Camera.main.transform.Translate(Vector3.down * (Time.deltaTime * 8f));    
        }
        if(Input.GetKey(KeyCode.A)){
            Camera.main.transform.Translate(Vector3.left * (Time.deltaTime * 5f));    
        }
        if(Input.GetKey(KeyCode.D)){
            Camera.main.transform.Translate(Vector3.right * (Time.deltaTime * 5f));    
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //????
            
        }
    }
}
