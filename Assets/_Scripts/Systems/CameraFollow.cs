using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class Follow_player : MonoBehaviour
{

    public Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(191, 150, -281);
    }
}

