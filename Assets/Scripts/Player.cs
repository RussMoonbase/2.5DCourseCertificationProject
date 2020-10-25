﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   [Header("Movement")]
   [SerializeField] private float _moveSpeed;
   private Vector3 _moveDirection;
   private Vector3 _moveVelocity;
   private CharacterController _controller;
   private float _horizontalInput;
   private float _yVelocity;

   [Header("Jumping")]
   [SerializeField] private float _jumpPower;
   [SerializeField] private float _gravity;


   // Start is called before the first frame update
   void Start()
   {
      _controller = GetComponent<CharacterController>();
   }

   // Update is called once per frame
   void Update()
   {
      _horizontalInput = Input.GetAxis("Horizontal");

      Movement();
   }

   private void Movement()
   {
      if (_controller.isGrounded)
      {
         //Debug.Log("Grounded");
         _moveDirection = new Vector3(_horizontalInput, 0f, 0f);
         _moveVelocity = _moveDirection * _moveSpeed;

         if (Input.GetKeyDown(KeyCode.Space))
         {
            _yVelocity = _jumpPower;
         }
      }
      else
      {
         _yVelocity -= _gravity;
      }

      _moveVelocity.y = _yVelocity;
      _controller.Move(_moveVelocity * Time.deltaTime);
   }
}