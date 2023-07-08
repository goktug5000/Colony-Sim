using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TileResource : MonoBehaviour
{
    [Header("Resurce List")]
    [SerializeField] private resourceWithChance[] Resurces;
    [Header("Die")]
    [SerializeField] [Range(0, 1000)] public int Die;
    // Start is called before the first frame update
    void Start()
    {
        float valuee = UnityEngine.Random.Range(0, Die+1);
        foreach(resourceWithChance Resurce in Resurces)
        {
            if(Resurce.ChanceRangeMin <= valuee && valuee <= Resurce.ChanceRangeMax)
            {
                var res = Instantiate(Resurce.ResurceObj);
                res.transform.parent = this.gameObject.transform;

                res.gameObject.name = Resurce.ResurceObj.name;
                res.transform.position = this.gameObject.transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class resourceWithChance
{
    [SerializeField] public GameObject ResurceObj;
    [SerializeField] [Range(0, 1000)] public int ChanceRangeMin, ChanceRangeMax;
}