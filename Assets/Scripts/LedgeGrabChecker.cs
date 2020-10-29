using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LedgeGrabChecker : MonoBehaviour
{
   [SerializeField] private Vector3 _snapHandsPosition;

   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "LedgeGrabChecker")
      {
         Player player = other.GetComponentInParent<Player>();

         if (player)
         {
            player.LedgeGrab(_snapHandsPosition);
         }
      }
   }
}
