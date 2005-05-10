using System;
using System.Drawing;

namespace Uberware.Gaming.Checkers
{
  public enum CheckersRank
  {
    Pawn = 0,
    King = 1,
  }
  
  public class CheckersPiece
  {
    private CheckersGame owner;
    private int player;
    private CheckersRank rank;
    private Point location;
    private bool inPlay;
    
    internal CheckersPiece (CheckersGame owner, int player, CheckersRank rank, Point location)
    {
      if (owner == null) throw new ArgumentNullException("owner", "Argument 'owner' must not be null");
      if (player < 1) throw new ArgumentOutOfRangeException("player", player, "Argument 'player' must be a valid player number (a number greater or equal to 1)");
      this.owner = owner;
      this.player = player;
      this.rank = rank;
      this.location = location;
    }
    
    public CheckersGame Owner
    { get { return owner; } }
    
    public bool InPlay
    { get { return inPlay; } }
    
    public int Player
    { get { return player; } }
    
    public CheckersRank Rank
    { get { return rank; } }
    
    public Point Location
    { get { return location; } }
    
    
    internal void Moved (Point location)
    { this.location = location; }
    
    internal void Promoted ()
    { rank = CheckersRank.King; }
    
    internal void RemovedFromPlay ()
    {
      location = Point.Empty;
      inPlay = false;
    }
  }
}
