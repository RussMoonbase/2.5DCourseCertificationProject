using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformCameraTarget : MonoBehaviour
{
   private bool _canFollowPlayer;
   private Transform _target;

   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void FixedUpdate()
   {
      if (_canFollowPlayer)
      {
         this.transform.position = _target.position;
      }
   }

   public void SetCanFollowPlayerBool(bool canFollowBoolean)
   {
      _canFollowPlayer = canFollowBoolean;
   }

   public void SetFollowTarget(Transform theTarget)
   {
      _target = theTarget;
   }

}
