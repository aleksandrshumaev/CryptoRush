using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTool : MonoBehaviour
{
    [SerializeField] int _id;
    [SerializeField] string _name;
    [SerializeField] float _efficiensy;
    [SerializeField] float _initialPrice;

    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public float Efficiensy { get => _efficiensy;}

    public int GetActualPrice(int lvl)
    {
        int price = (int)(_initialPrice * Mathf.Pow(1.01f, lvl));
        return price;
    }
}
