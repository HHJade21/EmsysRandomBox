using UnityEngine;
using UnityEngine.UI;
public class UI_Swipe : MonoBehaviour
{
    private Image image;

    public void Swipe()
    {
        image = this.GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Images/2");
    }
}