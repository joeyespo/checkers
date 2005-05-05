using System;
using System.Drawing;
using System.Collections;

namespace Uberware.Gaming.Checkers
{
  public class CheckersGame
  {
    public static readonly int PlayerCount = 2;
    public static readonly Size BoardSize = new Size(8, 8);
    
    private bool isPlaying;
    private int firstMove;
    private int turn;
    private int winner;
    private CheckersPieceCollection pieces;
    private CheckersPiece [,] board;
    private bool optionalJumping;
    
    public CheckersGame () : this(false)
    {}
    public CheckersGame (bool optionalJumping)
    {
      this.optionalJumping = optionalJumping;
      pieces = new CheckersPieceCollection();
      board = new CheckersPiece[BoardSize.Width, BoardSize.Height];
      firstMove = 1;
      Stop();
    }
    
    
    public bool IsPlaying
    { get { return isPlaying; } }
    
    public int Turn
    { get { return turn; } }
    
    public int Winner
    { get { return winner; } }
    
    public CheckersPiece [] Pieces
    { get { return pieces.ToArray(); } }
    
    public CheckersPiece [,] Board
    { get { return board; } }
    
    public int FirstMove
    {
      get { return firstMove; }
      set { if ((value < 1) || (value > 2)) throw new ArgumentOutOfRangeException("value", value, "First move must refer to a player number."); firstMove = value; }
    }
    
    
    /// <summary>Begins the checkers game.</summary>
    public void Play ()
    {
      if (isPlaying) throw new InvalidOperationException("Game has already started.");
      isPlaying = true;
      
      pieces.Clear();
      for (int y = BoardSize.Height-1; y >= 5; y--)
      {
        for (int x = 0; x < BoardSize.Width; x++)
        {
          if ((x % 2) == (y % 2)) continue;
          CheckersPiece piece = new CheckersPiece(this, 1, CheckersRank.Pawn, new Point(x, y));
          board[x, y] = piece;
          pieces.Add(piece);
        }
      }
      for (int y = 0; y < 3; y++)
      {
        for (int x = 0; x < BoardSize.Width; x++)
        {
          if ((x % 2) == (y % 2)) continue;
          CheckersPiece piece = new CheckersPiece(this, 2, CheckersRank.Pawn, new Point(x, y));
          board[x, y] = piece;
          pieces.Add(piece);
        }
      }
      
      // Set player's turn
      turn = firstMove;
    }
    
    /// <summary>Stops a decided game or forces a game-in-progress to stop prematurely with no winner.</summary>
    public void Stop ()
    {
      isPlaying = false;
      pieces.Clear();
      for (int y = 0; y < BoardSize.Height; y++)
        for (int x = 0; x < BoardSize.Width; x++)
          board[x, y] = null;
      winner = 0;
      turn = 0;
    }
    
    /// <summary>Returns whether or not piece is in board bounds.</summary>
    private bool InBounds (Point location)
    { return InBounds(location.X, location.Y); }
    private bool InBounds (int x, int y)
    { return ((x < 0) || (x >= BoardSize.Width) || (y < 0) || (y >= BoardSize.Height)); }
    
    /// <summary>Retrieves a piece at a particular location (or null if empty or out of bounds).</summary>
    public CheckersPiece PieceAt (int x, int y)
    {
      if (!InBounds(x, y)) return null;
      return board[x, y];
    }
    
    /// <summary>Returns whether or not the checkers piece can be moved this turn.</summary>
    /// <param name="piece">The checkers piece to test.</param>
    /// <returns>True when piece can be moved.</returns>
    public bool CanMovePiece (CheckersPiece piece)
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      if (piece == null) throw new ArgumentNullException("piece");
      if (!pieces.Contains(piece)) throw new ArgumentException("Argument 'piece' must be a piece in play to the current game.");
      return (piece.Player == turn);
    }
    
    /// <summary>Returns whether or not the checkers piece can be moved to the specified location this turn.</summary>
    /// <param name="piece">The checkers piece to test.</param>
    /// <param name="location">The location to test for valid movement.</param>
    /// <returns>True when piece can be moved to specified location.</returns>
    public bool IsValidMove (CheckersPiece piece, Point location)
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      // Failsafe .. be sure piece is valid
      if (piece == null) throw new ArgumentNullException("piece");
      if (!pieces.Contains(piece)) throw new ArgumentException("Argument 'piece' must be a piece in play to the current game.");
      // Failsafe .. be sure location is valid
      if ((location.X < 0) || (location.X >= BoardSize.Width) || (location.Y < 0) || (location.Y >= BoardSize.Height))
        throw new ArgumentOutOfRangeException("location", location, "Argument 'location' must be within the range of the board.");
      // Be sure piece can be moved
      // !!!!! ?? if (!CanMovePiece(piece)) return null;
      // Return whether or not move is valid
      return (CalculateMove(piece, location) != null);  // !!!!!
    }
    
    /// <summary>Returns all pieces belonging to the specified player.</summary>
    /// <param name="player">The player index to get the list of pieces.</param>
    /// <returns>A list of pieces that can be moved this turn.</returns>
    public CheckersPiece [] EnumPlayerPieces (int player)
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      ArrayList playerPieces = new ArrayList();
      foreach (CheckersPiece piece in pieces)
      {
        if (piece.Player != turn) continue;
        playerPieces.Add(piece);
      }
      return (CheckersPiece [])playerPieces.ToArray(typeof(CheckersPiece));
    }
    
    /// <summary>Returns all movable pieces for this turn.</summary>
    /// <returns>A list of pieces that can be moved this turn.</returns>
    public CheckersPiece [] EnumMovablePieces ()
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      ArrayList movablePieces = new ArrayList();
      foreach (CheckersPiece piece in pieces)
      {
        if (piece.Player != turn) continue;
        if (EnumMoves(piece).Length == 0) continue;
        movablePieces.Add(piece);
      }
      return (CheckersPiece [])movablePieces.ToArray(typeof(CheckersPiece));
    }
    
    /// <summary>Returns all valid moves relative to the passed piece.</summary>
    /// <param name="piece">The checkers piece whose moves are to be enumerated.</param>
    /// <returns>A list of pieces that can be moved this turn.</returns>
    public Point [] EnumMoves (CheckersPiece piece)
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      if (piece == null) throw new ArgumentNullException("piece");
      if (!pieces.Contains(piece)) throw new ArgumentException("Argument 'piece' must be a piece in play to the current game.");
      CheckersPiece [] jumped;
      return EnumMoves(piece, out jumped);
    }
    
    /// <summary>Returns whether or not the checkers piece can be moved to the specified location this turn.</summary>
    /// <param name="piece">The checkers piece to move.</param>
    /// <param name="location">The location for the piece to be moved to.</param>
    /// <returns>True if a piece was moved successfully.</returns>
    public bool MovePiece (CheckersPiece piece, Point location)
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      // Failsafe .. be sure piece is valid
      if (piece == null) throw new ArgumentNullException("piece");
      if (!pieces.Contains(piece)) throw new ArgumentException("Argument 'piece' must be a piece in play to the current game.");
      // Failsafe .. be sure location is valid
      if ((location.X < 0) || (location.X >= BoardSize.Width) || (location.Y < 0) || (location.Y >= BoardSize.Height))
        throw new ArgumentOutOfRangeException("location", location, "Argument 'location' must be within the range of the board.");
      // Be sure piece can be moved
      //if (!CanMovePiece(piece)) return null; !!!!! ??
      // Jump all jumped pieces
      CheckersPiece [] jumped = CalculateMove(piece, location); // !!!!!
      if (jumped == null) return false;     // Not a valid move
      foreach (CheckersPiece jumpedPiece in jumped)
      {
        if (board[jumpedPiece.Location.X, jumpedPiece.Location.Y] == jumpedPiece) board[jumpedPiece.Location.X, jumpedPiece.Location.Y] = null;
        pieces.Remove(jumpedPiece);
        jumpedPiece.RemovedFromPlay();
      }
      // Move the piece on board
      board[piece.Location.X, piece.Location.Y] = null;
      board[location.X, location.Y] = piece;
      piece.Moved(location);
      // Update player's turn
      int lastTurn = turn;
      if (++turn > PlayerCount) turn = 1;
      // Check for win by removal of opponent's pieces
      foreach (CheckersPiece p in pieces)
        if (p.Player != piece.Player)
        { DeclareWinner(lastTurn); return true; }
      // Check for win by no turns available
      if (EnumMovablePieces().Length == 0)
      { DeclareWinner(lastTurn); return true; }
      return true;
    }
    
    
    // Declares the winner
    private void DeclareWinner (int winner)
    {
      this.winner = winner;
      isPlaying = false;
    }
    
    private Point [] EnumMoves (CheckersPiece piece)
    {
      // Be sure piece can be moved
      if (!CanMovePiece(piece)) return new Point [0];
      return EnumMovesCore(board, piece.Location, false);
    }
    private Point [] EnumMovesCore (CheckersPiece [,] board, Point curLocation, bool hasJumped)
    {
      // !!!!! optionalJumping
      // Clone the board to avoid algorithm loops
      board = (CheckersPiece [,])board.Clone();
      // Check for jumps
      ArrayList jumpsNW = new ArrayList(), jumpsNE = new ArrayList(), jumpsSW = new ArrayList(), jumpsSE = new ArrayList();
      Point [] temp;
      if (InBounds(curLocation.X-1, curLocation.Y-1) && (board[curLocation.X-1, curLocation.Y-1] != null))
        if (InBounds(curLocation.X-2, curLocation.Y-2) && (board[curLocation.X-2, curLocation.Y-2] == null))
        {
          board[curLocation.X-1, curLocation.Y-1] = null;
          temp = EnumMovesCore(board, new Point(curLocation.X-2, curLocation.Y-2), out subJumped, true);
        }
      if (InBounds(curLocation.X+1, curLocation.Y-1) && (board[curLocation.X+1, curLocation.Y-1] != null))
        if (InBounds(curLocation.X+2, curLocation.Y-2) && (board[curLocation.X+2, curLocation.Y-2] == null))
        {
          board[curLocation.X+1, curLocation.Y-1] = null;
          temp = EnumMovesCore(board, new Point(curLocation.X+2, curLocation.Y-2), out jumped, true);
          if (temp != null) { jumpsNE.Add(board[curLocation.X+1, curLocation.Y-1]); jumpsNE.AddRange(temp); }
        }
      if (InBounds(curLocation.X-1, curLocation.Y+1) && (board[curLocation.X-1, curLocation.Y+1] != null))
        if (InBounds(curLocation.X-2, curLocation.Y+2) && (board[curLocation.X-2, curLocation.Y+2] == null))
        {
          board[curLocation.X-1, curLocation.Y+1] = null;
          temp = EnumMovesCore(board, new Point(curLocation.X-2, curLocation.Y+2), out jumped, true);
          if (temp != null) { jumpsSW.Add(board[curLocation.X-1, curLocation.Y+1]); jumpsSW.AddRange(temp); }
        }
      if (InBounds(curLocation.X+1, curLocation.Y+1) && (board[curLocation.X+1, curLocation.Y+1] != null))
        if (InBounds(curLocation.X+2, curLocation.Y+2) && (board[curLocation.X+2, curLocation.Y+2] == null))
        {
          board[curLocation.X+1, curLocation.Y+1] = null;
          temp = EnumMovesCore(board, new Point(curLocation.X+2, curLocation.Y+2), out jumped, true);
          if (temp != null) { jumpsSE.Add(board[curLocation.X+1, curLocation.Y+1]); jumpsSE.AddRange(temp); }
        }
      if ((jumpsNW.Count > 0) && (jumpsNW.Count > jumpsNE.Count) && (jumpsNW.Count > jumpsSW.Count) && (jumpsNW.Count > jumpsSE.Count)) return (CheckersPiece [])jumpsNW.ToArray(typeof(CheckersPiece));
      if ((jumpsNE.Count > 0) && (jumpsNE.Count > jumpsNW.Count) && (jumpsNE.Count > jumpsSW.Count) && (jumpsNE.Count > jumpsSE.Count)) return (CheckersPiece [])jumpsNE.ToArray(typeof(CheckersPiece));
      if ((jumpsSW.Count > 0) && (jumpsSW.Count > jumpsNW.Count) && (jumpsSW.Count > jumpsNE.Count) && (jumpsSW.Count > jumpsSE.Count)) return (CheckersPiece [])jumpsSW.ToArray(typeof(CheckersPiece));
      if ((jumpsSE.Count > 0) && (jumpsSE.Count > jumpsNW.Count) && (jumpsSE.Count > jumpsNE.Count) && (jumpsSE.Count > jumpsSW.Count)) return (CheckersPiece [])jumpsSE.ToArray(typeof(CheckersPiece));
      // Check for single moves (make sure single move leads to destination square)
      if (!hasJumped)
      {
        if (InBounds(curLocation.X-1, curLocation.Y-1) && (board[curLocation.X-1, curLocation.Y-1] == null))
          return (( (destLocation.X == curLocation.X-1) && (destLocation.Y == curLocation.Y-1) )?( new CheckersPiece [0] ):( null ));
        if (InBounds(curLocation.X+1, curLocation.Y-1) && (board[curLocation.X+1, curLocation.Y-1] == null))
          return (( (destLocation.X == curLocation.X+1) && (destLocation.Y == curLocation.Y-1) )?( new CheckersPiece [0] ):( null ));
        if (InBounds(curLocation.X-1, curLocation.Y+1) && (board[curLocation.X-1, curLocation.Y+1] == null))
          return (( (destLocation.X == curLocation.X-1) && (destLocation.Y == curLocation.Y+1) )?( new CheckersPiece [0] ):( null ));
        if (InBounds(curLocation.X+1, curLocation.Y+1) && (board[curLocation.X+1, curLocation.Y+1] == null))
          return (( (destLocation.X == curLocation.X+1) && (destLocation.Y == curLocation.Y+1) )?( new CheckersPiece [0] ):( null ));
      }
      // No success
      return null;
    }
    
    /*
    // Failed if returns null
    private CheckersPiece [] CalculateMove (CheckersPiece piece, Point location)
    {
      // Failsafe .. be sure piece is valid
      if (piece == null) throw new ArgumentNullException("piece");
      if (!pieces.Contains(piece)) throw new ArgumentException("Argument 'piece' must be a piece in play to the current game.");
      // Failsafe .. be sure location is valid
      if ((location.X < 0) || (location.X >= BoardSize.Width) || (location.Y < 0) || (location.Y >= BoardSize.Height))
        throw new ArgumentOutOfRangeException("location", location, "Argument 'location' must be within the range of the board.");
      
      // Be sure piece can be moved
      if (!CanMovePiece(piece)) return null;
      // Make sure square is empty
      if (board[location.X, location.Y] != null) return null;
      CalculateMoveProc(board, piece.Location, location);
      return null;
    }
    // Check from relative location (step)
    private CheckersPiece [] CalculateMoveProc (CheckersPiece [,] board, Point curLocation, Point destLocation)
    {
      // !!!!! optionalJumping
      board = (CheckersPiece [,])board.Clone();
      // Check for jumps
      ArrayList jumpsNW = new ArrayList(), jumpsNE = new ArrayList(), jumpsSW = new ArrayList(), jumpsSE = new ArrayList();
      CheckersPiece [] temp;
      if (InBounds(curLocation.X-1, curLocation.Y-1) && (board[curLocation.X-1, curLocation.Y-1] != null))
        if (InBounds(curLocation.X-2, curLocation.Y-2) && (board[curLocation.X-2, curLocation.Y-2] == null))
        {
          temp = CalculateMoveProc(board, new Point(curLocation.X-2, curLocation.Y-2), destLocation);
          if (temp != null) { jumpsNW.Add(board[curLocation.X-1, curLocation.Y-1]); jumpsNW.AddRange(temp); }
        }
      if (InBounds(curLocation.X+1, curLocation.Y-1) && (board[curLocation.X+1, curLocation.Y-1] != null))
        if (InBounds(curLocation.X+2, curLocation.Y-2) && (board[curLocation.X+2, curLocation.Y-2] == null))
        {
          temp = CalculateMoveProc(board, new Point(curLocation.X+2, curLocation.Y-2), destLocation);
          if (temp != null) { jumpsNE.Add(board[curLocation.X+1, curLocation.Y-1]); jumpsNE.AddRange(temp); }
        }
      if (InBounds(curLocation.X-1, curLocation.Y+1) && (board[curLocation.X-1, curLocation.Y+1] != null))
        if (InBounds(curLocation.X-2, curLocation.Y+2) && (board[curLocation.X-2, curLocation.Y+2] == null))
        {
          temp = CalculateMoveProc(board, new Point(curLocation.X-2, curLocation.Y+2), destLocation);
          if (temp != null) { jumpsSW.Add(board[curLocation.X-1, curLocation.Y+1]); jumpsSW.AddRange(temp); }
        }
      if (InBounds(curLocation.X+1, curLocation.Y+1) && (board[curLocation.X+1, curLocation.Y+1] != null))
        if (InBounds(curLocation.X+2, curLocation.Y+2) && (board[curLocation.X+2, curLocation.Y+2] == null))
        {
          temp = CalculateMoveProc(board, new Point(curLocation.X+2, curLocation.Y+2), destLocation);
          if (temp != null) { jumpsSE.Add(board[curLocation.X+1, curLocation.Y+1]); jumpsSE.AddRange(temp); }
        }
      if ((jumpsNW.Count > 0) && (jumpsNW.Count > jumpsNE.Count) && (jumpsNW.Count > jumpsSW.Count) && (jumpsNW.Count > jumpsSE.Count)) return (CheckersPiece [])jumpsNW.ToArray(typeof(CheckersPiece));
      if ((jumpsNE.Count > 0) && (jumpsNE.Count > jumpsNW.Count) && (jumpsNE.Count > jumpsSW.Count) && (jumpsNE.Count > jumpsSE.Count)) return (CheckersPiece [])jumpsNE.ToArray(typeof(CheckersPiece));
      if ((jumpsSW.Count > 0) && (jumpsSW.Count > jumpsNW.Count) && (jumpsSW.Count > jumpsNE.Count) && (jumpsSW.Count > jumpsSE.Count)) return (CheckersPiece [])jumpsSW.ToArray(typeof(CheckersPiece));
      if ((jumpsSE.Count > 0) && (jumpsSE.Count > jumpsNW.Count) && (jumpsSE.Count > jumpsNE.Count) && (jumpsSE.Count > jumpsSW.Count)) return (CheckersPiece [])jumpsSE.ToArray(typeof(CheckersPiece));
      // Check for single moves (make sure single move leads to destination square)
      if (InBounds(curLocation.X-1, curLocation.Y-1) && (board[curLocation.X-1, curLocation.Y-1] == null))
        return (( (destLocation.X == curLocation.X-1) && (destLocation.Y == curLocation.Y-1) )?( new CheckersPiece [0] ):( null ));
      if (InBounds(curLocation.X+1, curLocation.Y-1) && (board[curLocation.X+1, curLocation.Y-1] == null))
        return (( (destLocation.X == curLocation.X+1) && (destLocation.Y == curLocation.Y-1) )?( new CheckersPiece [0] ):( null ));
      if (InBounds(curLocation.X-1, curLocation.Y+1) && (board[curLocation.X-1, curLocation.Y+1] == null))
        return (( (destLocation.X == curLocation.X-1) && (destLocation.Y == curLocation.Y+1) )?( new CheckersPiece [0] ):( null ));
      if (InBounds(curLocation.X+1, curLocation.Y+1) && (board[curLocation.X+1, curLocation.Y+1] == null))
        return (( (destLocation.X == curLocation.X+1) && (destLocation.Y == curLocation.Y+1) )?( new CheckersPiece [0] ):( null ));
      // No success
      return null;
    }
    */
  }
}
