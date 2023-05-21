
public class Timer
{
    public float TimePassed { get; private set; } = 0;
    public bool IsTicking { get; private set; } = false;

    public void Tick(float timePassed)
    {
        if (IsTicking)
            TimePassed += timePassed;
    }

    public void Start()
    {
        IsTicking = true;
    }

    public void Stop()
    {
        IsTicking = false;
    }

    public void Reset()
    {
        TimePassed = 0;
    }
}
