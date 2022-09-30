using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] private GameObject Player_obj;
    [SerializeField] private PlayerMove thePlayerMove;
    [SerializeField] private int checkPoint = 0;


    private void Update()
    {
        this.transform.position = Player_obj.transform.position;

        if(checkPoint == 0)
        {
            if (thePlayerMove.vector.x == 1)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (thePlayerMove.vector.x == -1)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (thePlayerMove.vector.y == 1)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (thePlayerMove.vector.y == -1)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

    }
}
