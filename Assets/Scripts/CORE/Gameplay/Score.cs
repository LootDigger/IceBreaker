using System;
using UnityEngine;

namespace CORE.Gameplay
{
   [Serializable]
   public class Score : IModel<int>
   {
      [SerializeField]
      private int _scoreValue;
      public event Action<int> OnModelChangedEvent;
      
      public int Value { get => _scoreValue;
         set
         {
            _scoreValue = value;
            OnModelChangedEvent?.Invoke(_scoreValue);
         }
      }
   }
}
