using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderCheckerTop : MonoBehaviour
{
   [SerializeField] private Transform _bodySnapPosition;
   [SerializeField] private Transform _ledgeCameraTransform;

   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Player")
      {
         Player thePlayer = other.GetComponent<Player>();

         if (thePlayer)
         {
            thePlayer.ReachedTopOfLadder(_bodySnapPosition.position, _ledgeCameraTransform, this.gameObject);
         }
      }
   }


}
