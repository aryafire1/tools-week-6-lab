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

    //me notes waiting with enums? is that the thing? the WaitForSeconds() function thing

    /* 
    void GenLoop() {
        if (avoidee doesnt see thing, prob bool?) {
            enum wait for seconds here
        }
        else {
            if (check place, no place) {
            enum wait and check again
            }
            else {
            move there to closest spot
            }
        }
    }

    void FindSpot() {
        make poisson disc here
        make collection to store spots
            foreach (point p in disc) {
            draw visual gizmo line
            }
            foreach (point p in poisson disc) {
                if (avoidee sees it) {
                    ignore point
                }
                else {
                    add point to collection
                }
            }
    }
    */
}
