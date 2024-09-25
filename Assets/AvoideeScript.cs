using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoideeScript : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent navmesh;

    public GameObject toAvoid;
    public float range;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        navmesh = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navmesh == null) {
            Debug.LogWarning("Object does not have a Nav Mesh Agent. Assign and bake a nav mesh.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
