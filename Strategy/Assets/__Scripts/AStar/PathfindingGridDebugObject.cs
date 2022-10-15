using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PathfindingGridDebugObject : GridDebugObject
{
    [SerializeField] TMP_Text gCostText;
    [SerializeField] TMP_Text hCostText;
    [SerializeField] TMP_Text fCostText;

    private PathNode pathNode;

    public override void SetGridObject(object gridObject)
    {
        base.SetGridObject(gridObject);
        pathNode = (PathNode)gridObject;
    }

    protected override void Update()
    {
        base.Update();
        gCostText.text = pathNode.GetGCost().ToString();
        hCostText.text = pathNode.GetHCost().ToString();
        fCostText.text = pathNode.GetFCost().ToString();
    }
}
