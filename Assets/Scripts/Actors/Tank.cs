using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actors
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Tank : MonoBehaviour, IControllable
    {
        [SerializeField]
        private float speed;
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        [SerializeField]
        private Transform bodyTransform;
        public Transform BodyTransform
        {
            get { return bodyTransform; }
            set { bodyTransform = value; }
        }

        [SerializeField]
        private Transform barrelTransform;
        public Transform BarrelTransform
        {
            get { return barrelTransform; }
            set { barrelTransform = value; }
        }

        private Rigidbody2D rb2d;
        private Vector2 aimingDir;
        private Vector2 moveDir;
        private float forceFactor;

        private int currentWeapon;

        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            SetForceFactor(Speed);

        }

        private void SetForceFactor(float speed)
        {
            rb2d.drag = speed;
            forceFactor = speed * speed;
        }

        private void Update()
        {
            RotateBarrel();
            RotateBody();
        }

        private void FixedUpdate()
        {
            if (moveDir.sqrMagnitude > 0)
            {
                Vector2 moveForce = moveDir * forceFactor;
                rb2d.AddForce(moveForce, ForceMode2D.Force);
            }
        }

        private void RotateBarrel()
        {
            var barrelAngle = Mathf.Atan2(aimingDir.y, aimingDir.x) * Mathf.Rad2Deg - 90f;
            var barrelRotation = Quaternion.AngleAxis(barrelAngle, Vector3.forward);
            barrelTransform.localRotation = Quaternion.Slerp(barrelTransform.localRotation, barrelRotation, 16f * Time.deltaTime);
        }

        private void RotateBody()
        {
            if (rb2d.velocity.sqrMagnitude > 0)
            {
                var direction = rb2d.velocity.normalized;
                var bodyAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                var bodyRotation = Quaternion.AngleAxis(bodyAngle, Vector3.forward);
                bodyTransform.localRotation = Quaternion.Slerp(bodyTransform.localRotation, bodyRotation, 10f * Time.deltaTime);
            }
        }

        public void SetAimingPosition(Vector3 worldPos)
        {
            var relativeVec = worldPos - transform.position;
            relativeVec.z = 0;
            aimingDir = relativeVec.normalized;
        }

        public void SetMove(Vector2 moveDir)
        {
            this.moveDir = moveDir;
        }

        public void ActivateWeapon(int weaponIndex)
        {
            // TODO: Check if the weapon is ready to use.
            currentWeapon = weaponIndex;
        }

    }
}


