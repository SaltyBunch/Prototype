using UnityEngine;
using UnityEngine.EventSystems;

public class HideButtons : MonoBehaviour, ISelectHandler
{
    [SerializeField] private ShowButtons _showButtons;

    public void OnSelect(BaseEventData eventData) => _showButtons.HideButtons();
}