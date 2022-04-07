using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudoDestruct : MonoBehaviour
{
    public float duration;

    public bool active;
    void Update()
    {
        duration -= Time.deltaTime;
        if(duration < 0)
        {
            if (active)
            {
                gameObject.SetActive(false);
                Destroy(this);
            }
            else
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

    private void OnDisable()
    {
        if (active)
        {
            Destroy(this);
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
}
