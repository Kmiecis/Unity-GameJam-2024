using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider))]
    public class GroundedHandler : MonoBehaviour
    {
        [SerializeField, Tooltip("Transform below which ground will be found")]
        protected Transform _feetTransform;

        private List<GameObject> _grounds = new List<GameObject>();
        private bool _grounded = false;
        private List<Vector3> _normals = new List<Vector3>();
        private Vector3 _normal = Vector3.up;

        public event Action<bool> OnGroundedChange;

        public bool IsGrounded
        {
            get => _grounded;
            private set
            {
                if (_grounded != value)
                {
                    OnGroundedChange?.Invoke(value);
                    _grounded = value;
                }
            }
        }

        public Vector3 GroundNormal
        {
            get => _normal.normalized;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var position = _feetTransform.position;
            int contactCount = collision.contactCount;
            for (int i = 0; i < contactCount; ++i)
            {
                var contact = collision.GetContact(i);

                if (contact.point.y < position.y)
                {
                    _grounds.Add(collision.gameObject);

                    _normals.Add(contact.normal);
                    _normal += contact.normal;

                    IsGrounded = _grounds.Count > 0;

                    break;
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            var index = _grounds.IndexOf(collision.gameObject);
            if (index > -1)
            {
                _grounds.RemoveAt(index);

                var normal = _normals[index];
                _normals.RemoveAt(index);
                _normal -= normal;

                IsGrounded = _grounds.Count > 0;
            }
        }
    }
}
