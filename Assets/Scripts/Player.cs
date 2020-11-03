using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField] private GameObject _model;

   [Header("Movement")]
   [SerializeField] private float _moveSpeed;
   private Vector3 _moveDirection;
   private Vector3 _moveVelocity;
   private CharacterController _controller;
   private float _horizontalInput;
   private float _yVelocity;
   //[SerializeField] private bool _ableToMove = true;

   [Header("Rotation")]
   [SerializeField] private float _flipSpeed;

   [Header("Jumping")]
   [SerializeField] private float _jumpPower;
   [SerializeField] private float _gravity;

   [Header("Ledge Grab")]
   //[SerializeField] private float _snapHandsSpeed;
   private bool _isHanging;
   private Vector3 _climbUpBodyPosition;

   private CameraChanger _camerachanger;
   private Animator _anim;

   // Start is called before the first frame update
   void Start()
   {
      _controller = GetComponent<CharacterController>();
      _anim = GetComponentInChildren<Animator>();
      _camerachanger = GetComponent<CameraChanger>();
   }

   // Update is called once per frame
   void FixedUpdate()
   {
      _horizontalInput = Input.GetAxisRaw("Horizontal");

      if (_controller.enabled)
      {
         Movement();
      }

      if (_isHanging)
      {
         if (Input.GetKeyDown(KeyCode.E))
         {
            _anim.SetTrigger("ClimbUp");
            if (_camerachanger)
            {
               _camerachanger.IncreaseLedgeCamPriority();
            }
         }
      }

   }

   private void Movement()
   {
      if (_controller.isGrounded)
      {
         //Debug.Log("Grounded");
         if (_anim.GetBool("IsJumping") == true)
         {
            _anim.SetBool("IsJumping", false);
         }

         if (_horizontalInput < 0)
         {
            if (_model.transform.eulerAngles.y != -90f)
            {
              // Debug.Log("Rotating to -90");
               _model.transform.rotation = Quaternion.Slerp(_model.transform.rotation, Quaternion.Euler(0f, -90f, 0f), _flipSpeed * Time.deltaTime);
            }
           
         }
         else if (_horizontalInput > 0)
         {
            if (_model.transform.eulerAngles.y != 90f)
            {
               //Debug.Log("Rotating to 90");
               _model.transform.rotation = Quaternion.Slerp(_model.transform.rotation, Quaternion.Euler(0f, 90f, 0f), _flipSpeed * Time.deltaTime);
            }         
         }

         _moveDirection = new Vector3(_horizontalInput, 0f, 0f);
         _anim.SetFloat("Speed", Mathf.Abs(_horizontalInput));
         _moveVelocity = _moveDirection * _moveSpeed;

         if (Input.GetKeyDown(KeyCode.Space))
         {
            _anim.SetBool("IsJumping", true);
            _yVelocity = _jumpPower;
         }
      }
      else
      {
         _yVelocity -= _gravity * Time.deltaTime;
      }

      _moveVelocity.y = _yVelocity;
      _controller.Move(_moveVelocity * Time.deltaTime);
   }


   public void LedgeGrab(Vector3 handsTarget, Vector3 bodyTarget)
   {
      _isHanging = true;
      _controller.enabled = false;
      //_ableToMove = false;
      _anim.SetBool("IsHanging", true);
      _anim.SetFloat("Speed", 0f);
      _anim.SetBool("IsJumping", false);
      this.transform.position = handsTarget;
      _climbUpBodyPosition = bodyTarget;
   }

   public void ReturnToIdleAfterClimbUp()
   {
      if (_climbUpBodyPosition != Vector3.zero)
      {
         this.transform.position = _climbUpBodyPosition;
      }
      
      //_controller.enabled = true;
      _anim.SetBool("IsHanging", false);
   }

   public void EnableMovement()
   {
      _controller.enabled = true;
      if (_camerachanger)
      {
         _camerachanger.DecreaseLedgeCamPriority();
      }
      //_ableToMove = true;
   }

}
