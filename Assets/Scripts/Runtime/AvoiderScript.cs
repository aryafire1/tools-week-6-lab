using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public List<Vector3> routes = new List<Vector3> ();
    public List<Vector3> invalidroutes = new List<Vector3> ();
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
                    FindRoute();
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

    void FindRoute()
    {

        var sampler = new PoissonDiskSampler3D(10f, transform.position.y, 10f, radius);
        
        foreach (var sample in sampler.Samples())
        {
            //Debug.Log("Value Processed");
            var ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.tag == "Player")
                {
                    invalidroutes.Add(sample);
                }
                else
                {
                    routes.Add(sample);
                }
            }
        }
        
        
        
    }

    void Escape()
    {
        List<float> distances = new List<float>();
        Dictionary<Vector3, float> core = new Dictionary<Vector3, float>();
        Vector3 escapeRoute = new Vector3();
        if (routes.Count == 0)
        {
            Debug.Log("Yikes!!! Theres no where to run.");
        }
        else
        {
            foreach (var path in routes)
            {
                // couldn't find anonther way
                //core.Add(path, path.Distance(transform.position, path));
                
            }
            
            
        }

        navmesh.destination = escapeRoute;

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
