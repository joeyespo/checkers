using System;
using System.Drawing;
using Uberware.Gaming.Checkers;

namespace Uberware.Gaming.Checkers.Agents
{
  /// <summary>A Checkers agent that selects the next move completely randomly.</summary>
  public class CheckersRandomAgent : CheckersAgent
  {
    private Random rand;
    
    public CheckersRandomAgent ()
    { rand = new Random(); }
    public CheckersRandomAgent (int seed)
    { rand = new Random(seed); }
    
    public override CheckersMove NextMove (CheckersGame game)
    {
      CheckersPiece [] movable = game.EnumMovablePieces();
      CheckersMove move = new CheckersMove(game, movable[rand.Next(movable.Length)]);
      while (move.MustMove)
      {
        Point [] moves = move.EnumMoves();
        move.Move(moves[rand.Next(moves.Length)]);
      }
      return move;
    }
  }
}
