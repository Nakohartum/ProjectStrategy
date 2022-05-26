using System.Text;
using _Root.Scripts.Abstractions;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Root.Scripts.UserControlSystem.UI.Presenter
{
    public class GameOverScreenPresenter : MonoBehaviour
    {
        [Inject] private IGameStatus _gameStatus;

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _view;

        [Inject]

        private void Init()
        {
            _gameStatus.Status.ObserveOnMainThread().Subscribe(result =>
            {
                var sb = new StringBuilder($"Game over!");
                if (result == 0)
                {
                    sb.AppendLine("Draw");
                }
                else
                {
                    sb.AppendLine($"The winner is #{result}");
                }

                _view.SetActive(true);
                _text.text = sb.ToString();
                Time.timeScale = 0;
            });
        }
    }
}