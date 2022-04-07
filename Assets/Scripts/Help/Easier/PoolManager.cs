using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    Dictionary<GameObject, List<GameObject>> pools = new Dictionary<GameObject, List<GameObject>>();

    public GameObject Init(GameObject obj, float hidetime = 0)
    {
        if (obj != null)
        {
            if (pools.ContainsKey(obj))
            {
                if(pools[obj].FindAll((GameObject x) => !x.activeSelf).Count > 0)
                {
                    GameObject copy = pools[obj].Find((GameObject x) => !x.activeSelf);
                    copy.SetActive(true);
                    if(hidetime > 0)
                    {
                        AudoDestruct destruct = copy.AddComponent<AudoDestruct>();
                        destruct.active = true;
                        destruct.duration = hidetime;
                    }
                    return copy;
                }
                else
                {
                    GameObject copy = GameObject.Instantiate<GameObject>(obj);
                    pools[obj].Add(copy);
                    copy.SetActive(true);
                    if (hidetime > 0)
                    {
                        AudoDestruct destruct = copy.AddComponent<AudoDestruct>();
                        destruct.active = true;
                        destruct.duration = hidetime;
                    }
                    return copy;
                }
            }
            else
            {
                pools.Add(obj, new List<GameObject>());
                GameObject copy = GameObject.Instantiate<GameObject>(obj);
                pools[obj].Add(copy);
                copy.SetActive(true);
                if (hidetime > 0)
                {
                    AudoDestruct destruct = copy.AddComponent<AudoDestruct>();
                    destruct.active = true;
                    destruct.duration = hidetime;
                }
                return copy;
            }
        }
        else
        {
            return null;
        }
    }
}
