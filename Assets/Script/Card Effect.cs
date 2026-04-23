using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour
{
    public enum CardType
    {
        IncreaseClickRadius,
        IncreaseClickDamage,
        IncreaseSawRadius,
        AddSaw
    }

    public CardType type;

    public void Setup()
    {
        Button btn = GetComponent<Button>();

        btn.onClick.RemoveAllListeners();

        switch (type)
        {
            case CardType.IncreaseClickRadius:
                btn.onClick.AddListener(() =>
                    FindFirstObjectByType<TapToDestroy>().IncreaseRadius());
                break;

            case CardType.IncreaseClickDamage:
                btn.onClick.AddListener(() =>
                    FindFirstObjectByType<TapToDestroy>().IncreaseDamage());
                break;
            case CardType.IncreaseSawRadius:
                btn.onClick.AddListener(() =>
                    FindFirstObjectByType<Saw>().IncreaseRadius());
                break;
            case CardType.AddSaw:
                btn.onClick.AddListener(() =>
                    FindFirstObjectByType<SawPoolingManager>().AddSaw());
                break;
        }

        btn.onClick.AddListener(() =>
            CardSelectionUI.Instance.OnButtonClicked());
    }
}