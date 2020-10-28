using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrabChecker : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Player")
      {
         Debug.Log("Hit");
         Player.instance.SetIsHanging(true);
      }
   }
}
