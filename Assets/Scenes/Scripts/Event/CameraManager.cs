using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class CameraManager : MonoBehaviour
{
    [SerializeField] public bool DontDestroy;
    public static CameraManager instance;

    public GameObject target_obj;
    public float speed;
    private Vector3 targetPosition;
    public bool isCamera = false;
    public bool isChase = false;
    public bool isMoving;



    private void Awake()
    {
        if (DontDestroy)
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
    public void TransObj(GameObject obj, float speed)
    {
        if (obj != null)
            StartCoroutine(TO(obj, speed));
    }

    IEnumerator TO(GameObject obj, float speed)
    {
        isCamera= false;
        isMoving= true;
        while (Vector2.Distance(transform.position, obj.transform.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(obj.transform.position.x, obj.transform.position.y, this.transform.position.z), speed * Time.deltaTime);
            yield return null;
        }
            StopCoroutine(TO(obj, speed));
        target_obj = obj;
        isMoving= false;
        isCamera= true;
    }
}