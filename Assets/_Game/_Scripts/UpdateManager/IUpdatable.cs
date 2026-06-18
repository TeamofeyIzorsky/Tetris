
public interface IUpdatable
{
    public void Tick(float deltaTime);
}

public interface IPauseUpdatable : IUpdatable
{
    public bool IsPausable { get; set; }
}

