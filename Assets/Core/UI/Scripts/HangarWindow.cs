using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HangarWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _header;

    [SerializeField] private Button _putItems;
    [SerializeField] private Button _takeItems;

    [SerializeField] private Hangar _hangar;
    [SerializeField] private SmoothShowHide _smoothShowHide;
    [SerializeField] private CharacterMovement _characterMovement;

    public Root Root;
    private IStorager _characterBag;

    private void Start()
    {
        _characterBag = Root.CharacterMovement.GetComponent<IStorager>();

        _putItems.onClick.AddListener(PutItems);
        _takeItems.onClick.AddListener(TakeItems);

        UpdateHeader();
    }

    private void OnEnable()
    {
        _hangar.OnCountChanged += UpdateHeader;
    }

    private void OnDisable()
    {
        _hangar.OnCountChanged -= UpdateHeader;
    }

    public void PutItems()
    {
        var itemsCount = _hangar.PutItems(_characterBag.GetCount());
        _characterBag.Take(itemsCount);
        UpdateHeader();
    }

    public void TakeItems()
    {
        var itemsCount = _hangar.GetItems(_hangar.Count);
        _characterBag.Add(itemsCount);
        UpdateHeader();
    }

    public void UpdateHeader()
    {
        _header.text = _hangar.GetPopUpText();
    }

    public void Show()
    {
        Root.CharacterMovement.enabled = false;
        _smoothShowHide.Show();
    }

    public void Hide()
    {
        _smoothShowHide.Hide();
        Root.CharacterMovement.enabled = true;
    }
}
