using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderChecker : MonoBehaviour
{
   [SerializeField] private Transform _snapHandsTarget;


   private void OnTriggerStay(Collider other)
   {
      if (other.tag == "Player")
      {
         Player thePlayer = other.GetComponent<Player>();

         if (thePlayer)
         {
            if (!thePlayer.GetClimbingLadder())
            {
               thePlayer.LadderClimbReady(_snapHandsTarget.position);
            }
            else
            {
               thePlayer.GetOffLadder();
            }
            
         }
      }
   }
}
