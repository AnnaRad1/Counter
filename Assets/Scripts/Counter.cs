using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private float _startNumber = 0f;
    [SerializeField] private float _maxNumber = 100f;
    [SerializeField] private Button _button;

    private Coroutine _coroutine;
    private float _currentNumber;
    private bool _isCounting = false;
    private WaitForSecondsRealtime _wait;

    public float StartNumber => _startNumber;

    public event Action<float> Changed;

    private void Awake()
    {
        _currentNumber = _startNumber;
        _wait = new WaitForSecondsRealtime(_delay);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        _isCounting = !_isCounting;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(IncreaseNumbers());
    }

    public IEnumerator IncreaseNumbers()
    {
        while (_isCounting && _currentNumber != _maxNumber)
        {
            _currentNumber++;
            Changed?.Invoke(_currentNumber);
            yield return _wait;
        }
    }
}
