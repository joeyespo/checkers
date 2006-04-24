using System;

namespace Uberware.Gaming.Checkers.Agents
{
	public class MinMaxComplexAgent : MinMaxSimpleAgent
	{
    int [,] tableWeight = { { 0,4,0,4,0,4,0,4, },
                            { 4,0,3,0,3,0,3,0, },
                            { 0,3,0,2,0,2,0,4, },
                            { 4,0,2,0,1,0,3,0, },
                            { 0,3,0,1,0,2,0,4, },
                            { 4,0,2,0,2,0,3,0, },
                            { 0,3,0,3,0,3,0,4, },
                            { 4,0,4,0,4,0,4,0, },
    };
    
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
        if (piece.Player == player)
          strength += CalculatePieceStrength(piece);
      
      return strength;
    }
    
    int CalculatePieceStrength (CheckersPiece piece)
    {
      int strength = 0;
      
      // Heuristic: stronger if in key position on board, stronger yet if king
      if (piece.Rank == CheckersRank.Pawn)
      {
        if (piece.Direction == CheckersDirection.Up)
        {
          if (piece.Location.Y == CheckersGame.BoardBounds.Bottom-1)
            strength += 2;
          else
            strength += 1;
        }
        else
        {
          if (piece.Location.Y == CheckersGame.BoardBounds.Top+1)
            strength += 2;
          else
            strength += 1;
        }
      }
      else
      {
        // King piece more valuable
        strength += 10;
      }
      
      // Heuristic: Multiply the current strength by the board position
      strength *= tableWeight[piece.Location.X, piece.Location.Y];
      
      return strength;
    }
  }
}
