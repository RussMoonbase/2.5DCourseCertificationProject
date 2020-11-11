using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LedgeGrabChecker : MonoBehaviour
{
   //[SerializeField] private Vector3 _snapHandsPosition;
   //[SerializeField] private Vector3 _snapBodyPosition;
   [SerializeField] private Transform _ledgeCameraTransform;
   [SerializeField] private Transform _snapHandsTransform;
   [SerializeField] private Transform _snapBodyTransform;

   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "LedgeGrabChecker")
      {
         Player player = other.GetComponentInParent<Player>();

         if (player)
         {
            //player.LedgeGrab(_snapHandsPosition, _snapBodyPosition, _ledgeCameraTransform);
            player.LedgeGrab(_snapHandsTransform.position, _snapBodyTransform.position, _ledgeCameraTransform);
         }
      }
   }
}
