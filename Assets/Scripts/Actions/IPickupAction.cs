using UnityEngine;

namespace Actions
{
    public interface IPickupAction
    {
        public void Pickup(Vector3 actionLocation, GameObject gObject);
    }
}