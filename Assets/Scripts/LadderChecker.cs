using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderChecker : MonoBehaviour
{
   [SerializeField] private Transform _snapHandsTarget;
   [SerializeField] private GameObject _topOfLadderChecker;

   private void OnTriggerStay(Collider other)
   {
      if (other.tag == "Player")
      {
         Player thePlayer = other.GetComponent<Player>();
         _topOfLadderChecker.SetActive(true);

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
