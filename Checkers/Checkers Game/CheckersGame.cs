using System;
using System.Drawing;

namespace Uberware.Gaming.Checkers
{
  public class CheckersGame
  {
    
    public static readonly int SquareCount = 8;
    
    public event EventHandler Started;
    public event EventHandler Stopped;
    public event EventHandler CurrentPlayerChanged;
    
    private bool isPlaying;
    private int currentPlayer;
    private int winner;
    private CheckersPieceCollection pieces = new CheckersPieceCollection();
    
    public CheckersGame ()
    {
      isPlaying = false;
      currentPlayer = 0;
      winner = 0;
    }
    
    
    public bool IsPlaying
    { get { return isPlaying; } }
    
    public int CurrentPlayer
    { get { return currentPlayer; } }
    
    public int Winner
    { get { return winner; } }
    
    public CheckersPieceCollection Pieces
    { get { return pieces; } }
    
    public CheckersPiece GetPiece (Point location)
    {
      foreach (CheckersPiece piece in pieces)
        if (piece.Location == location) return piece;
      return null;
    }
    
    
    public void Play ()
    {
      if (isPlaying) throw new InvalidOperationException("Game has already started.");
      isPlaying = true;
      
      pieces.Clear();
      for (int i = 0; i < 3; i++)
      {
        for (int x = 0; x < SquareCount; x++)
        {
          if ((x % 2) == (i % 2)) continue;
          pieces.Add(new CheckersPiece(this, 1, CheckersRank.Pawn, new Point(x, i)));
        }
      }
      for (int i = SquareCount-1; i >= 5; i--)
      {
        for (int x = 0; x < SquareCount; x++)
        {
          if ((x % 2) == (i % 2)) continue;
          pieces.Add(new CheckersPiece(this, 2, CheckersRank.Pawn, new Point(x, i)));
        }
      }
      
      if (Started != null) Started(this, EventArgs.Empty);
    }
    
    public void Stop ()
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      if (Stopped != null) Stopped(this, EventArgs.Empty);
    }
    
    public void Stalemate ()
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      // !!!!!
    }
    
    /// <summary> Determines whether or not the checkers piece can be moved this turn.</summary>
    /// <param name="piece">The checkers peice to test.</param>
    /// <returns>True when peice can be moved.</returns>
    public bool CanMovePiece (CheckersPiece piece)
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      if (piece == null) throw new ArgumentNullException("piece");
      if (!pieces.Contains(piece)) return false;
      if (piece.Player == currentPlayer) return false;
      return true;
    }
    
    /// <summary> Determines whether or not the checkers piece can be moved to the specified location this turn. </summary>
    /// <param name="piece">The checkers peice to test.</param>
    /// <param name="location">The location to test for valid movement.</param>
    /// <returns>True when peice can be moved to specified location.</returns>
    public bool CanMovePiece (CheckersPiece piece, Point location)
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      if (!CanMovePiece(piece)) return false;
      // !!!!!
      return false;
    }
    
    public bool MovePiece (CheckersPiece piece, Point location)
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      if (!CanMovePiece(piece, location)) return false;
      // !!!!! Move the piece, and remove any jumped pieces
      // !!!!! Take most complicated route ??
      return true;
    }
    
  }
}
