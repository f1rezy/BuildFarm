using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void Init(int value)
    {
        _text.text = "+" + value.ToString();
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
