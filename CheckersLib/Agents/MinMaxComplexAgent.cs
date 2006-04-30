using System;

namespace Uberware.Gaming.Checkers.Agents
{
	public class MinMaxComplexAgent : MinMaxSimpleAgent
	{
    public MinMaxComplexAgent (int maxSearchDepth) : base(maxSearchDepth)
		{}
    public MinMaxComplexAgent () : base()
    {}
    
    protected override int CalculateStrength (CheckersGame initGame, CheckersGame curGame)
    {
      int player   = (( curGame.Turn != 1 )?( 2 ):( 1 ));
      int opponent = (( curGame.Turn != 1 )?( 1 ):( 2 ));
      int strength = base.CalculateStrength(initGame, curGame);
      
      // Sum all player's piece heuristic values
      foreach (CheckersPiece piece in curGame.Pieces)
      {
        if (piece.Player == player)
          strength += CalculatePieceStrength(piece);
        else
          strength -= CalculatePieceStrength(piece);
      }
      
      return strength;
    }
    
    int CalculatePieceStrength (CheckersPiece piece)
    {
      int strength = 0;
      
      // Heuristic: Stronger simply because another piece is present
      strength += 1;
      
      // Rank-specific heuristics
      if (piece.Rank == CheckersRank.Pawn)
      {
        // Heuristic: stronger if in primary position on board
        if (piece.Direction == CheckersDirection.Up)
        { if (piece.Location.Y == CheckersGame.BoardBounds.Bottom) strength += 1; }
        else
        { if (piece.Location.Y == CheckersGame.BoardBounds.Top) strength += 1; }
      }
      else
      {
        // Heuristic: Stronger if king is on board
        strength += 19;
      }
      
      return strength;
    }
  }
}
