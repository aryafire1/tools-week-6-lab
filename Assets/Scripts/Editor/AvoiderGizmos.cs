using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AvoiderScript))]
public class AvoiderGizmos : Editor
{
    private void OnSceneGUI()
    {
        AvoiderScript guide = (AvoiderScript)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(guide.transform.position, Vector3.up, Vector3.forward, 360, guide.radius);

        Vector3 sightangle1 = DirectionAngle(guide.transform.eulerAngles.y, -guide.angle / 2);
        Vector3 sightangle2 = DirectionAngle(guide.transform.eulerAngles.y, guide.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(guide.transform.position, guide.transform.position + sightangle1 * guide.radius);
        Handles.DrawLine(guide.transform.position, guide.transform.position + sightangle2 * guide.radius);

        if (guide.inSight)
        {
            Handles.color = Color.green;
            Handles.DrawLine(guide.transform.position, guide.toAvoid.transform.position);
            foreach (var failedpath in guide.invalidroutes)
            {
                Gizmos.color = Color.red;
                Debug.DrawLine(guide.transform.position, failedpath);
            }

            foreach (var path in guide.routes)
            {
                Gizmos.color = Color.blue;
                Debug.DrawLine(guide.transform.position, path);

            }
        }
    }

    private Vector3 DirectionAngle(float eulerY, float angle)
    {
        angle += eulerY;
        
        //Creates the triangle angle inside the circle gizmo
        //Takes the sin of the angle given, 0 which should be the center of the avoider, and then the cos which is the other line forming the triangle
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
