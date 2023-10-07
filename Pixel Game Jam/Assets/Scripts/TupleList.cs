using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TupleList : MonoBehaviour
{
    [System.Serializable]
    public class Vector3Tuple {
        public Vector3 v1;
        public Vector3 v2;

        public Vector3Tuple(Vector3 v1, Vector3 v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }

    public List<Vector3Tuple> vectorList;
}
