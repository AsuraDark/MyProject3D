using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color RandomColor => new Color(Random.value, Random.value, Random.value);
}
