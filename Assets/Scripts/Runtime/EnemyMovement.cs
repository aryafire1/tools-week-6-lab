using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        var sampler = new PoissonDiscSampler(12,12,12);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
