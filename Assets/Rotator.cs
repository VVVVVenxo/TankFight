using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime); //rotates the game object this script is attached to by 15, 30, and 45 degrees per second around the x, y, and z axes, respectively    
    }
}
