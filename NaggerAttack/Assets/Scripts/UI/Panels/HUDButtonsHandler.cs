using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDButtonsHandler : MonoBehaviour
{
    [SerializeField] private Button _openMenuButton;
    [SerializeField] private MainMenuPanel _mainMenuPanel;
    [SerializeField] private GameCycle _gameCycle;

    #region MonoBehaviour

    private void OnEnable()
    {
        _openMenuButton.onClick.AddListener(OnOpenMenuButtonClick);
    }

    private void OnDisable()
    {
        _openMenuButton.onClick.RemoveListener(OnOpenMenuButtonClick);
    }

    #endregion

    private void OnOpenMenuButtonClick()
    {
        _mainMenuPanel.Show();
        _gameCycle.Pause();
    }
}
