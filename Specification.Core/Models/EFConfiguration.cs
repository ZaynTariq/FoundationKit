namespace Specification.Core.Models;

public class EFConfiguration
{
    private EFConfiguration() { }

    public static EFConfiguration? Instance { get; private set; }
    public static EFConfiguration Create()
    {
        Instance ??= new EFConfiguration();

        return Instance;
    }
    public bool UseAsNotTracking { get; set; } = true;
}
