using UnityEngine;

namespace Main.Scripts.UI
{
    public class Compass : MonoBehaviour
    {
        public Transform target;
        private Vector3 _vector;

        private void Update()
        {
            _vector.z = target.eulerAngles.y;
            transform.localEulerAngles = _vector;
        }
    }
}
