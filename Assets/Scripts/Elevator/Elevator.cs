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
   [SerializeField] private Transform _target;
   [SerializeField] private float _moveSpeed;
   private Rigidbody _rBody;

   // Start is called before the first frame update
   void Start()
   {
      //_target = _downTarget;
      _rBody = GetComponent<Rigidbody>();
   }

   // Update is called once per frame
   void FixedUpdate()
   {
      if (_canMove)
      {
         Debug.Log("Distance = " + Vector3.Distance(this.transform.position, _target.position));
         if (Vector3.Distance(this.transform.position, _target.position) > 0.1)
         {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _target.position, _moveSpeed * Time.deltaTime);
         }
         else
         {
            _canMove = false;

            if (_target == _downTarget)
            {
               _target = _upTarget;
            }

            if (_target == _upTarget)
            {
               _target = _downTarget;
            }

            StartCoroutine(WaitAtPositionRoutine());
         }
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Player")
      {
         Debug.Log("Player entered");
         other.transform.parent = this.gameObject.transform;
         other.gameObject.GetComponent<CameraChanger>().IncreasePlatformCamPriority();
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.tag == "Player")
      {
         other.transform.parent = null;
         other.gameObject.GetComponent<CameraChanger>().DecreasePlatformCamPriority();
      }
   }

   public void SetElevatorToGoingDown()
   {
      _target = _downTarget;
   }

   public void SetElevatorToGoingUp()
   {
      _target = _upTarget;     
   }

   public void ElevatorCanMove()
   {
      _canMove = true;
   }

   private IEnumerator WaitAtPositionRoutine()
   {
      yield return new WaitForSeconds(_waitTime);
      Debug.Log("Called");
      _canMove = true;
   }
}
