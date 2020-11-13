using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
   [SerializeField] private Light _pointLight;

   private void OnTriggerStay(Collider other)
   {
      if (other.tag == "Player")
      {
         if (Input.GetKeyDown(KeyCode.P))
         {
            _pointLight.color = Color.green;
         }
         
      }
   }
}
