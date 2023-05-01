using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBoxLocator : MonoBehaviour
{

    public GameObject egg;

    private EggController eggController;

    private SpriteRenderer renderer;

    private Transform eggTransform;

    // Start is called before the first frame update
    void Start()
    {
       eggController = egg.GetComponent<EggController>();

       renderer = this.GetComponent<SpriteRenderer>();

       eggTransform = egg.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (eggController.IsVisible) {
            Debug.Log("HIDE BOULE");
            renderer.enabled = false;
        } else {
            Debug.Log("SHOW BOULE");
            var pos = this.transform.position;
            pos.x = eggTransform.position.x;
            this.transform.position = pos;
            renderer.enabled = true;
        }
    }
}
