using UnityEngine;
using UnityEngine.UI;

public class ChoseeButtons : MonoBehaviour
{
    public Text HeroText;
    private void Hero1Ckilck()
    {
        HeroText.text = "Военачальник воодушивляет карты и переманивает карты противника. Вначале игры получает артефакт:";
    }
    private void Hero2Ckilck()
    {
        HeroText.text = "Coming soon";
    }
}
