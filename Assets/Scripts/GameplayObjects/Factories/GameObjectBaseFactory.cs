using UnityEngine;

public abstract class GameObjectBaseFactory : IGameObjectFactory
{
    public abstract GameObject Create();
}
