using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _currentChanceSplit;
    [SerializeField] private float _chanceReductionMultiplier;
    [SerializeField] private float _scaleReductionMultiplier;

    public float CurrentChanceSplit => _currentChanceSplit;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Init(float previousChanceSplit, Vector3 previousScale, Color newColor)
    {
        _currentChanceSplit = previousChanceSplit / _chanceReductionMultiplier;
        transform.localScale = previousScale / _scaleReductionMultiplier;
        _renderer.material.color = newColor;
    }
}
