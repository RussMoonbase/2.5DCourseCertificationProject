using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrabChecker : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "LedgeGrabChecker")
      {
         Player player = other.GetComponentInParent<Player>();

         if (player)
         {
            player.LedgeGrab();
         }
      }
   }
}
