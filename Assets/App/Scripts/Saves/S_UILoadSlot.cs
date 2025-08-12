using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_UILoadSlot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI textNameValue;
    [SerializeField] private TextMeshProUGUI textPlayerValue;
    [SerializeField] private TextMeshProUGUI textDateValue;
    [SerializeField] private Button buttonSlot;
    [SerializeField] private Button buttonLoad;
    [SerializeField] private Button buttonDelete;

    [HideInInspector] public string saveName;

    private S_UILoads currentUILoads = null;

    public void Setup(S_UILoads UILoads, S_ClassDataTemp dataTemp, ScrollRect scrollRect)
    {
        currentUILoads = UILoads;

        saveName = dataTemp.saveName;

        textNameValue.text = $"{dataTemp.saveName}";

        if (dataTemp.dateSaved != "")
        {
            UpdateSlot(dataTemp);
        }
        else
        {
            ClearSlot();
        }

        buttonSlot.GetComponent<S_UISelectableScroll>().Setup(scrollRect);
        buttonLoad.GetComponent<S_UISelectableScroll>().Setup(scrollRect);
        buttonDelete.GetComponent<S_UISelectableScroll>().Setup(scrollRect);

        SetupNavigation();
    }

    private void SetupNavigation()
    {
        if (buttonLoad.IsInteractable())
        {
            Navigation nav = buttonSlot.navigation;
            nav.mode = Navigation.Mode.Explicit;

            nav.selectOnLeft = buttonLoad.GetComponent<Selectable>();
            nav.selectOnRight = buttonLoad.GetComponent<Selectable>();

            buttonSlot.navigation = nav;
        }
    }

    public void UpdateSlot(S_ClassDataTemp dataTemp)
    {
        textPlayerValue.text = $"{dataTemp.playerName}, {dataTemp.currentLevel.Substring(3)}";
        textDateValue.text = $"{dataTemp.dateSaved}";

        buttonLoad.interactable = true;
        buttonDelete.interactable = true;
    }

    public void ClearSlot()
    {
        textPlayerValue.text = $"";
        textDateValue.text = $"";

        buttonLoad.interactable = false;
        buttonDelete.interactable = false;
    }

    public void ScrollAuto(Selectable item)
    {
        currentUILoads.ScrollAuto(item);
    }
}