using System;
using System.Drawing;

namespace Uberware.Gaming.Checkers
{
  public class CheckersPiece
  {
    
    private CheckersGame owner;
    private int player;
    private CheckersRank rank;
    private Point location;
    
    internal CheckersPiece (CheckersGame owner, int player, CheckersRank rank, Point location)
    {
      this.owner = owner;
      this.player = player;
      this.rank = rank;
      this.location = location;
    }
    
    public int Player
    { get { return player; } }
    
    public CheckersRank Rank
    { get { return rank; } }
    
    public Point Location
    { get { return location; } }
    
  }
}
