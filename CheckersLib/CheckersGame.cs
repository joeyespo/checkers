using System;
using System.Drawing;
using System.Collections;

namespace Uberware.Gaming.Checkers
{
  public class CheckersGame
  {
    public static readonly int PlayerCount = 2;
    public static readonly Size BoardSize = new Size(8, 8);
    
    private bool optionalJumping;
    
    private bool isPlaying;
    private int firstMove;
    private int turn;
    private int winner;
    private CheckersPieceCollection pieces;
    private CheckersPiece [,] board;
    
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
      set { if ((value < 1) || (value > 2)) throw new ArgumentOutOfRangeException("value", value, "First move must refer to a player number."); firstMove = value; }
    }
    
    
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
    }
    
    /// <summary>Stops a decided game or forces a game-in-progress to stop prematurely with no winner.</summary>
    public void Stop ()
    {
      isPlaying = false;
      pieces.Clear();
      for (int y = 0; y < BoardSize.Height; y++)
        for (int x = 0; x < BoardSize.Width; x++)
          board[x, y] = null;
      // !!!!! Stop for good ??
      winner = 0;
      turn = 0;
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
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      if (piece == null) throw new ArgumentNullException("piece");
      if (!pieces.Contains(piece)) throw new ArgumentException("Argument 'piece' must be a piece in play to the current game.");
      return (piece.Player == turn);
    }
    
    /// <summary>Returns all pieces belonging to the specified player.</summary>
    /// <param name="player">The player index to get the list of pieces.</param>
    /// <returns>A list of pieces that belong to the specified player.</returns>
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
    
    /// <summary>Returns a list of movable pieces this turn.</summary>
    /// <returns>A list of pieces that can be moved this turn.</returns>
    public CheckersPiece [] EnumMovablePieces ()
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
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
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
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
      // !!!!!
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
      // Update player's turn
      int prevTurn = turn;
      if (++turn > PlayerCount) turn = 1;
      // Check for win by removal of opponent's pieces or by no turns available this turn
      if ((EnumPlayerPieces(prevTurn).Length == 0) || (EnumMovablePieces().Length == 0))
        DeclareWinner(prevTurn);
      return true;
      /*
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
      */
    }
    
    public CheckersMove BeginMove (CheckersPiece piece)
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
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
    }
    
    /* !!!!!
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
    */
    
    /* !!!!!
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
    */
    
    /* !!!!!
    /// <summary>Returns all valid moves relative to the passed piece.</summary>
    /// <param name="piece">The checkers piece whose moves are to be enumerated.</param>
    /// <returns>A list of pieces that can be moved this turn.</returns>
    public Point [] EnumMoves (CheckersPiece piece)
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      if (piece == null) throw new ArgumentNullException("piece");
      if (!pieces.Contains(piece)) throw new ArgumentException("Argument 'piece' must be a piece in play to the current game.");
      CheckersPiece [] jumped;
      return EnumStepMovesCore(piece, out jumped);
    }
    */
    
    /* !!!!!
    public bool BeginMove (CheckersPiece piece)
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      if (IsMoving) throw new InvalidOperationException("Cannot begin moving a Checkers piece when a piece is already being moved.");
      // Failsafe .. be sure piece is valid
      if (piece == null) throw new ArgumentNullException("piece");
      if (!pieces.Contains(piece)) throw new ArgumentException("Argument 'piece' must be a piece in play to the current game.");
      // Begin moving the piece (if able)
      if (!CanMovePiece(piece)) return false;
      moving = piece; startLocation = piece.Location;
      return true;
    }
    
    public bool StepMove (Point location)
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      if (!IsMoving) throw new InvalidOperationException("Action requires a piece to be in movement (call BeginMove first).");
      // Failsafe .. be sure location is valid
      if ((location.X < 0) || (location.X >= BoardSize.Width) || (location.Y < 0) || (location.Y >= BoardSize.Height))
        throw new ArgumentOutOfRangeException("location", location, "Argument 'location' must be within the range of the board.");
      if (!CanStepMove(location)) return false;
      // !!!!!
      return true;
    }
    public bool CanStepMove (Point location)
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      if (!IsMoving) throw new InvalidOperationException("Action requires a piece to be in movement (call BeginMove first).");
      foreach (Point p in EnumStepMoves()) if (p == location) return true;
      return false;
    }
    
    public Point [] EnumStepMoves ()
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      if (!IsMoving) throw new InvalidOperationException("Action requires a piece to be in movement (call BeginMove first).");
      CheckersPiece [] jumped;
      return EnumStepMovesCore(moving.Location, out jumped);
    }
    
    public bool EndMove ()
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      if (!IsMoving) throw new InvalidOperationException("Action requires a piece to be in movement (call BeginMove first).");
      // !!!!! Can end ??
      return true;
    }
    
    public bool CancelMove ()
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      if (!IsMoving) throw new InvalidOperationException("Action requires a piece to be in movement (call BeginMove first).");
      if (startLocation != moving.Location) return false;
      moving = null;
      startLocation = Point.Empty;
      return false;
    }
    
    private Point [] EnumStepMovesCore (Point fromLocation, out CheckersPiece [] jumped)
    {
      // Enumerate next step moves
      ArrayList moves = new ArrayList();
      ArrayList jumpedList = new ArrayList();
      // !!!!! rank (can only jump in one direction)
      // Append jumps
      if (InBounds(fromLocation.X-1, fromLocation.Y-1) && (board[fromLocation.X-1, fromLocation.Y-1] != null))
        if (InBounds(fromLocation.X-2, fromLocation.Y-2) && (board[fromLocation.X-2, fromLocation.Y-2] == null))
        { moves.Add(new Point(fromLocation.X-2, fromLocation.Y-2)); jumpedList.Add(board[fromLocation.X-1, fromLocation.Y-1]); }
      if (InBounds(fromLocation.X+1, fromLocation.Y-1) && (board[fromLocation.X+1, fromLocation.Y-1] != null))
        if (InBounds(fromLocation.X+2, fromLocation.Y-2) && (board[fromLocation.X+2, fromLocation.Y-2] == null))
        { moves.Add(new Point(fromLocation.X+2, fromLocation.Y-2)); jumpedList.Add(board[fromLocation.X-1, fromLocation.Y-1]); }
      if (InBounds(fromLocation.X-1, fromLocation.Y+1) && (board[fromLocation.X-1, fromLocation.Y+1] != null))
        if (InBounds(fromLocation.X-2, fromLocation.Y+2) && (board[fromLocation.X-2, fromLocation.Y+2] == null))
        { moves.Add(new Point(fromLocation.X-2, fromLocation.Y+2)); jumpedList.Add(board[fromLocation.X-1, fromLocation.Y-1]); }
      if (InBounds(fromLocation.X+1, fromLocation.Y+1) && (board[fromLocation.X+1, fromLocation.Y+1] != null))
        if (InBounds(fromLocation.X+2, fromLocation.Y+2) && (board[fromLocation.X+2, fromLocation.Y+2] == null))
        { moves.Add(new Point(fromLocation.X+2, fromLocation.Y+2)); jumpedList.Add(board[fromLocation.X-1, fromLocation.Y-1]); }
      // Append single-move moves in the enumeration (if able to)
      if ((startLocation == fromLocation) && ((optionalJumping) || (moves.Count == 0)))
      {
        if (InBounds(fromLocation.X-1, fromLocation.Y-1) && (board[fromLocation.X-1, fromLocation.Y-1] == null)) { moves.Add(new Point(fromLocation.X-1, fromLocation.Y-1)); jumpedList.Add(null); }
        if (InBounds(fromLocation.X+1, fromLocation.Y-1) && (board[fromLocation.X+1, fromLocation.Y-1] == null)) { moves.Add(new Point(fromLocation.X+1, fromLocation.Y-1)); jumpedList.Add(null); }
        if (InBounds(fromLocation.X-1, fromLocation.Y+1) && (board[fromLocation.X-1, fromLocation.Y+1] == null)) { moves.Add(new Point(fromLocation.X-1, fromLocation.Y+1)); jumpedList.Add(null); }
        if (InBounds(fromLocation.X+1, fromLocation.Y+1) && (board[fromLocation.X+1, fromLocation.Y+1] == null)) { moves.Add(new Point(fromLocation.X+1, fromLocation.Y+1)); jumpedList.Add(null); }
      }
      jumped = (CheckersPiece [])jumpedList.ToArray(typeof(CheckersPiece));
      return (Point [])moves.ToArray(typeof(Point));
    }
    */
    
    /* !!!!!
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
    */
  }
}
