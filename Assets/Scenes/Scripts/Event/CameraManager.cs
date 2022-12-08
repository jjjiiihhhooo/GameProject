using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public GameObject target_obj;
    public float speed;
    private Vector3 targetPosition;
    public bool isCamera = false;
    public bool isChase = false;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (isCamera)
        {
            if (target_obj.gameObject != null && !isChase)
            {
                targetPosition.Set(target_obj.transform.position.x, target_obj.transform.position.y, -1.0f);


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