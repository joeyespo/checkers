using System;
using Uberware.Gaming.Checkers;

namespace Uberware.Gaming.Checkers.Agents
{
  public class CheckersBestMatchAgent : CheckersAgent
  {
    private int depth;
    
    public CheckersBestMatchAgent (int depth)
    { this.depth = depth;  }
    
    public int Depth
    { get { return depth; } }
    
    public override CheckersMove NextMove (CheckersGame game)
    {
      foreach (CheckersPiece piece in game.EnumMovablePieces())
      {
        CheckersMove move = new CheckersMove(game, piece);
        move.EnumMoves();
      }
      // Should never reach here
      return null;
    }
  }
}
