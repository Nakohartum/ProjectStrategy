using System;
using System.Linq;
using Abstractions;
using UnityEngine;

namespace _Root.Scripts.UserControlSystem
{
    public class MouseInteractionPresenter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableObject _selectedObject;
        
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            if (hits.Length == 0)
            {
                return;
            }

            var mainBuilding = hits.Select(hit => hit.collider.GetComponent<IUnitProduce>())
                .FirstOrDefault(c => c != null);

            var selectable = hits.Select(hit => hit.collider.GetComponent<ISelectable>())
                .FirstOrDefault();
            _selectedObject.SetValue(selectable);

            if (mainBuilding == default)
            {
                return;
            }
            
            mainBuilding.ProduceUnit();
        }
    }
}