using System;
using System.Collections;
using System.Collections.Generic;
using CORE;
using CORE.GameStates;
using Patterns.Command;
using Patterns.ServiceLocator;
using UnityEngine;

public class GameStartUI_TEMP : MonoBehaviour
{
   private CoreStateMachine coreStateMachine;
   private void Start()
   {
      coreStateMachine = ServiceLocator.GetService<CoreStateMachine>();
   }

   public void StartGame()
   {
      coreStateMachine.SetState<CORE_GameplayState>();
      gameObject.SetActive(false);
   }
}
