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
    
    private LoadingUIScreen _loadingUIScreen;
    private MainMenuUIScreen _mainMenuUIScreen;
    private GameplayUIScreen _gameplayUIScreen;
    private GameOverUIScreen _gameOverUIScreen;

    private List<UIScreen> _uiScreens;
    
    private UIScreen _currentScreen;

    private void Awake()
    {
      DontDestroyOnLoad(gameObject);
      InitScreens();
      SubscribeDockerEvents();
    }

    private void OnDestroy()
    {
      UnsubscribeDockerEvents();
    }

    private void InitScreens()
    {
      var root = _uiDocument.rootVisualElement;
      _loadingUIScreen = new LoadingUIScreen(root.Q<VisualElement>("LoadingUI"));
      _mainMenuUIScreen = new MainMenuUIScreen(root.Q<VisualElement>("GameMenuUI"));
      _gameplayUIScreen = new GameplayUIScreen(root.Q<VisualElement>("InGameUI"));
      _gameOverUIScreen = new GameOverUIScreen(root.Q<VisualElement>("GameOverUI"));

      _uiScreens = new List<UIScreen>()
      {
        _loadingUIScreen,
        _mainMenuUIScreen,
        _gameplayUIScreen,
        _gameOverUIScreen
      };
    }

    private void SubscribeDockerEvents()
    {
      UIEventDocker.OnLoadingUIShown += UIEventDockerOnOnLoadingUIShown;
      UIEventDocker.OnMainMenuUIShown += UIEventDockerOnOnMainMenuUIShown;
      UIEventDocker.OnGameplayUIShown += UIEventDockerOnOnGameplayUIShown;
      UIEventDocker.OnGameOverUIShown += UIEventDockerOnOnGameOverUIShown;
    }
    
    private void UnsubscribeDockerEvents()
    {
      UIEventDocker.OnLoadingUIShown -= UIEventDockerOnOnLoadingUIShown;
      UIEventDocker.OnMainMenuUIShown -= UIEventDockerOnOnMainMenuUIShown;
      UIEventDocker.OnGameplayUIShown -= UIEventDockerOnOnGameplayUIShown;
      UIEventDocker.OnGameOverUIShown -= UIEventDockerOnOnGameOverUIShown;
    }

    private void UIEventDockerOnOnGameOverUIShown()
    {
      ShowScreen<GameOverUIScreen>();
    }

    private void UIEventDockerOnOnGameplayUIShown()
    {
      ShowScreen<GameplayUIScreen>();
    }

    private void UIEventDockerOnOnMainMenuUIShown()
    {
      ShowScreen<MainMenuUIScreen>();
    }

    private void UIEventDockerOnOnLoadingUIShown()
    {
      ShowScreen<LoadingUIScreen>();
    }

    public void ShowScreen<T>() where T: UIScreen
    {
      foreach (var screen in _uiScreens)
      {
        if (screen is T)
        {
          Show(screen);
          break;
        }
      }
    }
    
    private void Show(UIScreen screen)
    {
      if (screen == null)
        return;

      if (_currentScreen != null)
      {
          _currentScreen.HideInstantly();
      }

      screen.ShowInstantly();
      _currentScreen = screen;
    }
  }
}
