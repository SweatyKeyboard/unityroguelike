using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Interactive : MonoBehaviour
{
    [SerializeField] Common.InteractiveType type;

    public Common.InteractiveType Type { get { return type; } set { type = value; } }
    public int Index { get; set; }
}
