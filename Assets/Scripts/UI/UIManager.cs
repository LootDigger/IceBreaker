using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
  public class UIManager : MonoBehaviour
  {
    [SerializeField] 
    private UIDocument _uiDocument;
    
    private UIScreen _mainMenuUIScreen;
    private UIScreen _gameplayUIScreen;
    private UIScreen _gameOverUIScreen;

    private List<UIScreen> _uiScreens;

    private void Awake()
    {
      InitScreens();

    }

    private void InitScreens()
    {
      var root = _uiDocument.rootVisualElement;
      _mainMenuUIScreen = new UIScreen(root.Q<VisualElement>("MainMenuUI"));
      _gameplayUIScreen = new UIScreen(root.Q<VisualElement>("MainMenuUI"));
      _gameOverUIScreen = new UIScreen(root.Q<VisualElement>("GameOverUI"));

      _uiScreens = new List<UIScreen>()
      {
        _mainMenuUIScreen,
        _gameplayUIScreen,
        _gameOverUIScreen
      };
    }
    
    
    
    
  }
}
