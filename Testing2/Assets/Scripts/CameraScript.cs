using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject monheco;

    // Update is called once per frame
    void Update()
    {
        if (monheco != null)
        {
            Vector3 position = transform.position;
            position.x = monheco.transform.position.x;
            transform.position = position;
        }
    }
}
