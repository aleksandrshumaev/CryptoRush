using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmToolsPool : MonoBehaviour
{
    [SerializeField] List<FarmTool> _farmToolList = new List<FarmTool>();
    // Start is called before the first frame update
    public FarmTool GetToolFromId(int id)
    {
        foreach (FarmTool tool in _farmToolList)
        {
            if (tool.Id == id)
            {
                return tool;
            }
        }
        return null;
    }
    public int GetPoolCount()
    {
        return _farmToolList.Count;
    }
}
