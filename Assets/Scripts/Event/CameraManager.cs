using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target_obj;
    public float speed;
    private Vector3 targetPosition;
    private bool isCamera = false;
    private ChaseScene chaseScene;
    private void Start()
    {
        chaseScene = FindObjectOfType<ChaseScene>();
    }

    void Update()
    {
        if(isCamera)
        {
            if (target_obj.gameObject != null)
            {
                targetPosition.Set(target_obj.transform.position.x, target_obj.transform.position.y, this.transform.position.z);

                if (chaseScene.isChase == true)
                    targetPosition.y += 2;
                
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
