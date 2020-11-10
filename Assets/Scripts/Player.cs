using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField] private GameObject _model;

   [Header("Ground Movement")]
   [SerializeField] private float _groundMoveSpeed;
   private Vector3 _moveDirection;
   private Vector3 _moveVelocity;
   private CharacterController _controller;
   private float _horizontalInput;
   private float _verticalInput;
   private float _yVelocity;
   //private float _xVelocity;
   //[SerializeField] private float _rollPower;
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

   [Header("Rolling")]
   private bool _isRolling;
   [SerializeField] private float _maxRollDistance;
   private Vector3 _rollEndPosition;
   private float _rollSpeed;

   [Header("Ladder Movement")]
   private bool _canClimbLadder;
   private bool _climbingLadder;
   private Vector3 _ladderSnapHandsPosition;
   [SerializeField] private float _ladderMoveSpeed;

   private CameraChanger _camerachanger;
   private Animator _anim;
   private PlayerAnimation _playerAnimation;

   // Start is called before the first frame update
   void Start()
   {
      _controller = GetComponent<CharacterController>();
      _anim = GetComponentInChildren<Animator>();
      _camerachanger = GetComponent<CameraChanger>();
      _playerAnimation = GetComponentInChildren<PlayerAnimation>();
   }

   // Update is called once per frame
   void Update()
   {
      _horizontalInput = Input.GetAxisRaw("Horizontal");
      _verticalInput = Input.GetAxisRaw("Vertical");

      if (_isRolling)
      {
         this.transform.position = Vector3.Lerp(this.transform.position, _rollEndPosition, _rollSpeed * Time.deltaTime);
      }

      if (_controller.enabled && !_climbingLadder)
      {
         GroundMovement();
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

      if (_canClimbLadder)
      {
         if (Input.GetKeyDown(KeyCode.E))
         {
            LadderGrab();
         }
      }

      if (_climbingLadder)
      {
         LadderMovement();
      }
   }

   private void GroundMovement()
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
         _moveVelocity = _moveDirection * _groundMoveSpeed;
  
         if (Input.GetKeyDown(KeyCode.Space))
         {
            _anim.SetBool("IsJumping", true);
            _yVelocity = _jumpPower;
         }

         if (Input.GetKeyDown(KeyCode.LeftShift))
         {
            if (_model.transform.eulerAngles.y >= 0 && _model.transform.eulerAngles.y < 230)
            {
               SetRollEndPosition(_maxRollDistance);
            }
            else if (_model.transform.eulerAngles.y > 230)
            {
               SetRollEndPosition(-_maxRollDistance);
            }
            Rolling();
         }
      }
      else
      {
         _yVelocity -= _gravity * Time.deltaTime;     
      }

      _moveVelocity.y = _yVelocity;
      _controller.Move(_moveVelocity * Time.deltaTime);
   }

   public void LadderClimbReady(Vector3 handsTarget)
   {
      _canClimbLadder = true;
      _ladderSnapHandsPosition = handsTarget;
   }

   private void LadderGrab()
   {
      _climbingLadder = true;
      _anim.SetBool("IsClimbingLadder", true);
      _controller.enabled = false;
      this.transform.position = _ladderSnapHandsPosition;    
   }

   private void LadderMovement()
   {

      if (!_controller.enabled)
      {
         _controller.enabled = true;
      }
      _moveDirection = new Vector3(0f, _verticalInput, 0f);
      _anim.SetFloat("LadderClimbSpeed", _verticalInput);
      _moveVelocity = _moveDirection * _ladderMoveSpeed;
      _controller.Move(_moveVelocity * Time.deltaTime);
   }

   public void LedgeGrab(Vector3 handsTarget, Vector3 bodyTarget, Transform ledgeCamTransform)
   {
      if (_camerachanger)
      {
         _camerachanger.SetLedgeCameraTarget(ledgeCamTransform);
      }

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

   public void EnableController()
   {
      _controller.enabled = true;

      //_ableToMove = true;
   }

   public void DecreaseLedgeCamPriority()
   {
      if (_camerachanger)
      {
         _camerachanger.DecreaseLedgeCamPriority();
      }
   }

   //public void SetXVelocity(float speed)
   //{
   //   _xVelocity = speed;
   //}

   private void Rolling()
   {
      //_controller.enabled = false;
      _isRolling = true;
      _anim.SetBool("IsRolling", _isRolling);
   }

   public void SetRollSpeed(float newSpeed)
   {
      _rollSpeed = newSpeed;
      Debug.Log("Roll Speed = " + _rollSpeed);
   }

   private void SetRollEndPosition(float distanceOffset)
   {
      _rollEndPosition = new Vector3(this.transform.position.x + distanceOffset, this.transform.position.y, this.transform.position.z);
   }

   public bool GetClimbingLadder()
   {
      return _climbingLadder;
   }

   public void GetOffLadder()
   {
      _anim.SetBool("IsClimbingLadder", false);
      _climbingLadder = false;
   }

   public void ReachedTopOfLadder()
   {
      _anim.SetBool("ReachedTopOfLadder", true);
   }

}
