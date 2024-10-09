using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Vector3> waypoints; 
    private List<float> segmentLengths; 

    private void Awake()
    {
        CalculateSegmentLengths();
    }

    public float GetParam(Vector3 position, float lastParam)
    {
        float closestParam = 0f;
        float minDistance = float.MaxValue;

        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Vector3 start = waypoints[i];
            Vector3 end = waypoints[i + 1];

            Vector3 projectedPoint = GetClosestPointOnSegment(position, start, end);

            float distance = Vector3.Distance(position, projectedPoint);

            if (distance < minDistance)
            {
                minDistance = distance;
                
                float segmentParam = segmentLengths[i] + Vector3.Distance(start, projectedPoint);
                closestParam = segmentParam;
            }
        }

        return closestParam;
    }

    public Vector3 GetPosition(float param)
    {
        param = Mathf.Clamp(param, 0, segmentLengths[segmentLengths.Count - 1]);

        for (int i = 0; i < segmentLengths.Count - 1; i++)
        {
            if (param >= segmentLengths[i] && param < segmentLengths[i + 1])
            {
                float segmentLength = segmentLengths[i + 1] - segmentLengths[i];
                float segmentParam = (param - segmentLengths[i]) / segmentLength;

                return Vector3.Lerp(waypoints[i], waypoints[i + 1], segmentParam);
            }
        }

        return waypoints[waypoints.Count - 1];
    }

    private void CalculateSegmentLengths()
    {
        segmentLengths = new List<float> { 0 }; 
        float totalLength = 0;

        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            totalLength += Vector3.Distance(waypoints[i], waypoints[i + 1]);
            segmentLengths.Add(totalLength);
        }
    }

    private Vector3 GetClosestPointOnSegment(Vector3 point, Vector3 start, Vector3 end)
    {
        Vector3 segment = end - start;

        float t = Vector3.Dot(point - start, segment) / Vector3.Dot(segment, segment);
        t = Mathf.Clamp01(t); 

        return start + t * segment;
    }
}
