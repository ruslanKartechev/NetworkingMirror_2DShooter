using System.Collections.Generic;
using GameCore.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.Lobby
{
    public class LobbyUI : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _clientButton;
        [SerializeField] private TMP_InputField _playerNameField;
        [SerializeField] private TMP_InputField _lobbyNameField;
        [SerializeField] private int _maxCharactersInput;
        [Space(10)] 
        [SerializeField] private PlayerSpritePainterUI _painter;
        [SerializeField] private Button _nextColorButton;
        [SerializeField] private Button _prevColorButton;
        [SerializeField] private PlayerAppearanceRepository _appearanceRepository;
        private GameManager _gameManager;
        private ILobbyUIListener _listener;
        private string _lobbyName = "localHost";
        private string _playerName;
        private int _index;

        public void SetListener(ILobbyUIListener listener)
        {
            _listener = listener;
            ApplyColor();
        }

        public void Show()
        {
            SetupInputField(_playerNameField);
            SetupInputField(_lobbyNameField);
            
            _playerNameField.onEndEdit.RemoveAllListeners();
            _playerNameField.onEndEdit.AddListener(SavePlayerName);
            
            _lobbyNameField.onEndEdit.RemoveAllListeners();
            _lobbyNameField.onEndEdit.AddListener(SaveLobbyName);
            
            _hostButton.onClick.AddListener(() =>
            {
                _listener.OnCreateRoom(_lobbyName);
            });
            _clientButton.onClick.AddListener(() =>
            {
                _listener.OnJoinRoom(_lobbyName);
            });
            _nextColorButton.onClick.AddListener(NextColor);
            _prevColorButton.onClick.AddListener(PrevColor);
            SetRandomColor();
            SetRandomName();
            _canvas.enabled = true;
        }

        private void SetRandomName()
        {
            _playerName = _appearanceRepository.GetRandomName();
            _playerNameField.text = _playerName;
            _listener.OnSetName(_playerName);
        }

        private void SetRandomColor()
        {
            _index = _appearanceRepository.GetRandomColorIndex();
            ApplyColor();
        }

        private void NextColor()
        {
            _index++;
            CorrectIndex();
            ApplyColor();
        }

        private void PrevColor()
        {
            _index--;
            CorrectIndex();
            ApplyColor();
        }

        private void ApplyColor()
        {
            var color = _appearanceRepository.GetColor(_index);
            _painter.SetColor(color);
            _listener.SetColor(color);
        }

        private void CorrectIndex()
        {
            if (_index >= _appearanceRepository.ColorsCount)
                _index = 0;
            if (_index < 0)
                _index = _appearanceRepository.ColorsCount;
        }
        
        
        private void SetupInputField(TMP_InputField inputField)
        {
            inputField.textComponent.alignment = TextAlignmentOptions.Center;
            inputField.lineLimit = 1;
            inputField.characterLimit = _maxCharactersInput;
        }
        
        public void Hide()
        {
            _canvas.enabled = false;
        }

        private void SaveLobbyName(string name)
        {
            _lobbyName = name;
        }
        
        private void SavePlayerName(string name)
        {
            _playerName = name;
            _listener.OnSetName(name);
        }

        private void OnDisable()
        {
            _hostButton.onClick.RemoveAllListeners();
            _clientButton.onClick.RemoveAllListeners();
        }
    }
}