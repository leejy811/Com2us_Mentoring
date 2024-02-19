using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isHaveTower { get; set; }

    private void Awake()
    {
        isHaveTower = false;
    }
}
