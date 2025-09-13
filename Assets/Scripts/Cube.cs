using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Spliter _spliter;
    [SerializeField] private float _currentChanceSplit;
    [SerializeField] private float _chanceReductionMultiplier;
    [SerializeField] private float _scaleReductionMultiplier;

    public float CurrentChanceSplit => _currentChanceSplit;

    private void Awake()
    {
        _spliter = GetComponent<Spliter>();
    }

    public void Init(float previousChanceSplit, Vector3 previousScale, Color newColor)
    {
        Renderer renderer = GetComponent<Renderer>();

        _currentChanceSplit = previousChanceSplit / _chanceReductionMultiplier;
        transform.localScale = previousScale / _scaleReductionMultiplier;
        renderer.material.color = newColor;
    }

    public void Split()
    {
        _spliter.SplitCube(this);
    }
}
