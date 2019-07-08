using UnityEngine;

public static class Spline
{
    /// <summary>
    /// Input a value between 0 and 1, and receive the position on the spline
    /// </summary>
    /// <param name="spline"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Vector3 SplineLerp(Vector3[] spline, float t)
    {
        float floatIndex = spline.Length * t;
        int index = (int)floatIndex;
        float tt = floatIndex - index;

        if (index + 1 > spline.Length - 1)
            return spline[spline.Length - 1];

        return Vector3.Lerp(spline[index], spline[index+1], tt);
    }

    #region Generic
    /// <summary>
    /// Input generic objects array with a function to extract its positions, then return an array of Vector3 containings the spline.
    /// Ex : Vector3[] spline = GetFullSpline(rigidbodies, 10, rigidbody => rigidbody.position);
    /// The ouput length will be resolutionPerSegment * (inputPositions.Length - 1);
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="inputPositions"></param>
    /// <param name="resolutionPerSegment"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static Vector3[] GetFullSpline<T>(T[] input, int resolutionPerSegment, System.Func<T, Vector3> func)
    {
        int length = resolutionPerSegment * (input.Length - 1);
        Vector3[] positions = new Vector3[length];
        for (int i = 0; i < input.Length - 1; i++)
        {
            int index = i * resolutionPerSegment;
            SetSpline(ref positions, i, index, input, resolutionPerSegment, func);
        }
        return positions;
    }

    static void SetSpline<T>(ref Vector3[] positions, int index, int placementIndex, T[] points, int resolutionPerSegment, System.Func<T, Vector3> func)
    {
        float increment = 1 / (float)resolutionPerSegment;

        for (int i = 0; i < resolutionPerSegment; i++)
        {
            float t = i * increment;
            Vector3[] pos = Get4PointPositions(index, t, points, func);
            positions[placementIndex + i] = GetSplinePosition(index, t, pos);
        }
    }

    static Vector3[] Get4PointPositions<T>(int index, float t, T[] points, System.Func<T, Vector3> func)
    {
        Vector3[] pos = new Vector3[4];
        pos[0] = (index - 1 >= 0) ? func(points[index - 1]) : func(points[0]) - (func(points[1]) - func(points[0])).normalized * 0.1f;
        pos[1] = func(points[index]);
        pos[2] = func(points[index + 1]);
        pos[3] = (index + 2 < points.Length) ? func(points[index + 2]) : func(points[index + 1]) - (func(points[index + 1]) - func(points[index])).normalized * 0.1f;
        return pos;
    }

    #endregion
    #region Vector3
    /// <summary>
    /// Input a vector3 array, then return an array of Vector3 containings the spline.
    /// Ex : Vector3[] spline = GetFullSpline(positions, 10);
    /// The ouput length will be resolutionPerSegment * (inputPositions.Length - 1);
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="inputPositions"></param>
    /// <param name="resolutionPerSegment"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static Vector3[] GetFullSpline(Vector3[] inputPositions, int resolutionPerSegment)
    {
        int length = resolutionPerSegment * (inputPositions.Length - 1);
        Vector3[] positions = new Vector3[length];
        for (int i = 0; i < inputPositions.Length-1; i++)
        {
            int index = i * resolutionPerSegment;
            SetSpline(ref positions, i, index, inputPositions, resolutionPerSegment);
        }
        return positions;
    }

    static void SetSpline(ref Vector3[] positions, int index, int placementIndex, Vector3[] points, int resolutionPerSegment)
    {
        float increment = 1 / (float)resolutionPerSegment;

        for (int i = 0; i < resolutionPerSegment; i++)
        {
            float t = i * increment;
            Vector3[] pos = Get4PointPositions(index, t, points);
            positions[placementIndex + i] = GetSplinePosition(index, t, pos);
        }
    }

    static Vector3[] Get4PointPositions(int index, float t, Vector3[] points)
    {
        Vector3[] pos = new Vector3[4];
        pos[0] = (index - 1 >= 0) ? points[index - 1] : points[0] - (points[1] - points[0]).normalized * 0.1f;
        pos[1] = points[index];
        pos[2] = points[index + 1];
        pos[3] = (index + 2 < points.Length) ? points[index + 2] : points[index + 1] - (points[index + 1] - points[index]).normalized * 0.1f;
        return pos;
    }
    #endregion

    static Vector3 GetSplinePosition(int index, float t, Vector3[] pos)
    {
        float tt = t * t;
        float ttt = t * t * t;

        float c0 = -ttt + 2 * tt - t;
        float c1 = 3 * ttt - 5 * tt + 2;
        float c2 = -3 * ttt + 4 * tt + t;
        float c3 = ttt - tt;

        return 0.5f * (c0 * pos[0] + c1 * pos[1] + c2 * pos[2] + c3 * pos[3]); 
    }
}
