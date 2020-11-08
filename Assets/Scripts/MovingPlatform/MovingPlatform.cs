using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovingPlatform : MonoBehaviour
{
   [SerializeField] private Transform _targetA, _targetB;
   [SerializeField] private float _moveSpeed;
   [SerializeField] private bool _moveToTargetAOnStart;
   private Transform _targetPosition;
   [SerializeField] private CinemachineBrain _cmBrain;
   // Start is called before the first frame update
   void Start()
   {
      if (_moveToTargetAOnStart)
      {
         _targetPosition = _targetA;
      }
      else
      {
         _targetPosition = _targetB;
      }
      
   }

   // Update is called once per frame
   void FixedUpdate()
   {
      if (Vector3.Distance(this.transform.position, _targetA.position) < 0.01f)
      {
         _targetPosition = _targetB;
      }

      if (Vector3.Distance(this.transform.position, _targetB.position) < 0.01f)
      {
         _targetPosition = _targetA;
      }

      this.transform.position = Vector3.MoveTowards(this.transform.position, _targetPosition.position, _moveSpeed * Time.deltaTime);
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Player")
      {
         other.transform.parent = this.gameObject.transform;
         other.GetComponent<CameraChanger>().IncreasePlatformCamPriority();
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.tag == "Player")
      {
         other.GetComponent<CameraChanger>().DecreasePlatformCamPriority();
         other.transform.parent = null;
      }
   }

}
