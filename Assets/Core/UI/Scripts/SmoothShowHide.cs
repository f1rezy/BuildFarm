using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothShowHide : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private float _duration = .5f;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _targetAlpha = 1f;


    private IEnumerator SmoothShow(float duration)
    {
        _group.interactable = true;
        _group.blocksRaycasts = true;
        for (float i = 0; i <= 1f; i += Time.deltaTime / duration)
        {
            _group.alpha = _targetAlpha * _curve.Evaluate(i);
            yield return null;
        }
    }

    private IEnumerator SmoothHide(float duration)
    {
        for (float i = 1f; i >= 0; i -= Time.deltaTime / duration)
        {
            _group.alpha = _targetAlpha * _curve.Evaluate(i);
            yield return null;
        }
        _group.interactable = false;
        _group.blocksRaycasts = false;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        _group.alpha = 0;
        _group.interactable = false;
        _group.blocksRaycasts = false;
        gameObject.SetActive(true);
        StartCoroutine(SmoothShow(_duration));
    }

    public void Hide()
    {
        StartCoroutine(SmoothHide(_duration));
    }

    private void Start()
    {
        if( _group == null ) _group = GetComponent<CanvasGroup>();
    }
}
