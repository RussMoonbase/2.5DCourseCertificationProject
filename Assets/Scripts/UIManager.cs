using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
   public static UIManager instace;
   [SerializeField] private TMP_Text _coinAmountText;

   private void Awake()
   {
      instace = this;
   }

   // Start is called before the first frame update
   void Start()
   {
      _coinAmountText.text = "0";
   }

   // Update is called once per frame
   void Update()
   {

   }

   public void UpdateCoinAmount(int newAmount)
   {
      if (_coinAmountText)
      {
         _coinAmountText.text = newAmount.ToString();
      }
      
   }
}
