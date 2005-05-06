using System;

namespace Uberware.Gaming.Checkers
{
  public abstract class CheckersAgent
  {
    /// <summary>Returns the best possible next move using the current move-finding technique. </summary>
    /// <returns>The next best possible move.</returns>
    public abstract CheckersMove NextMove (CheckersGame game);
  }
}
