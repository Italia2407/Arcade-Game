using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private HealthManager _healthManager;

    public float jumpHeight = 1.0f;
    public float jumpDistance = 1.0f;
    private float _jumpForce;
    private float _gravityForce;

    public float runSpeed = 1.0f;
    public float accelerationTime = 1.0f;
    public float decelerationTimeGround = 1.0f;
    public float decelerationTimeAir = 1.0f;
    private float _acceleration;
    private float _decelerationGround;
    private float _decelerationAir;

    private bool _canMove;

    private float _horizontalInput;
    private bool _jumpInput;

    public float detectionDistance = 0.01f;
    public float detectionSkinDepth = 0.001f;
    public LayerMask detectionLayers;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _healthManager = GetComponent<HealthManager>();

        _jumpForce = 4 * jumpHeight * runSpeed / jumpDistance;
        _gravityForce = -8 * jumpHeight * Mathf.Pow(runSpeed, 2) / Mathf.Pow(jumpDistance, 2);

        _acceleration = runSpeed / accelerationTime;
        _decelerationGround = runSpeed / decelerationTimeGround;
        _decelerationAir = runSpeed / decelerationTimeAir;

        _canMove = true;

        _horizontalInput = 0.0f;
        _jumpInput = false;

        _healthManager.Die.AddListener(DisableMovement);
    }

    private void FixedUpdate()
    {
        Vector2 velocity = _rigidbody.velocity;
        bool grounded = IsGrounded();

        // Handle Gravity
        if (!grounded)
        {
            velocity.y += _gravityForce * Time.fixedDeltaTime;
        }

        // Horizontal Movement
        if (_horizontalInput != 0.0f)
        {
            velocity.x += _horizontalInput * _acceleration * Time.deltaTime;
            velocity.x = Mathf.Clamp(velocity.x, -runSpeed, runSpeed);

            _horizontalInput = 0.0f;
        }
        else
        {
            int movementDirection = (int)Mathf.Sign(velocity.x);
            if (velocity.x == 0.0f)
                movementDirection = 0;

            float deceleration = grounded ? _decelerationGround : _decelerationAir;
            velocity.x += (-movementDirection * deceleration) * Time.fixedDeltaTime;

            if (movementDirection == 1 && velocity.x < 0.0f)
                velocity.x = 0.0f;
            else if (movementDirection == -1 && velocity.x > 0.0f)
                velocity.x = 0.0f;
        }

        Debug.Log("Fixed: " + _jumpInput);
        // Jumping
        if (_jumpInput && grounded)
        {
            Debug.Log("Has Jumped");
            velocity.y = _jumpForce;

            _jumpInput = false;
        }

        _rigidbody.velocity = velocity;
    }

    private void Update()
    {
        // Input System
        {
            if (_canMove)
            {
                _horizontalInput = Input.GetAxisRaw("Horizontal");
                _jumpInput = Input.GetButtonDown("Jump");
                Debug.Log("Update: " + _jumpInput);
            }
            else
            {
                _horizontalInput = 0.0f;
                _jumpInput = false;
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    GameObject collidedObject = collision.gameObject;

    //    if (collidedObject.tag == "Terrain")
    //    {
    //        List<ContactPoint2D> contactPoints = new List<ContactPoint2D>();
    //        collision.GetContacts(contactPoints);
    //        foreach (ContactPoint2D contactPoint in contactPoints)
    //        {
    //            Vector2 contactNormal = contactPoint.normal;
    //            Vector2 contactPosition = contactPoint.point;

    //            if (contactNormal == Vector2.up)
    //            {
    //                _velocity.y = 0.0f;
    //                transform.position = new Vector2(transform.position.x, contactPosition.y + _collider.bounds.extents.y);
    //                Debug.Log("Pos: " + contactPosition.y + ", Shift: " + _collider.bounds.extents.y);
    //            }
    //            else if (contactNormal == Vector2.down)
    //            {
    //                _velocity.y = 0.0f;
    //                transform.position = new Vector2(transform.position.x, contactPosition.y - _collider.bounds.extents.y);
    //            }
    //            else if (contactNormal == Vector2.right)
    //            {
    //                _velocity.y = 0.0f;
    //                transform.position = new Vector2(contactPosition.x + _collider.bounds.extents.x, transform.position.y);
    //            }
    //            else if (contactNormal == Vector2.down)
    //            {
    //                _velocity.y = 0.0f;
    //                transform.position = new Vector2(contactPosition.x - _collider.bounds.extents.x, transform.position.y);
    //            }
    //        }
    //    }
    //}

    public bool IsGrounded()
    {
        Vector2 raycastPointL = new Vector2(-_collider.bounds.extents.x + detectionSkinDepth, -_collider.bounds.extents.y + detectionSkinDepth);
        Vector2 raycastPointM = new Vector2(0.0f, -_collider.bounds.extents.y + detectionSkinDepth);
        Vector2 raycastPointR = new Vector2(_collider.bounds.extents.x - detectionSkinDepth, -_collider.bounds.extents.y + detectionSkinDepth);

        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D raycastHitL = Physics2D.Raycast(position2D + raycastPointL, Vector2.down, detectionDistance, detectionLayers);
        RaycastHit2D raycastHitM = Physics2D.Raycast(position2D + raycastPointM, Vector2.down, detectionDistance, detectionLayers);
        RaycastHit2D raycastHitR = Physics2D.Raycast(position2D + raycastPointR, Vector2.down, detectionDistance, detectionLayers);

        bool hitL = raycastHitL.collider != null;
        bool hitM = raycastHitM.collider != null;
        bool hitR = raycastHitR.collider != null;

        return hitL || hitM || hitR;
    }

    private void DisableMovement()
    {
        _canMove = false;
    }
}
