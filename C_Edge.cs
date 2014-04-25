using System;
namespace CSharpShowDijkstraWorking {
    public class Edge : IComparable<Edge> {
        public int Cost;
        private int _edgeID;
        private static int _edgeIDCount = -1;
        private Vector2D _pointA, _pointB;
        public int EdgeID {
            get { return _edgeID; }
        }
        public Vector2D PointA {
            get { return _pointA; }
        }
        public Vector2D PointB {
            get { return _pointB; }
        }
        public Edge(Vector2D pointA, Vector2D pointB, int cost) {
            Cost = cost;
            _pointA = pointA;
            _pointB = pointB;
            _edgeID = ++_edgeIDCount;
        }
        public Vector2D getOtherVector(Vector2D baseVector) {
            if (baseVector.VectorID == _pointA.VectorID) {
                return _pointB;
            }
            else if (baseVector.VectorID == _pointB.VectorID) {
                return _pointA;
            }
            else {
                return null;   //if you manage to have a vector which isn't A or B
            }
        }
        public override string ToString() {
            return "Edge ID: " + _edgeID + " - Connected to vectors " + _pointA.VectorID + " and " + _pointB.VectorID + " at a cost of " + Cost;
        }
        public int CompareTo(Edge otherEdge) {
            return this.Cost.CompareTo(otherEdge.Cost);
        }
    }
}
