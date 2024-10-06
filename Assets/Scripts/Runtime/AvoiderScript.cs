using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class AvoiderScript : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent navmesh;
    
    [Range(0,360)]
    public float angle;
    public GameObject toAvoid;
    //not sure we need this one?
    //public bool GizmosOn = true;
    public float radius;
    public bool inSight;
    
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    
    
    // Start is called before the first frame update
    void Start()
    {
        navmesh = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navmesh == null) {
            Debug.LogWarning("Object does not have a Nav Mesh Agent. Assign and bake a nav mesh.");
        }
        toAvoid = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Timer());

    }

    private IEnumerator Timer()
    {
        float interval = 0.2f;
        WaitForSeconds timer = new WaitForSeconds(interval);
        
        while (true)
        {
            yield return timer;
            Hunt();
        }
    }

    void Hunt()
    {
        Collider[] aura = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (aura.Length != 0)
        { 
            Transform target = aura[0].transform;
            Vector3 playerDirection = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, playerDirection) < angle / 2)
            {
                float avoidDistance = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, playerDirection, avoidDistance, obstructionMask))
                {
                    inSight = true;
                }
                else
                {
                    inSight = false;
                }
            }
            else
            {
                inSight = false;
            }
        }
        else if(inSight)
        {
            inSight = false;
        }
    }

    //me notes waiting with enums? is that the thing? the WaitForSeconds() function thing

    /* 
    //bool in pseudocode denotes if point is safe to move to

    void GenLoop() {
        if (avoidee doesnt see thing, prob bool?) {
            enum wait for seconds here
            bool false
        }
        else {
            if (check place, no place) {
            enum wait and check again
            bool false
            }
            else {
            move there to closest spot
            FindSpot()
            bool true
            }
        }
    }

    void FindSpot() {
        make poisson disc here
        make collection to store spots
            foreach (point p in disc) {
                if (gizmosOn == true) {
                draw visual gizmo line
                }
            }
            foreach (point p in poisson disc) {
            CheckVisibility()
                if (player sees it) {
                    ignore point
                }
                else {
                    add point to collection
                }
            }
    }

    void CheckVisibility() {
        make ray from poisson points
        if (ray hits wall) {
            point is visible
            bool false
        }
        else {
            point not visible
            bool true
        }
    }
    */
}
