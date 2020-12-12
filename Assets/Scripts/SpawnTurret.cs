using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTurret : MonoBehaviour
{
    public GameObject[] nodes;
    public GameObject turret;
    private int index = 0;

    public void Spawn()
    {
        Instantiate(turret, nodes[index].transform.position, Quaternion.identity);
        index+=1;
    }
}
