using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour{

    private float lengthX, startPosX;
    public GameObject cam;
    [SerializeField] float parallaxEffect;
    void Start(){
        startPosX = transform.position.x;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update(){
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPosX + dist, cam.transform.position.y, transform.position.z);

        if(temp > (startPosX + lengthX)){
            startPosX += lengthX;
        }
        else if(temp < (startPosX - lengthX)){
            startPosX -= lengthX;
        }
        
    }
}
