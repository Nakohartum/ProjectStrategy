using System;
using System.Collections.Generic;
using Abstractions;
using UI.View;
using UniRx;
using UnityEngine;
using Zenject;


namespace _Root.Scripts.UserControlSystem.UI.Presenter
{
    public class CommandsButtonPresenter : MonoBehaviour
    {
        [Inject] private IObservable<ISelectable> _selectable;
        [SerializeField] private CommandButtonsView _view;
        [Inject] private CommandsButtonModel _model;
        private ISelectable _currentSelectable;

        private void Start()
        {
            _view.OnClick += _model.OnCommandButtonClicked;
            _model.OnCommandSent += _view.UnblockAllInteractions;
            _model.OnCommandCancel += _view.UnblockAllInteractions;
            _model.OnCommandAccepted += _view.BlockInteractions;

            _selectable.Subscribe(OnSelected);
        }

        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
            {
                return;
            }

            if (_currentSelectable != null)
            {
                _model.OnSelectionChanged();
            }

            _currentSelectable = selectable;
            _view.Clear();
            if (selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
                _view.MakeLayout(commandExecutors);
            }
        }
    }
}