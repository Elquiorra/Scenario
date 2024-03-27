using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBound : MonoBehaviour
{
    private Camera mainCamera;
    private Bounds cameraBounds;
    private Vector3 targetPosition;
    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start(){
        var height = mainCamera.orthographicSize;
        var width = height + mainCamera.aspect;

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;

        cameraBounds = new Bounds();
        cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0f),
            new Vector3(maxX, maxY, 0f)
        );
    }
    
    void Update()
    {
        targetPosition = GetCameraBounds();
        targetPosition = transform.position;
    }

    private Vector3 GetCameraBounds(){
        return new Vector3(
            Mathf.Clamp(targetPosition.x, cameraBounds.min.x, cameraBounds.max.x),
            Mathf.Clamp(targetPosition.y, cameraBounds.min.y, cameraBounds.max.y),
            transform.position.z
        );
            
    }
}
