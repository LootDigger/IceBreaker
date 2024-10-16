using System.Collections;
using System.Collections.Generic;
using CORE;
using CORE.GameStates;
using Patterns.ServiceLocator;
using UnityEngine;

public class GameStartUI_TEMP : MonoBehaviour
{
   public void StartGame()
   {
      GameManagerController.GetInstance().SetGameState(ServiceLocator.GetService<CORESM_InGame>());
      gameObject.SetActive(false);
   }
}
