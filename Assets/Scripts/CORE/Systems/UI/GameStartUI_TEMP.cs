using System.Collections;
using System.Collections.Generic;
using CORE;
using CORE.GameStates;
using CORE.GameStates.Commands;
using Patterns.Command;
using Patterns.ServiceLocator;
using UnityEngine;

public class GameStartUI_TEMP : MonoBehaviour
{
   public void StartGame()
   {
      CommandExecuter.ExecuteCommand(new SetInGameStateCommand());
      gameObject.SetActive(false);
   }
}
