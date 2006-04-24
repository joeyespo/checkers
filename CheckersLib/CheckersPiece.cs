using System;
using System.Drawing;

namespace Uberware.Gaming.Checkers
{
  public enum CheckersRank
  {
    Pawn = 0,
    King = 1,
  }
  
  public enum CheckersDirection
  {
    Up   = 0,
    Down = 1,
  }
  
  public class CheckersPiece
  {
    private CheckersGame owner;
    private int player;
    private CheckersRank rank;
    private Point location;
    private bool inPlay;
    
    /// <summary>Creates a new generic CheckersPiece object that is not currently in play.</summary>
    /// <param name="player">The player index of the piece.</param>
    /// <param name="rank">The piece's rank.</param>
    public CheckersPiece (int player, CheckersRank rank) : this(null, player, rank, Point.Empty, false)
    {}
    /// <summary>Creates a new generic CheckersPiece object that is in play.</summary>
    /// <param name="player">The player index of the piece.</param>
    /// <param name="rank">The piece's rank.</param>
    /// <param name="location">The piece's location on the board.</param>
    public CheckersPiece (int player, CheckersRank rank, Point location) : this(null, player, rank, location, true)
    {}
    internal CheckersPiece (CheckersGame owner, int player, CheckersRank rank, Point location, bool inPlay)
    {
      if ((player < 1) || (player > 2)) throw new ArgumentOutOfRangeException("player", player, "Argument 'player' must be a valid player number (a number greater or equal to 1).");
      if ((location.X < 0) || (location.X >= CheckersGame.BoardSize.Width) || (location.Y < 0) || (location.Y >= CheckersGame.BoardSize.Height))
        throw new ArgumentOutOfRangeException("location", player, "Argument 'location' must be a valid position on the board.");
      this.owner = owner;
      this.player = player;
      this.rank = rank;
      this.location = location;
      this.inPlay = inPlay;
    }
    
    public CheckersGame Owner
    { get { return owner; } }
    
    public bool InPlay
    { get { return inPlay; } }
    
    public int Player
    { get { return player; } }
    
    /// <summary>
    /// The piece's primary direction relative to the point-of-view of the internal board.
    /// Note: if no direction, will return CheckersDirection.Up, therefore invalid for pieces without a valid player.
    /// </summary>
    public CheckersDirection Direction
    { get { return (( player != 1 )?( CheckersDirection.Down ):( CheckersDirection.Up )); } }
    
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
    
    /// <summary>Creates a duplicate Checkers piece object from an identical (possibly cloned) game.</summary>
    /// <returns>The new Checkers piece object.</returns>
    public CheckersPiece Clone (CheckersGame game)
    {
      CheckersPiece clonedPiece = game.PieceAt(Location);
      // Make sure piece exists
      if (clonedPiece == null) return null;
      // Make sure piece is equivalent
      if ((clonedPiece.Player != Player) || (clonedPiece.InPlay != InPlay) || (clonedPiece.Rank != Rank))
        return null;
      // Return cloned piece
      return clonedPiece;
    }
  }
}
