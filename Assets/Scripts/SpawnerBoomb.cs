using UnityEngine;

public class SpawnerBoomb : Spawner<Boomb>
{
    [SerializeField] private SpawnerCube _spawnerCube;

    private void OnEnable()
    {
        _spawnerCube.ReleasedCube += OnCubeDisapeared;
    }

    private void OnDisable()
    {
        _spawnerCube.ReleasedCube -= OnCubeDisapeared;
    }

    protected override void ActionOnGet(Boomb boomb)
    {
        base.ActionOnGet(boomb);

        boomb.DisapearedBoomb += OnCustomObjectDisapeared;
    }

    protected override void ActionOnRelease(Boomb boomb)
    {
        base.ActionOnRelease(boomb);

        boomb.DisapearedBoomb -= OnCustomObjectDisapeared;
    }

    private void OnCubeDisapeared(Cube cube)
    {
        Boomb boomb = GetCustomObject();
        boomb.ChangePosition(cube.transform.position);
    }
}