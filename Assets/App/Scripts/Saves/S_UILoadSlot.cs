using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_UILoadSlot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI textNameValue;
    [SerializeField] private TextMeshProUGUI textPlayerValue;
    [SerializeField] private TextMeshProUGUI textDateValue;
    [SerializeField] private Button buttonLoad;
    [SerializeField] private Button buttonDelete;

    [HideInInspector] public string saveName;

    public void Setup(S_ClassDataTemp dataTemp)
    {
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
}