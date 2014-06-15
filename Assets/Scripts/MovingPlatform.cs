using UnityEngine;

namespace Assets.Scripts
{
    public class MovingPlatform : MonoBehaviour
    {
        public Vector3 StartPosition;
        public Vector3 EndPosition;

        public float Speed = 5;

        private bool _moveTowards = true;

        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _moveTowards ? EndPosition : StartPosition, Speed * Time.deltaTime);

            if (transform.position == EndPosition) _moveTowards = false;
            if (transform.position == StartPosition) _moveTowards = true;
        }
    }
}