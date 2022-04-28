using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class CommandButtonsView : MonoBehaviour
    {
        public Action<ICommandExecutor> OnClick;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _patrolButton;
        [SerializeField] private Button _stopButton;
        [SerializeField] private Button _produceUnitButton;

        private Dictionary<Type, Button> _buttonsByExecutorType;

        private void Start()
        {
            _buttonsByExecutorType = new Dictionary<Type, Button>();
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IAttackCommand>), _attackButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IMoveCommand>), _moveButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IPatrolCommand>), _patrolButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IStopCommand>), _stopButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IProduceUnitCommand>), _produceUnitButton);
        }

        public void BlockInteractions(ICommandExecutor ce)
        {
            UnblockAllInteractions();
            GetButtonGameObjectByType(ce.GetType()).GetComponent<Selectable>().interactable = false;
        }

        public void UnblockAllInteractions()
        {
            SetInteractable(true);
        }

        private void SetInteractable(bool value)
        {
            _attackButton.GetComponent<Selectable>().interactable = value;
            _moveButton.GetComponent<Selectable>().interactable = value;
            _patrolButton.GetComponent<Selectable>().interactable = value;
            _stopButton.GetComponent<Selectable>().interactable = value;
            _produceUnitButton.GetComponent<Selectable>().interactable = value;
        }

        private Button GetButtonGameObjectByType(Type executorInstanceType)
        {
            return _buttonsByExecutorType.First(type => type.Key.IsAssignableFrom(executorInstanceType)).Value;
        }
        
        public void MakeLayout(IEnumerable<ICommandExecutor> commandExecutors)
        {
            foreach (ICommandExecutor commandExecutor in commandExecutors)
            {
                var button = _buttonsByExecutorType
                    .First(type => type.Key.IsInstanceOfType(commandExecutor)).Value;
                button.gameObject.SetActive(true);
                button.onClick.AddListener(() => OnClick?.Invoke(commandExecutor));
            }
        }

        public void Clear()
        {
            foreach (var button in _buttonsByExecutorType)
            {
                button.Value.onClick.RemoveAllListeners();
                button.Value.gameObject.SetActive(false);
            }
        }
    }
}