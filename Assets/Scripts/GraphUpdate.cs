using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class GraphUpdate : MonoBehaviour
{
    public void OnEnable()
    {
        AstarPath.active.UpdateGraphs (new GraphUpdateObject(GetComponent<Collider>().bounds)); 
    }
}
