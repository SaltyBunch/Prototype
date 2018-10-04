using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button _buttonScript;
    [SerializeField] private GameObject _lobbyButtons;
    private bool _selected;

    private bool _transition;
    private bool _complete;
    [SerializeField] private List<TextMeshProUGUI> _buttons;

    private void Start()
    {
        if (_buttonScript == null)
            _buttonScript = GetComponent<Button>();
        if (_buttonScript == null)
        {
            Debug.LogError("Button script not found on " + gameObject.name + " : " + gameObject.GetInstanceID());
        }

        _lobbyButtons.SetActive(false);
    }

    private void Update()
    {
//        if (EventSystem.current.currentSelectedGameObject == gameObject && _selected == false)
//        {
//            _selected = true;
//            ShowLobbyButtons();
//        }
//        else if (EventSystem.current.currentSelectedGameObject != gameObject && _selected == true)
//        {
//            _selected = false;
//            HideLobbyButtons();
//        }
    }

    private void HideLobbyButtons()
    {
        _transition = false;
        foreach (var button in _buttons)
        {
            button.color = new Color(button.color.r, button.color.g, button.color.b, 0);
        }

        _lobbyButtons.SetActive(false);
        
        _complete = false;
    }


    private void ShowLobbyButtons()
    {
        _transition = true;
        StartCoroutine(FadeInButtons());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _selected = true;
        ShowLobbyButtons();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _selected = false;
        HideLobbyButtons();
    }

    private IEnumerator FadeInButtons()
    {
        _lobbyButtons.SetActive(true);
        while (_transition && !_complete)
        {
            foreach (var button in _buttons)
            {
                button.color = new Color(button.color.r, button.color.g, button.color.b,
                    button.color.a + Time.deltaTime);
                if (button.color.a >= 1)
                    _complete = true;
            }

            yield return null;
        }

        if (!_transition && !_complete)
        {
            foreach (var button in _buttons)
            {
                button.color = new Color(button.color.r, button.color.g, button.color.b, 0);
            }

            _lobbyButtons.SetActive(false);
        }
    }
}