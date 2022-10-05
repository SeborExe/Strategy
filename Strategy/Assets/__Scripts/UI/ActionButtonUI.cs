using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    TMP_Text actionNameText;
    Button button;

    private BaseAction baseAction;

    [SerializeField] GameObject selectedGameObject;

    private void Awake()
    {
        actionNameText = GetComponentInChildren<TMP_Text>();
        button = GetComponent<Button>();
    }

    public void SetBaseAction(BaseAction baseAction)
    {
        this.baseAction = baseAction;
        actionNameText.text = baseAction.GetActionName().ToUpper();

        button.onClick.AddListener(() => UnitActionSystem.Instance.SetSelectedAction(baseAction));
    }

    public void UpdateSelectedVisual()
    {
        BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
        selectedGameObject.SetActive(selectedBaseAction == baseAction);
    }
}
