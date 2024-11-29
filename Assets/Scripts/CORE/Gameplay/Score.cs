using System;
using UnityEngine;

namespace CORE.Gameplay
{
   [Serializable]
   public class Score : IModel
   {
      [SerializeField]
      private int _scoreValue;
      
      public event Action<int> OnScoreChangedEvent;

      public int ScoreValue
      {
         get => _scoreValue;
         set
         {
            _scoreValue = value;
            OnScoreChangedEvent.Invoke(_scoreValue);
         }
      }
   }
}
