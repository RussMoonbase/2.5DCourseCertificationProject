using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour
{
   [SerializeField] private CinemachineVirtualCameraBase _playerCamera;
   [SerializeField] private CinemachineVirtualCameraBase _ledgeCamera;
   [SerializeField] private CinemachineVirtualCameraBase _platformCamera;
   [SerializeField] private int _ledgeCamIncrease;
   [SerializeField] private int _platformCamIncrease;


   // if e pressed then increase ledge cam priority
   public void IncreaseLedgeCamPriority()
   {
      _ledgeCamera.Priority = _playerCamera.Priority + _ledgeCamIncrease;
   }

   // when climb is done set the ledge cam priority to be lower
   public void DecreaseLedgeCamPriority()
   {
      _ledgeCamera.Priority = _playerCamera.Priority - _ledgeCamIncrease;
   }

   public void IncreasePlatformCamPriority()
   {
      _platformCamera.Priority = _playerCamera.Priority + _platformCamIncrease;
   }

   public void DecreasePlatformCamPriority()
   {
      _platformCamera.Priority = _playerCamera.Priority - _platformCamIncrease;
   }

   public void SetLedgeCameraTarget(Transform target)
   {
      _ledgeCamera.Follow = target;
      _ledgeCamera.LookAt = target;
   }
}
