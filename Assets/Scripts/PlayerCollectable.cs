using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectable : MonoBehaviour
{
   [SerializeField] private int _coinAmount;

   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {

   }

   public void UpdatePlayerCoinsByOne()
   {
      _coinAmount++;
      UIManager.instace.UpdateCoinAmount(_coinAmount);
   }
}
