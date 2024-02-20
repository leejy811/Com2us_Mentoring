using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    Vector3 mousePos;
    Camera main;

    void Start()
    {
        main = Camera.main;
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = main.ScreenToWorldPoint(mousePos);

        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }
}
