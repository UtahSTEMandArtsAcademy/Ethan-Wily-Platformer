using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaceImDippingPlatform : MonoBehaviour
{
    public void youareMom(float wait)
    {
        StartCoroutine(Dissaper(wait));
    }
    private IEnumerator Dissaper(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
}
