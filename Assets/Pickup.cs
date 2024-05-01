using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public IntDaa collectableCount;
    public IntDaa playerHealth;
    public void pickedUp(string tag)
    {
        if(tag == "col")
        {
            collectableCount.daa++;
        }

        if(tag == "hec")
        {
            playerHealth.daa++;
        }

        Destroy(this.gameObject);
    }
}
