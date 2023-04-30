using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform followTransform;

    public Transform eggTransform;

    private float initialY;

    public float eggUpperLimitFollow = 4;

    // Start is called before the first frame update
    void Start()
    {
       this.initialY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(eggTransform.position.y >= (this.initialY + this.eggUpperLimitFollow)) {
        var test = Vector3.Lerp(this.transform.position, eggTransform.position - new Vector3(0,3.5f,0), Time.fixedDeltaTime * 2);
        this.transform.position = new Vector3(followTransform.position.x, test.y, this.transform.position.z);
        } else {
            var initialYLerp = Mathf.Lerp(this.transform.position.y, this.initialY, Time.fixedDeltaTime * 2);
            this.transform.position = new Vector3(followTransform.position.x, initialYLerp, this.transform.position.z);
        }
        
    }
}
