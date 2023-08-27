using System.Net;
using UnityEngine;
using UnityEngine.Splines;

namespace RailShooter {
    public static class FlightPathFactory {
        public static SplineContainer GenerateFlightPath(Vector3 startPoint, Vector3 endPoint) {
            /*Vector3[] pathPoints = new Vector3[spawn.Length];

            for (int i = 0; i < spawn.Length; i++) {
                pathPoints[i] = spawn[i].GetRandomPoint();
            }*/
            Vector3[] pathPoints = new Vector3[2];

            pathPoints[0] = startPoint;
            pathPoints[1] = endPoint;

            return CreateFlightPath(pathPoints);
        }

        static SplineContainer CreateFlightPath(Vector3[] pathPoints) {
            GameObject flightPath = new GameObject("Flight Path");
            
            var container = flightPath.AddComponent<SplineContainer>();
            var spline = container.AddSpline();
            var knots = new BezierKnot[pathPoints.Length];
            
            for (int i = 0; i < pathPoints.Length; i++) {
                knots[i] = new BezierKnot(
                    pathPoints[i], 
                    -30 * Vector3.forward, 
                    30 * Vector3.forward);
            }
                        
            spline.Knots = knots;
            
            return container;
        }
    }

}