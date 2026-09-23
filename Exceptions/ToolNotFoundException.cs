namespace ToolTrack.Exceptions;

public class ToolNotFoundException : Exception
{
    public ToolNotFoundException(string message)
        : base(message)
    {
    }
}