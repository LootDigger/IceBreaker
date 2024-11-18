using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIEventDocker : MonoBehaviour
{
   public static Action OnLoadingUIShown;
   public static Action OnMainMenuUIShown;
   public static Action OnGameplayUIShown;
   public static Action OnGameOverUIShown;
}
