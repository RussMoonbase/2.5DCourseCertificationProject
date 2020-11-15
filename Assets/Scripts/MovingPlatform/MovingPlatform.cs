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
   [SerializeField] private MovingPlatformCameraTarget _camTarget;

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
         other.gameObject.GetComponent<CameraChanger>().DecreasePlayerCamDamping();
         //other.GetComponent<CameraChanger>().IncreasePlatformCamPriority();
         //_camTarget.SetCanFollowPlayerBool(true);
         //_camTarget.SetFollowTarget(other.transform);
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.tag == "Player")
      {
         //other.GetComponent<CameraChanger>().DecreasePlatformCamPriority();
         //_camTarget.SetCanFollowPlayerBool(false);
         other.gameObject.GetComponent<CameraChanger>().IncreasePlayerCamDamping();
         other.transform.parent = null;
      }
   }

}
