using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class MovementBordersSetter : MonoBehaviour, IBorderChecker
    {
        [SerializeField] private float xMin;
        [SerializeField] private float xMax;
        [SerializeField] private float yMin;
        [SerializeField] private float yMax;
        
        public bool CheckBorders(Vector3 position)
        {
            if (position.x <= xMin)
                return false;
            if (position.x >= xMax)
                return false;
            if (position.y <= yMin)
                return false;
            if (position.y >= yMax)
                return false;
            return true;
        }


#if UNITY_EDITOR
        [SerializeField] private List<Transform> _cornerPoints;

        private void OnDrawGizmos()
        {
            if (_cornerPoints.Count < 4)
                return;
            var xMax = float.MinValue; 
            var yMax = float.MinValue;
            var xMin = float.MaxValue;
            var yMin = float.MaxValue;
            
            for (var i = 0; i < _cornerPoints.Count; i++)
            {
                var p = _cornerPoints[i].position;
                if (p.x > xMax)
                    xMax = p.x;
                if (p.x < xMin)
                    xMin = p.x;
                if (p.y > yMax)
                    yMax = p.y;
                if (p.y < yMin)
                    yMin = p.y;
            }
            Gizmos.color = Color.green;
            var c1 = new Vector3(xMin, yMin);
            var c2 = new Vector3(xMax, yMin);
            var c3 = new Vector3(xMax, yMax);
            var c4 = new Vector3(xMin, yMax);
            Gizmos.DrawLine(c1, c2);
            Gizmos.DrawLine(c2, c3);
            Gizmos.DrawLine(c3, c4);
            Gizmos.DrawLine(c4, c1);
            Gizmos.color = Color.white;
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
        }
#endif
    }
}