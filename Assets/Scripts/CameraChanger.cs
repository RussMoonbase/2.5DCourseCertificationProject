using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour
{
   [SerializeField] private CinemachineVirtualCameraBase _playerCamera;
   [SerializeField] private CinemachineVirtualCameraBase _ledgeCamera;

   // if e pressed then increase ledge cam priority
   public void IncreaseLedgeCamPriority()
   {
      _ledgeCamera.Priority = _playerCamera.Priority + 1;
   }

   // when climb is done set the ledge cam priority to be lower
   public void DecreaseLedgeCamPriority()
   {
      _ledgeCamera.Priority = _playerCamera.Priority - 1;
   }
}
