using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Actions
{
    public interface IPlaceAction
    {
        public void Place(Vector3 actionLocation);
    }
}