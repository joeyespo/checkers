using System;

namespace Uberware.Gaming.Checkers.Agents
{
	public class MinMaxSimpleAgent : MinMaxBaseAgent
	{
    public MinMaxSimpleAgent (int maxSearchDepth) : base(maxSearchDepth)
		{}
    public MinMaxSimpleAgent () : base()
    {}
    
    
    protected override int CalculateStrength (CheckersGame initGame, CheckersGame curGame)
    {
      int player   = (( curGame.Turn != 1 )?( 2 ):( 1 ));
      int opponent = (( curGame.Turn != 1 )?( 1 ):( 2 ));
      int strength = 0;
      
      // Strength Heuristics
      // --------------------
      
      // Heuristic: Stronger for each player pawn and king the player still has on the board
      strength +=  3 * curGame.GetPawnCount(player);
      strength += 10 * curGame.GetKingCount(player);
      
      // Heuristic: Stronger if opponent was jumped
      strength += 2 * ((initGame.GetPawnCount(opponent) - curGame.GetPawnCount(opponent)));
      strength += 5 * (initGame.GetKingCount(opponent) - curGame.GetKingCount(opponent));
      
      // Weakness Heuristics
      // --------------------
      
      // Heuristic: Weaker for each opponent pawn and king the player still has on the board
      strength -=  3 * curGame.GetPawnCount(opponent);
      strength -= 10 * curGame.GetKingCount(opponent);
      
      // Heuristic: Weaker if player was jumped
      strength -= 2 * ((initGame.GetPawnCount(player) - curGame.GetPawnCount(player)));
      strength -= 5 * (initGame.GetKingCount(player) - curGame.GetKingCount(player));
      
      // Return the difference of strengths
      return strength;
    }
  }
}
