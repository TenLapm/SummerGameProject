using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public SoundManager.Sound hoverSound = SoundManager.Sound.Hover;
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.PlaySound(SoundManager.Sound.Hover);
    }
}
