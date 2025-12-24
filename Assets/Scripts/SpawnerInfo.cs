using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SpawnerInfo<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] Spawner<T> _spawner;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _spawner.SpawnedObject += UpdateInfoSpawner;
    }

    private void OnDisable()
    {
        _spawner.SpawnedObject -= UpdateInfoSpawner;
    }

    private void UpdateInfoSpawner()
    {
        _text.text = new string($"Spawner {typeof(T).Name}\n" +
            $"количество заспавненых объектов за всё время (появление на сцене: {_spawner.SpawnedObjects}\n" +
            $"количество созданных объектов: {_spawner.CreatedObjects} \n" +
            $"количество активных объектов на сцене: {_spawner.ActiveObjects}");
    }
}