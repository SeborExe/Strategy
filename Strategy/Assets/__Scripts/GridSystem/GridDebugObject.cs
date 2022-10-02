using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    private GridObject gridObject;
    TMP_Text gridText;

    private void Awake()
    {
        gridText = GetComponentInChildren<TMP_Text>();
    }

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }

    private void Update()
    {
        gridText.text = gridObject.ToString();
    }
}
