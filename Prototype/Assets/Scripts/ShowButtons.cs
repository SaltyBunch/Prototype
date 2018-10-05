using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    [SerializeField] private List<TextMeshProUGUI> _buttons;
    [SerializeField] private Button _buttonScript;
    private bool _complete;
    [SerializeField] private GameObject _lobbyButtons;
    private bool _selected;

    private bool _transition;

    public void OnPointerEnter(PointerEventData eventData) => ShowLobbyButtons();

    public void OnPointerExit(PointerEventData eventData) => HideLobbyButtons();

    public void OnSelect(BaseEventData eventData) => ShowLobbyButtons();

    private void Start()
    {
        if (_buttonScript == null)
            _buttonScript = GetComponent<Button>();
        if (_buttonScript == null)
            Debug.LogError("Button script not found on " + gameObject.name + " : " + gameObject.GetInstanceID());

        _lobbyButtons.SetActive(false);
    }

    private void HideLobbyButtons()
    {
        _transition = false;
        foreach (var button in _buttons) button.color = new Color(button.color.r, button.color.g, button.color.b, 0);

        _lobbyButtons.SetActive(false);

        _complete = false;
    }


    private void ShowLobbyButtons()
    {
        _transition = true;
        StartCoroutine(FadeInButtons());
    }

    private IEnumerator FadeInButtons()
    {
        _lobbyButtons.SetActive(true);
        while (_transition && !_complete)
        {
            foreach (var button in _buttons)
            {
                button.color = new Color(button.color.r, button.color.g, button.color.b,
                    button.color.a + 1.5f * Time.deltaTime);
                if (button.color.a >= 1)
                    _complete = true;
            }

            yield return null;
        }

        if (!_transition && !_complete)
        {
            foreach (var button in _buttons)
                button.color = new Color(button.color.r, button.color.g, button.color.b, 0);

            _lobbyButtons.SetActive(false);
        }
    }

    public void HideButtons() => HideLobbyButtons();
}