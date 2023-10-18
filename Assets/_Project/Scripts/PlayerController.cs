using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using KBCore.Refs;
using UnityEngine.InputSystem;

namespace Platformer
{
    public class PlayerController : ValidatedMonoBehaviour
    {
        [Header("References")]
        [SerializeField, Self] Animator animator;
        [SerializeField, Self] GroundChecker groundChecker;
        [SerializeField, Anywhere] CinemachineFreeLook freeLookVCam;
        [SerializeField] InputReader input; // Modifiez le type ici
        [SerializeField, Self] Rigidbody rb;

        [Header("Movement Settings")]
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] float rotationSpeed = 15f;
        [SerializeField] float smoothTime = 0.2f;
        [SerializeField] float jumpForce = 5f;

        const float ZeroF = 0f;

        Transform mainCam;

        float currentSpeed;
        float velocity;
        Vector3 movement;

        static readonly int Speed = Animator.StringToHash("Speed");

        void Awake()
        {
            mainCam = Camera.main.transform;
            freeLookVCam.Follow = transform;
            freeLookVCam.LookAt = transform;
            freeLookVCam.OnTargetObjectWarped(transform, transform.position - freeLookVCam.transform.position - Vector3.forward);
            rb.freezeRotation = true;
        }

        void Start()
        {
            // Modifiez la méthode pour activer les événements du InputReader
            input.EnablePlayerActions();
            input.Jump += OnJump;
        }

        private void OnJump()
        {
            if (groundChecker.IsGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        void Update()
        {
            movement = new Vector3(input.Direction.x, 0f, input.Direction.y);
            UpdateAnimator();
        }

        void UpdateAnimator()
        {
            animator.SetFloat(Speed, currentSpeed);
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        public void HandleMovement()
        {
            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movement;

            if (adjustedDirection.magnitude > ZeroF)
            {
                HandleRotation(adjustedDirection);
                HandleHorizontalMovement(adjustedDirection);
                SmoothSpeed(adjustedDirection.magnitude);
            }
            else
            {
                SmoothSpeed(ZeroF);
                rb.velocity = new Vector3(ZeroF, rb.velocity.y, ZeroF);
            }
        }

        void HandleHorizontalMovement(Vector3 adjustedDirection)
        {
            Vector3 velocity = adjustedDirection * moveSpeed * Time.fixedDeltaTime;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }

        void HandleRotation(Vector3 adjustedDirection)
        {
            if (adjustedDirection != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(adjustedDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        void SmoothSpeed(float value)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
        }
    }
}
