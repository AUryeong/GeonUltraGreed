using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActiveOnEnd : MonoBehaviour
{
    void Disable()
    {
        gameObject.SetActive(false);
    }
}
