using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
   //[SerializeField] private bool _isGoingUp;
   //[SerializeField] private bool _isGoingDown;
   [SerializeField] private bool _canMove;
   [SerializeField] private Transform _downTarget, _upTarget;
   [SerializeField] private float _waitTime;
   private Transform _target;
   [SerializeField] private float _moveSpeed;

   // Start is called before the first frame update
   void Start()
   {
      _target = _downTarget;
   }

   // Update is called once per frame
   void Update()
   {
      if (_canMove)
      {
         if (Vector3.Distance(this.transform.position, _target.position) > 0.01)
         {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _target.position, _moveSpeed * Time.deltaTime);
         }
         else
         {
            _canMove = false;
         }
      }
   }

   public void SetElevatorToGoingDown()
   {
      _target = _downTarget;
      _canMove = true;
   }

   public void SetElevatorToGoingUp()
   {
      _target = _upTarget;
      _canMove = true;
   }

   private IEnumerator WaitAtPositionRoutine()
   {
      yield return new WaitForSeconds(_waitTime);
      _canMove = true;
   }
}
