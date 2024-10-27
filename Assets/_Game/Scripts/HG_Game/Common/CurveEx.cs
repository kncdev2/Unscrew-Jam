using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CurveEx
{
    //arrayToCurve is original Vector3 array, smoothness is the number of interpolations. 
    public static Vector3[] MakeSmoothCurve(this Vector3[] points, int smooth)
    {
        if (points.Length == 1) return points.Select(x => x).ToArray();
 
        List<Vector3> result = new List<Vector3>();
        
        result.Add(points[0]);
        for (int i = 1; i < points.Length; i++)
        {
            for (int j = 0; j < smooth; j++)
            {
                float t = (float)j / smooth;
                result.Add(Vector3.Lerp(points[i-1],points[i],t));
            }
        }
        return result.ToArray();
    }
    public static Vector3[] MakeSmoothCurve(this Vector3[] points, float ratio)
    {
        if (points.Length == 1) return points.Select(x => x).ToArray();
 
        List<Vector3> result = new List<Vector3>();
        
        result.Add(points[0]);
        for (int i = 1; i <= points.Length-2; i++)
        {
        
            var currentPoint = points[i];
            var previousPoint = points[i-1];
            var nextPoint = points[i+1];
            var t1 = (currentPoint - previousPoint);
            var t2 = (nextPoint - currentPoint);
// this is the average tangent.
            var avgTangent = Vector3.Lerp(t1, t2, ratio);
            result.Add(currentPoint - avgTangent);
            result.Add(currentPoint + avgTangent);
            
        }
        result.Add(points[points.Length-1]);

        return result.ToArray();
    }
 
    private static Vector3[] PathControlPointGenerator(Vector3[] path)
    {
        Vector3[] suppliedPath;
        Vector3[] vector3s;
 
        //create and store path points:
        suppliedPath = path;
 
        //populate calculate path;
        int offset = 2;
        vector3s = new Vector3[suppliedPath.Length + offset];
        Array.Copy(suppliedPath, 0, vector3s, 1, suppliedPath.Length);
 
        //populate start and end control points:
        //vector3s[0] = vector3s[1] - vector3s[2];
        vector3s[0] = vector3s[1] + (vector3s[1] - vector3s[2]);
        vector3s[vector3s.Length - 1] = vector3s[vector3s.Length - 2] + (vector3s[vector3s.Length - 2] - vector3s[vector3s.Length - 3]);
 
        //is this a closed, continuous loop? yes? well then so let's make a continuous Catmull-Rom spline!
        if (vector3s[1] == vector3s[vector3s.Length - 2])
        {
            Vector3[] tmpLoopSpline = new Vector3[vector3s.Length];
            Array.Copy(vector3s, tmpLoopSpline, vector3s.Length);
            tmpLoopSpline[0] = tmpLoopSpline[tmpLoopSpline.Length - 3];
            tmpLoopSpline[tmpLoopSpline.Length - 1] = tmpLoopSpline[2];
            vector3s = new Vector3[tmpLoopSpline.Length];
            Array.Copy(tmpLoopSpline, vector3s, tmpLoopSpline.Length);
        }
 
        return (vector3s);
    }
 
    private static Vector3 Interp(Vector3[] pts, float t)
    {
        int numSections = pts.Length - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * numSections), numSections - 1);
        float u = t * numSections - currPt;
 
        Vector3 a = pts[currPt];
        Vector3 b = pts[currPt + 1];
        Vector3 c = pts[currPt + 2];
        Vector3 d = pts[currPt + 3];
 
        return .5f * (
            (-a + 3f * b - 3f * c + d) * (u * u * u)
            + (2f * a - 5f * b + 4f * c - d) * (u * u)
            + (-a + c) * u
            + 2f * b
        );
    }
}