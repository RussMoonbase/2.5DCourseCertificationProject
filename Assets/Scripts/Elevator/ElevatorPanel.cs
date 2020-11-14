using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
   [SerializeField] private Light _pointLight;
   [SerializeField] private bool _isTopFloor;
   [SerializeField] private Elevator _theElevator;

   private void Start()
   {

   }


   private void OnTriggerStay(Collider other)
   {
      if (other.tag == "Player")
      {
         if (Input.GetKeyDown(KeyCode.P))
         {
            _pointLight.color = Color.green;

            if (_isTopFloor)
            {
               _theElevator.SetElevatorToGoingUp();
            }
         }
         
      }
   }
}
