using System;
using System.Collections.Generic;
using _Root.Scripts.Core.Unit;
using Abstractions;
using Injector;
using UI.View;
using UnityEngine;
using UserControl;


namespace _Root.Scripts.UserControlSystem.UI.Presenter
{
    public class CommandsButtonPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableObject _selectableObject;
        [SerializeField] private CommandButtonsView _commandButtonsView;
        [SerializeField] private AssetContext _assetContext;

        private ISelectable _currentSelectable;

        private void Start()
        {
            _selectableObject.OnSelected += Selected;
            Selected(_selectableObject.CurrentValue);
            _commandButtonsView.OnClick += OnButtonClick;
        }

        private void Selected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
            {
                return;
            }

            _currentSelectable = selectable;
            _commandButtonsView.Clear();
            if (selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as Component).GetComponents<ICommandExecutor>());
                _commandButtonsView.MakeLayout(commandExecutors);
            }
        }

        private void OnButtonClick(ICommandExecutor commandExecutor)
        {
            var unitProducer = commandExecutor as CommandExecutorBase<IProduceUnitCommand>;
            if (unitProducer != null)
            {
                unitProducer.ExecuteSpecificCommand(_assetContext.Inject(new ProduceUnitCommandHeir()));
                return;
            }

            var attack = commandExecutor as CommandExecutorBase<IAttackCommand>;
            if (attack != null)
            {
                attack.ExecuteSpecificCommand(_assetContext.Inject(new AttackCommand()));
                return;
            }
            var patrol = commandExecutor as CommandExecutorBase<IPatrolCommand>;
            if (patrol != null)
            {
                patrol.ExecuteSpecificCommand(_assetContext.Inject(new PatrolCommand()));
                return;
            }
            var stop = commandExecutor as CommandExecutorBase<IStopCommand>;
            if (stop != null)
            {
                stop.ExecuteSpecificCommand(_assetContext.Inject(new StopCommand()));
                return;
            }
            var move = commandExecutor as CommandExecutorBase<IMoveCommand>;
            if (move != null)
            {
                move.ExecuteSpecificCommand(_assetContext.Inject(new MoveCommand()));
                return;
            }

            throw new ApplicationException($"{nameof(CommandsButtonPresenter)}.{nameof(OnButtonClick)} :" +
                                           $"Unknown type of commands executor: {commandExecutor.GetType().FullName}!");
        }
    }
}