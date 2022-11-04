using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearByDis : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform obj;
    private SpriteRenderer sprite;
    private float distance;
    private float detectDis = 3;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(obj.position, player.position);

        if (distance <= detectDis)
        {
                sprite.color = new Color(0, 0, 0, 1- (distance / detectDis));
        }
    }
}
