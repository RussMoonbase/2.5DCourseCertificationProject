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

   [Header("Rotation")]
   [SerializeField] private float _flipSpeed;

   [Header("Jumping")]
   [SerializeField] private float _jumpPower;
   [SerializeField] private float _gravity;

   [Header("Ledge Grab")]
   [SerializeField] private float _snapHandsSpeed;

   private Animator _anim;

   // Start is called before the first frame update
   void Start()
   {
      _controller = GetComponent<CharacterController>();
      _anim = GetComponentInChildren<Animator>();
   }

   // Update is called once per frame
   void Update()
   {
      _horizontalInput = Input.GetAxisRaw("Horizontal");

      if (_controller.enabled)
      {
         Movement();
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


   public void LedgeGrab(Vector3 handsTarget)
   {
      _controller.enabled = false;
      _anim.SetBool("IsHanging", true);

      this.transform.position = handsTarget;

      //if (this.transform.position != handsTarget)
      //{
      //   this.transform.position = Vector3.Lerp(this.transform.position, handsTarget, _snapHandsSpeed * Time.deltaTime);
      //}
   }

   //private IEnumerator SnapHands(Vector3 moveHandsToTarget)
   //{

   //}
}
