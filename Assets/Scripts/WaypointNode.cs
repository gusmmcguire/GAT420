using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : Node
{
    public WaypointNode[] nextWaypoints;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<SearchAgent>(out SearchAgent agent))
        {
            if(agent.targetNode == this)
            {
                agent.targetNode = nextWaypoints[Random.Range(0, nextWaypoints.Length)];
            }
        }
    }

    public static WaypointNode GetRandomWaypoint()
    {
        var waypoints = GetNodes<WaypointNode>();

        return (waypoints == null) ? null : waypoints[Random.Range(0, waypoints.Length)];
    }

}
