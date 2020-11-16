﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Player")
      {
         other.GetComponent<PlayerCollectable>().UpdatePlayerCoinsByOne();
         Destroy(this.gameObject);
      }
   }
}
