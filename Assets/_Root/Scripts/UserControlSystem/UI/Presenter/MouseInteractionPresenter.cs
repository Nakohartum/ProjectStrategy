using System;
using System.Linq;
using _Root.Scripts.Abstractions;
using _Root.Scripts.Core.Unit;
using Abstractions;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Root.Scripts.UserControlSystem
{
    public class MouseInteractionPresenter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableObject _selectedObject;
        [SerializeField] private AttackableValue _attackableValue;
        [SerializeField] private EventSystem _eventSystem;
        [Inject] private CommandsButtonModel _commandsButtonModel;
        [SerializeField] private Vector3Value _groundClicksRMB;
        [SerializeField] private Transform _groundTransform;

        private Plane _groundPlane;

        private void Start()
        {
            _groundPlane = new Plane(_groundTransform.up, 0);
        }

        private void Update()
        {
            if (!Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1))
            {
                return;
            }

            if (_eventSystem.IsPointerOverGameObject())
            {
                return;
            }

            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray);
            if (Input.GetMouseButtonUp(0))
            {
                if (Hitted<ISelectable>(hits, out var selectable))
                {
                    _selectedObject.SetValue(selectable);
                }
            }
            else
            {
                if (Hitted<IAttackable>(hits, out var attackable))
                {
                    _attackableValue.SetValue(attackable);
                }
                else if (_groundPlane.Raycast(ray, out var enter))
                {
                    _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
                }
            }
    
            if (Input.GetMouseButtonDown(1))
            {
                if (_groundPlane.Raycast(ray, out var enter))
                {
                    _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
                    _commandsButtonModel.OnRightMouseButtonClick(_selectedObject.CurrentValue);
                }
            }
        }

        private bool Hitted<T>(RaycastHit[] hits, out T result) where T : class
        {
            result = default;
            if (hits.Length == 0)
            {
                return false;
            }

            result = hits.Select(hit => hit.collider.GetComponentInParent<T>()).FirstOrDefault(c => c != null);
            return result != default;
        }
    }
}