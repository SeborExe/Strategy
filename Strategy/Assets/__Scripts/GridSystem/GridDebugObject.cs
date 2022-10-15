using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    private object gridObject;
    [SerializeField] TMP_Text gridText;

    private void Awake()
    {
        gridText = GetComponentInChildren<TMP_Text>();
    }

    public  virtual void SetGridObject(object gridObject)
    {
        this.gridObject = gridObject;
    }

    protected virtual void Update()
    {
        gridText.text = gridObject.ToString();
    }
}
