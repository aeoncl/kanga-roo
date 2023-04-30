using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform followTransform;
    public Transform eggTransform;
    public float eggUpperLimitFollow = 4;
    public float parallaxSpeed = 1f;

    private float initialY;
    private SpriteRenderer _backGroundSprite;
    private float _lastKnownXPosition;

    void Start()
    {
       this.initialY = this.transform.position.y;
       this._backGroundSprite = transform.GetComponentInChildren<SpriteRenderer>();
       this._lastKnownXPosition = followTransform.position.x;
    }

    void FixedUpdate()
    {
        if(eggTransform.position.y >= (this.initialY + this.eggUpperLimitFollow)) {
            var test = Vector3.Lerp(this.transform.position, eggTransform.position - new Vector3(0,3.5f,0), Time.fixedDeltaTime * 3);
            this.transform.position = new Vector3(followTransform.position.x, test.y, this.transform.position.z);
        } else {
            var initialYLerp = Mathf.Lerp(this.transform.position.y, this.initialY, Time.fixedDeltaTime * 2);
            this.transform.position = new Vector3(followTransform.position.x, initialYLerp, this.transform.position.z);
        }

        _backGroundSprite.material.mainTextureOffset += new Vector2(followTransform.position.x - _lastKnownXPosition, 0f) / 500;
        _lastKnownXPosition = followTransform.position.x;
    }
}
