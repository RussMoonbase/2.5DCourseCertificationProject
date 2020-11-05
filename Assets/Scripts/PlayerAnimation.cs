using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
   private float _rollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void Roll(int speed)
   {
      _rollSpeed = speed;
   }

   public float GetRollSpeed()
   {
      Debug.Log("RollSpeed = " + _rollSpeed);
      return _rollSpeed;
   }
}
