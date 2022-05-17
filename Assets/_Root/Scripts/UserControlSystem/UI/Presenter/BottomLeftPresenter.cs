using System;
using Abstractions;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Root.Scripts.UserControlSystem
{
    public class BottomLeftPresenter : MonoBehaviour
    {
        [SerializeField] private Image _selectedImage;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _sliderBackground;
        [SerializeField] private Image _sliderFillImage;
        [Inject] private IObservable<ISelectable> _selectableObject;

        private void Start()
        {
            _selectableObject.Subscribe(OnSelected);
        }

        private void OnSelected(ISelectable selected)
        {
            bool isSelected = selected != null;
            _selectedImage.enabled = isSelected;
            _healthSlider.gameObject.SetActive(isSelected);
            
            _text.enabled = isSelected;
            if (isSelected)
            {
                _selectedImage.sprite = selected.Icon;
                _text.text = $"{selected.Health}/{selected.MaxHealth}";
                _healthSlider.minValue = 0;
                _healthSlider.maxValue = selected.MaxHealth;
                _healthSlider.value = selected.Health;
                var color = Color.Lerp(Color.red, Color.green, selected.Health / (float) selected.MaxHealth);
                _sliderBackground.color = color * 0.5f;
                _sliderFillImage.color = color;
            }
        }
    }
}