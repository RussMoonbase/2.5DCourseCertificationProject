using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderCheckerTop : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Player")
      {
         Player thePlayer = other.GetComponent<Player>();

         if (thePlayer)
         {
            thePlayer.ReachedTopOfLadder();
         }
      }
   }
}
