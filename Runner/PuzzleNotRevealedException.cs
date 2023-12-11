using System.Runtime.Serialization;

namespace Formula9.AdventOfCode.Runner;

[Serializable]
public class PuzzleNotRevealedExceptionException : Exception
{
    public PuzzleNotRevealedExceptionException() { }
    public PuzzleNotRevealedExceptionException(string message) : base(message) { }
    public PuzzleNotRevealedExceptionException(string message, Exception inner) : base(message, inner) { }
    protected PuzzleNotRevealedExceptionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}