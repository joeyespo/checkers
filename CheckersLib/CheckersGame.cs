using System;
using System.Drawing;
using System.Collections;

namespace Uberware.Gaming.Checkers
{
  public class CheckersGame
  {
    public static readonly int PiecesPerPlayer = 12;
    public static readonly int PlayerCount = 2;
    public static readonly Size BoardSize = new Size(8, 8);
    
    public event EventHandler GameStarted;
    public event EventHandler GameStopped;
    public event EventHandler TurnChanged;
    public event EventHandler WinnerDeclared;
    
    private bool optionalJumping;
    
    private bool isPlaying;
    private int firstMove;
    private int turn;
    private int winner;
    private CheckersPieceCollection pieces;
    private CheckersPiece [,] board;
    private CheckersPiece lastMoved;
    
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
    
    
    public bool OptionalJumping
    {
      get { return optionalJumping; }
      set { if (isPlaying) throw new InvalidOperationException("Cannot change game rules while game is in play."); optionalJumping = value; }
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
    { get { return (CheckersPiece [,])board.Clone(); } }
    
    public int FirstMove
    {
      get { return firstMove; }
      set { if ((value < 1) || (value > 2)) throw new ArgumentOutOfRangeException("player", value, "First move must refer to a valid player number."); firstMove = value; }
    }
    public CheckersPiece LastMoved
    { get { return lastMoved; } }
    
    
    /// <summary>Begins the checkers game.</summary>
    public void Play ()
    {
      if (isPlaying) throw new InvalidOperationException("Game has already started.");
      Stop();
      isPlaying = true;
      
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
      lastMoved = null;
      if (GameStarted != null) GameStarted(this, EventArgs.Empty);
    }
    
    /// <summary>Stops a decided game or forces a game-in-progress to stop prematurely with no winner.</summary>
    public void Stop ()
    {
      isPlaying = false;
      pieces.Clear();
      for (int y = 0; y < BoardSize.Height; y++)
        for (int x = 0; x < BoardSize.Width; x++)
          board[x, y] = null;
      lastMoved = null;
      winner = 0;
      turn = 0;
      if (GameStopped != null) GameStopped(this, EventArgs.Empty);
    }
    
    /// <summary>Returns whether or not piece is in board bounds.</summary>
    /// <param name="location">The location to test.</param>
    /// <returns>True if specified location is in bounds.</returns>
    public bool InBounds (Point location)
    { return InBounds(location.X, location.Y); }
    /// <summary>Returns whether or not piece is in board bounds.</summary>
    /// <param name="x">The x location to test.</param>
    /// <param name="y">The y location to test.</param>
    /// <returns>True if specified location is in bounds.</returns>
    public bool InBounds (int x, int y)
    { return ((x >= 0) && (x < BoardSize.Width) && (y >= 0) && (y < BoardSize.Height)); }
    
    /// <summary>Retrieves a piece at a particular location (or null if empty or out of bounds).</summary>
    public CheckersPiece PieceAt (int x, int y)
    {
      if ((!isPlaying) && (winner == 0)) throw new InvalidOperationException("Operation requires game to be playing.");
      if (!InBounds(x, y)) return null;
      return board[x, y];
    }
    /// <summary>Retrieves a piece at a particular location (or null if empty or out of bounds).</summary>
    public CheckersPiece PieceAt (Point location)
    { return PieceAt(location.X, location.Y); }
    
    /// <summary>Returns whether or not the checkers piece can be moved this turn.</summary>
    /// <param name="piece">The checkers piece to test.</param>
    /// <returns>True when piece can be moved.</returns>
    public bool CanMovePiece (CheckersPiece piece)
    {
      if ((!isPlaying) && (winner == 0)) throw new InvalidOperationException("Operation requires game to be playing.");
      if (piece == null) throw new ArgumentNullException("piece");
      if (!pieces.Contains(piece)) throw new ArgumentException("Argument 'piece' must be a piece in play to the current game.");
      return (piece.Player == turn);
    }
    
    /// <summary>Gets the number of player's pieces that are remaining.</summary>
    /// <param name="player">The player index to get the count.</param>
    /// <returns>The number of pieces that are remaining.</returns>
    public int GetRemainingCount (int player)
    {
      if ((!isPlaying) && (winner == 0)) throw new InvalidOperationException("Operation requires game to be playing.");
      if ((player < 1) || (player > 2)) throw new ArgumentOutOfRangeException("player", player, "Argument 'player' must refer to a valid player number.");
      int count = 0;
      foreach (CheckersPiece piece in pieces)
        if (piece.Player == player) count++;
      return count;
    }
    /// <summary>Gets the number of player's pieces that have been jumped by the opponent.</summary>
    /// <param name="player">The player index to get the count.</param>
    /// <returns>The number of pieces that have been jumped by the opponent.</returns>
    public int GetJumpedCount (int player)
    { return (PiecesPerPlayer - GetRemainingCount(player)); }
    
    /// <summary>Returns all pieces belonging to the specified player.</summary>
    /// <param name="player">The player index to get the list of pieces.</param>
    /// <returns>A list of pieces that belong to the specified player.</returns>
    public CheckersPiece [] EnumPlayerPieces (int player)
    {
      if ((!isPlaying) && (winner == 0)) throw new InvalidOperationException("Operation requires game to be playing.");
      if ((player < 1) || (player > 2)) throw new ArgumentOutOfRangeException("player", player, "Argument 'player' must refer to a valid player number.");
      ArrayList playerPieces = new ArrayList();
      foreach (CheckersPiece piece in pieces)
      {
        if (piece.Player != player) continue;
        playerPieces.Add(piece);
      }
      return (CheckersPiece [])playerPieces.ToArray(typeof(CheckersPiece));
    }
    
    /// <summary>Returns a list of movable pieces this turn.</summary>
    /// <returns>A list of pieces that can be moved this turn.</returns>
    public CheckersPiece [] EnumMovablePieces ()
    {
      if ((!isPlaying) && (winner == 0)) throw new InvalidOperationException("Operation requires game to be playing.");
      ArrayList movable = new ArrayList();
      foreach (CheckersPiece piece in EnumPlayerPieces(turn))
      {
        CheckersMove move = new CheckersMove(this, piece);
        if (move.CanMove) movable.Add(piece);
      }
      return (CheckersPiece [])movable.ToArray(typeof(CheckersPiece));
    }
    /// <summary>Returns a list of movable pieces this turn.</summary>
    /// <param name="optionalJumping">Overrides the game's OptionalJumping parameter for the enumeration.</param>
    /// <returns>A list of pieces that can be moved this turn.</returns>
    public CheckersPiece [] EnumMovablePieces (bool optionalJumping)
    {
      if ((!isPlaying) && (winner == 0)) throw new InvalidOperationException("Operation requires game to be playing.");
      ArrayList movable = new ArrayList();
      foreach (CheckersPiece piece in EnumPlayerPieces(turn))
      {
        CheckersMove move = new CheckersMove(this, piece);
        if (move.EnumMoves(optionalJumping).Length != 0) movable.Add(piece);
      }
      return (CheckersPiece [])movable.ToArray(typeof(CheckersPiece));
    }
    
    /// <summary>Moves a Checkers piece on the board.</summary>
    /// <param name="move">The movement object to which the piece will move to.</param>
    /// <returns>True if the piece was moved successfully.</returns>
    public bool MovePiece (CheckersMove move)
    {
      if (move == null) return false;
      return MovePiece(move.Piece, move.Path);
    }
    /// <summary>Moves a Checkers piece on the board.</summary>
    /// <param name="piece">The checkers piece to move.</param>
    /// <param name="path">The the location for the piece to be moved to, and the path taken to get there.</param>
    /// <returns>True if the piece was moved successfully.</returns>
    public bool MovePiece (CheckersPiece piece, Point [] path)
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      // Failsafe .. be sure piece is valid
      if (piece == null) throw new ArgumentNullException("piece");
      if (!pieces.Contains(piece)) throw new ArgumentException("Argument 'piece' must be a piece in play to the current game.");
      // Be sure piece can be moved
      if (!CanMovePiece(piece)) throw new ArgumentException("Checkers piece cannot be moved on this turn.", "piece");
      CheckersMove move = CheckersMove.FromPath(this, piece, path);
      if (!move.Moved) return false;        // No movement
      if (move.MustMove) return false;      // A move must yet be made
      // Remove jumped pieces
      foreach (CheckersPiece jumped in move.Jumped)
      {
        if (board[jumped.Location.X, jumped.Location.Y] == jumped) board[jumped.Location.X, jumped.Location.Y] = null;
        pieces.Remove(jumped); jumped.RemovedFromPlay();
      }
      // Move the piece on board
      board[piece.Location.X, piece.Location.Y] = null;
      board[move.CurrentLocation.X, move.CurrentLocation.Y] = piece;
      piece.Moved(move.CurrentLocation);
      lastMoved = piece;
      // King a pawn if reached other end of board
      if (piece.Rank == CheckersRank.Pawn)
      {
        if (((piece.Player == 1) && (piece.Location.Y == 0)) || ((piece.Player == 2) && (piece.Location.Y == BoardSize.Height-1)))
          piece.Promoted();
      }
      // Update player's turn
      int prevTurn = turn;
      if (++turn > PlayerCount) turn = 1;
      // Check for win by removal of opponent's pieces or by no turns available this turn
      if ((EnumPlayerPieces(prevTurn).Length == 0) || (EnumMovablePieces().Length == 0))
        DeclareWinner(prevTurn);
      else
        if (TurnChanged != null) TurnChanged(this, EventArgs.Empty);
      return true;
    }
    
    public CheckersMove BeginMove (CheckersPiece piece)
    {
      if ((!isPlaying) && (winner == 0)) throw new InvalidOperationException("Operation requires game to be playing.");
      // Failsafe .. be sure piece is valid
      if (piece == null) throw new ArgumentNullException("piece");
      if (!pieces.Contains(piece)) throw new ArgumentException("Argument 'piece' must be a piece in play to the current game.");
      // Be sure piece can be moved
      if (!CanMovePiece(piece)) throw new ArgumentException("Checkers piece cannot be moved on this turn.", "piece");
      return new CheckersMove(this, piece);
    }
    
    
    // Declares the winner
    private void DeclareWinner (int winner)
    {
      this.winner = winner;
      isPlaying = false;
      turn = 0;
      if (WinnerDeclared != null) WinnerDeclared(this, EventArgs.Empty);
    }
  }
}
