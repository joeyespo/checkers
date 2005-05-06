using System;
using System.Drawing;
using System.Collections;
using Uberware.Gaming.Checkers;

namespace Uberware.Gaming.Checkers.Agents
{
  public class CheckersMostJumpsAgent : CheckersAgent
  {
    private Random rand;
    
    public CheckersMostJumpsAgent ()
    { rand = new Random(); }
    public CheckersMostJumpsAgent (int seed)
    { rand = new Random(seed); }
    
    public override CheckersMove NextMove (CheckersGame game)
    {
      int maxJumps = 0;
      CheckersPiece [] movables = game.EnumMovablePieces();
      ArrayList possibleMoves = new ArrayList(movables.Length);
      foreach (CheckersPiece movable in movables)
        possibleMoves.Add(new CheckersMove(game, movable));
      // Get all possible jump combos
      ArrayList finishedMoves = new ArrayList();
      while (possibleMoves.Count > 0)
      {
        CheckersMove move = (CheckersMove)possibleMoves[0];
        possibleMoves.RemoveAt(0);
        Point [] points = move.EnumMoves();
        if (points.Length == 0)
        {
          // Move is complete; add to finished moves and test for current max jumps
          finishedMoves.Add(move);
          if (maxJumps < move.Jumped.Length) maxJumps = move.Jumped.Length;
          continue;
        }
        // Enumerate all moves from this point and append them to the possible moves array
        foreach (Point p in points)
        {
          CheckersMove next = move.Fork();
          possibleMoves.Add(next.Move(p));
        }
      }
      // Get list of max jumps
      ArrayList moveList = new ArrayList();
      foreach (CheckersMove move in finishedMoves)
      {
        if (move.Jumped.Length != maxJumps) continue;
        moveList.Add(move);
      }
      // Choose at random between any path with same number of jumps
      if (moveList.Count == 0) return null;
      return (CheckersMove)moveList[rand.Next(moveList.Count)];
    }
  }
}
