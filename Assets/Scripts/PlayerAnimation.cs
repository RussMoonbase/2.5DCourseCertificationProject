using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
   private float _rollSpeed;
   private Player _thePlayer;

    // Start is called before the first frame update
    void Start()
    {
      _thePlayer = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void Roll(int speed)
   {
      _thePlayer.SetRollSpeed(speed);
   }

}
