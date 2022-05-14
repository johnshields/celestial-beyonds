using UnityEngine;

namespace Main.Scripts.UI
{
    public class Compass : MonoBehaviour
    {
        private Transform _target;
        private Vector3 _vector;

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Target").transform;
        }

        private void Update()
        {
            _vector.z = _target.eulerAngles.y;
            transform.localEulerAngles = _vector;
        }
    }
}
