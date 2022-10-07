using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] TMP_Text actionPointsText;
    [SerializeField] private Unit unit;
    [SerializeField] Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;

    private void Start()
    {
        Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;
        healthSystem.OnDamage += HealthSystem_OnDamage;

        UpdateActionPointsText();
        UpdateHealthBar();
    }

    private void Unit_OnAnyActionPointsChanged(object sender, System.EventArgs e)
    {
        UpdateActionPointsText();
    }

    private void HealthSystem_OnDamage(object sender, System.EventArgs e)
    {
        UpdateHealthBar();
    }

    private void UpdateActionPointsText()
    {
        actionPointsText.text = unit.GetActionPoints().ToString();
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = healthSystem.GetHealthNormalized();
    }
}
