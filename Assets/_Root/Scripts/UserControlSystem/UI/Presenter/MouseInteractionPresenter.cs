using System;
using System.Linq;
using _Root.Scripts.Abstractions;
using _Root.Scripts.Core.Unit;
using Abstractions;
using UniRx;
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


        [Inject]
        private void Init()
        {
            _groundPlane = new Plane(_groundTransform.up, 0);
            var allStreams = Observable.EveryUpdate().Where(_ => !_eventSystem.IsPointerOverGameObject());
            var rightClicks = allStreams.Where(_ => Input.GetMouseButtonDown(1));
            var leftClicks = allStreams.Where(_ => Input.GetMouseButtonDown(0));
            var leftMouseRay = leftClicks.Select(_ => _camera.ScreenPointToRay(Input.mousePosition));
            var rightMouseRay = rightClicks.Select(_ => _camera.ScreenPointToRay(Input.mousePosition));
            var leftHits = leftMouseRay.Select(ray => Physics.RaycastAll(ray));
            var rightHits = rightMouseRay.Select(ray =>(ray, Physics.RaycastAll(ray)));
            leftHits.Subscribe(hits =>
            {
                if (Hitted<ISelectable>(hits, out var selectable))
                {
                    _selectedObject.SetValue(selectable);
                }
            });
            rightHits.Subscribe(data =>
            {
                var (ray, hits) = data;
                if (Hitted<IAttackable>(hits, out var attackable))
                {
                    _attackableValue.SetValue(attackable);
                }
                else if (_groundPlane.Raycast(ray, out var enter))
                {
                    _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
                }
            });
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