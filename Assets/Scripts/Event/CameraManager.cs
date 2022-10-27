using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target_obj;
    public float speed;
    private Vector3 targetPosition;

    public bool isCamera = false;

    private void Awake()
    {
        this.transform.position = new Vector3(0, 0, -1);
        isCamera = false; 

    private bool isCamera = false;
    private ChaseScene chaseScene;
    private void Start()
    {
        chaseScene = FindObjectOfType<ChaseScene>();
    }

    void Update()
    {
        if (isCamera)
        {
            if (target_obj.gameObject != null && !chaseScene.isChase)
            {
                targetPosition.Set(target_obj.transform.position.x, target_obj.transform.position.y, this.transform.position.z);


                this.transform.position = targetPosition;
            }
        }
    }

    public void Bool()
    {
        isCamera = true;
    }

    public void Transform(Transform _transform)
    {
        this.gameObject.transform.position = new Vector3(_transform.position.x, _transform.position.y, this.transform.position.z);
    }
}
