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
    public static readonly Rectangle BoardBounds = new Rectangle(0, 0, BoardSize.Width-1, BoardSize.Height-1);
    
    public event EventHandler GameStarted;
    public event EventHandler GameStopped;
    public event EventHandler TurnChanged;
    public event EventHandler WinnerDeclared;
    
    private bool optionalJumping;
    
    private bool isReadOnly;
    private bool isPlaying;
    private int firstMove;
    private int turn;
    private int winner;
    private CheckersPieceCollection pieces;
    private CheckersPiece [,] board;
    private CheckersMove lastMove;
    
    public CheckersGame () : this(false)
    {}
    public CheckersGame (bool optionalJumping)
    {
      // Game rules
      this.optionalJumping = optionalJumping;
      
      // Initialize variables
      isReadOnly = false;
      pieces = new CheckersPieceCollection();
      board = new CheckersPiece[BoardSize.Width, BoardSize.Height];
      firstMove = 1;
      Stop();
    }
    
    /// <summary>Creates a Checkers game from supplied game parameters.</summary>
    /// <param name="optionalJumping">The Optional Jumping rule.</param>
    /// <param name="board">The Checkers board that makes up the game.</param>
    /// <param name="turn">Whose turn it is.</param>
    /// <returns>The Checkers game.</returns>
    public static CheckersGame Create (bool optionalJumping, CheckersPiece [,] board, int turn)
    { return Create(optionalJumping, board, turn, 0); }
    /// <summary>Creates a Checkers game from supplied game parameters.</summary>
    /// <param name="optionalJumping">The Optional Jumping rule.</param>
    /// <param name="board">The Checkers board that makes up the game.</param>
    /// <param name="turn">Whose turn it is.</param>
    /// <param name="winner">The winner, or 0 if none yet.</param>
    /// <returns>The Checkers game.</returns>
    public static CheckersGame Create (bool optionalJumping, CheckersPiece [,] board, int turn, int winner)
    {
      if ((board.GetLength(0) != BoardSize.Width) || (board.GetLength(1) != BoardSize.Height))
        throw new ArgumentOutOfRangeException("board", board, "Board's dimensions must be " + BoardSize.Width + "x" + BoardSize.Height);
      CheckersGame game = new CheckersGame(optionalJumping);
      game.board = new CheckersPiece[BoardSize.Width, BoardSize.Height];
      game.pieces = new CheckersPieceCollection();
      for (int y = 0; y < BoardSize.Height; y++)
        for (int x = 0; x < BoardSize.Width; x++)
        {
          CheckersPiece piece = board[x, y];
          if (piece == null) continue;
          if (piece.Owner != null) throw new ArgumentOutOfRangeException("board", board, "Board contains a Checkers piece that belongs to another Checkers game.");
          if (!piece.InPlay) throw new ArgumentOutOfRangeException("board", board, "Board contains a Checkers piece that is not in play.");
          if ((piece.Location.X != x) || (piece.Location.Y != y)) throw new ArgumentOutOfRangeException("board", board, "Board contains a Checkers piece that does not match up with it's board location.");
          if ((piece.Player != 1) || (piece.Player != 2)) throw new ArgumentOutOfRangeException("board", board, "Board contains a Checkers piece that is not associated with a valid player.");
          game.pieces.Add(new CheckersPiece(game, piece.Player, piece.Rank, piece.Location, piece.InPlay));
        }
      game.isPlaying = true;
      game.turn = turn;
      game.winner = winner;
      return game;
    }
    
    /// <summary>Returns a read-only game from the original game.</summary>
    /// <param name="game">The game to copy and make read-only.</param>
    /// <returns>The read-only copy.</returns>
    public static CheckersGame MakeReadOnly (CheckersGame game)
    {
      CheckersGame result = game.Clone();
      result.isReadOnly = true;
      return result;
    }
    
    #region Public Properties
    
    /// <summary>Gets or sets whether or not jumping is optional.</summary>
    public bool OptionalJumping
    {
      get { return optionalJumping; }
      set { if (isPlaying) throw new InvalidOperationException("Cannot change game rules while game is in play."); optionalJumping = value; }
    }
    
    /// <summary>Gets whether or not the game is currently in play.</summary>
    public bool IsPlaying
    { get { return isPlaying; } }
    
    /// <summary>Gets whether or not the game is read only.</summary>
    public bool IsReadOnly
    { get { return isReadOnly; } }
    
    /// <summary>Gets the current player turn. (Note: this is 0 if game is not in play)</summary>
    public int Turn
    { get { return turn; } }
    
    /// <summary>Gets the current player winner.</summary>
    public int Winner
    { get { return winner; } }
    
    /// <summary>Gets a list of all in play pieces.</summary>
    public CheckersPiece [] Pieces
    { get { return pieces.ToArray(); } }
    
    /// <summary>Gets the board matrix.</summary>
    public CheckersPiece [,] Board
    { get { return (CheckersPiece [,])board.Clone(); } }
    
    /// <summary>Gets or sets the player to have the first move.</summary>
    public int FirstMove
    {
      get { return firstMove; }
      set { if ((value < 1) || (value > 2)) throw new ArgumentOutOfRangeException("player", value, "First move must refer to a valid player number."); firstMove = value; }
    }
    
    /// <summary>Gets the last moved piece.</summary>
    public CheckersMove LastMove
    { get { return lastMove; } }
    
    #endregion
    
    /// <summary>Creates a duplicate Checkers game object.</summary>
    /// <returns>The new Checkers move object.</returns>
    public CheckersGame Clone ()
    {
      CheckersGame game = new CheckersGame(optionalJumping);
      game.isReadOnly = isReadOnly;
      game.isPlaying = isPlaying;
      game.firstMove = firstMove;
      game.turn = turn;
      game.winner = winner;
      game.pieces = new CheckersPieceCollection();
      game.board = new CheckersPiece[BoardSize.Width, BoardSize.Height];
      foreach (CheckersPiece piece in pieces)
      {
        CheckersPiece newPiece = new CheckersPiece(game, piece.Player, piece.Rank, piece.Location, piece.InPlay);
        game.board[newPiece.Location.X, newPiece.Location.Y] = newPiece;
        game.pieces.Add(newPiece);
      }
      int lastMovePieceIndex = (( lastMove != null )?( pieces.IndexOf(lastMove.Piece) ):( -1 ));
      game.lastMove = (( lastMovePieceIndex != -1 )?( CheckersMove.FromPath(game, game.pieces[lastMovePieceIndex], lastMove.Path) ):( null ));
      return game;
    }
    
    /// <summary>Begins the checkers game.</summary>
    public void Play ()
    {
      if (isReadOnly) throw new InvalidOperationException("Game is read only.");
      if (isPlaying) throw new InvalidOperationException("Game has already started.");
      Stop();
      isPlaying = true;
      
      for (int y = BoardSize.Height-1; y >= 5; y--)
      {
        for (int x = 0; x < BoardSize.Width; x++)
        {
          if ((x % 2) == (y % 2)) continue;
          CheckersPiece piece = new CheckersPiece(this, 1, CheckersRank.Pawn, new Point(x, y), true);
          board[x, y] = piece;
          pieces.Add(piece);
        }
      }
      for (int y = 0; y < 3; y++)
      {
        for (int x = 0; x < BoardSize.Width; x++)
        {
          if ((x % 2) == (y % 2)) continue;
          CheckersPiece piece = new CheckersPiece(this, 2, CheckersRank.Pawn, new Point(x, y), true);
          board[x, y] = piece;
          pieces.Add(piece);
        }
      }
      
      // Set player's turn
      turn = firstMove;
      lastMove = null;
      if (GameStarted != null) GameStarted(this, EventArgs.Empty);
    }
    
    /// <summary>Stops a decided game or forces a game-in-progress to stop prematurely with no winner.</summary>
    public void Stop ()
    {
      if (isReadOnly) throw new InvalidOperationException("Game is read only.");
      isPlaying = false;
      pieces.Clear();
      for (int y = 0; y < BoardSize.Height; y++)
        for (int x = 0; x < BoardSize.Width; x++)
          board[x, y] = null;
      lastMove = null;
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
    /// <summary>Gets the total number of pieces that are remaining in the game.</summary>
    /// <returns>The total number of pieces that are remaining in the game.</returns>
    /// <remarks>This is the same value as: Pieces.Length</remarks>
    public int GetRemainingCount ()
    { return pieces.Count; }
    
    /// <summary>Gets the number of player's pieces that have been jumped by the opponent.</summary>
    /// <param name="player">The player index to get the count.</param>
    /// <returns>The number of pieces that have been jumped by the opponent.</returns>
    public int GetJumpedCount (int player)
    { return PiecesPerPlayer - GetRemainingCount(player); }
    /// <summary>Gets the total number pieces that have been jumped in the game.</summary>
    /// <returns>The total number of pieces that have been jumped in the game.</returns>
    /// <remarks>This is the same value as: 2*PiecesPerPlayer - Pieces.Length</remarks>
    public int GetJumpedCount ()
    { return 2*PiecesPerPlayer - pieces.Count; }
    
    /// <summary>Gets the number of player's kings.</summary>
    /// <param name="player">The player index to get the count.</param>
    /// <returns>The number of king pieces.</returns>
    public int GetKingCount (int player)
    {
      if ((!isPlaying) && (winner == 0)) throw new InvalidOperationException("Operation requires game to be playing.");
      if ((player < 1) || (player > 2)) throw new ArgumentOutOfRangeException("player", player, "Argument 'player' must refer to a valid player number.");
      int count = 0;
      foreach (CheckersPiece piece in pieces)
        if ((piece.Player == player) && (piece.Rank == CheckersRank.King)) count++;
      return count;
    }
    /// <summary>Gets the total number of kings in the game.</summary>
    /// <returns>The number of king pieces.</returns>
    public int GetKingCount ()
    {
      if ((!isPlaying) && (winner == 0)) throw new InvalidOperationException("Operation requires game to be playing.");
      int count = 0;
      foreach (CheckersPiece piece in pieces)
        if (piece.Rank == CheckersRank.King) count++;
      return count;
    }
    
    /// <summary>Gets the number of player's pawns.</summary>
    /// <param name="player">The player index to get the count.</param>
    /// <returns>The number of pawn pieces.</returns>
    public int GetPawnCount (int player)
    {
      if ((!isPlaying) && (winner == 0)) throw new InvalidOperationException("Operation requires game to be playing.");
      if ((player < 1) || (player > 2)) throw new ArgumentOutOfRangeException("player", player, "Argument 'player' must refer to a valid player number.");
      int count = 0;
      foreach (CheckersPiece piece in pieces)
        if ((piece.Player == player) && (piece.Rank == CheckersRank.Pawn)) count++;
      return count;
    }
    /// <summary>Gets the total number of pawns in the game.</summary>
    /// <returns>The number of pawn pieces.</returns>
    public int GetPawnCount ()
    {
      if ((!isPlaying) && (winner == 0)) throw new InvalidOperationException("Operation requires game to be playing.");
      int count = 0;
      foreach (CheckersPiece piece in pieces)
        if (piece.Rank == CheckersRank.Pawn) count++;
      return count;
    }
    
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
        CheckersMove move = new CheckersMove(this, piece, false);
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
        CheckersMove move = new CheckersMove(this, piece, false);
        if (move.EnumMoves(optionalJumping).Length != 0) movable.Add(piece);
      }
      return (CheckersPiece [])movable.ToArray(typeof(CheckersPiece));
    }
    
    /// <summary>Returns a list of legal moves from all pieces this turn.</summary>
    /// <returns>A list of legal moves.</returns>
    public CheckersMove [] EnumLegalMoves ()
    {
      if ((!isPlaying) && (winner == 0)) throw new InvalidOperationException("Operation requires game to be playing.");
      Stack incompleteMoves = new Stack();
      ArrayList moves = new ArrayList();
      foreach (CheckersPiece piece in EnumMovablePieces())
        incompleteMoves.Push(BeginMove(piece));
      while (incompleteMoves.Count > 0)
      {
        CheckersMove move = (CheckersMove)incompleteMoves.Pop();
        foreach (Point location in move.EnumMoves())
        {
          CheckersMove nextMove = move.Clone();
          if (!nextMove.Move(location)) continue;
          if (nextMove.CanMove)
            incompleteMoves.Push(nextMove);
          if (!nextMove.MustMove)
            moves.Add(nextMove);
        }
      }
      return (CheckersMove [])moves.ToArray(typeof(CheckersMove));
    }
    /// <summary>Returns a list of legal moves from all pieces this turn.</summary>
    /// <param name="optionalJumping">Overrides the game's OptionalJumping parameter for the enumeration.</param>
    /// <returns>A list of legal moves.</returns>
    public CheckersMove [] EnumLegalMoves (bool optionalJumping)
    {
      if ((!isPlaying) && (winner == 0)) throw new InvalidOperationException("Operation requires game to be playing.");
      Stack incompleteMoves = new Stack();
      ArrayList moves = new ArrayList();
      foreach (CheckersPiece piece in EnumMovablePieces(optionalJumping))
        incompleteMoves.Push(BeginMove(piece));
      while (incompleteMoves.Count > 0)
      {
        CheckersMove move = (CheckersMove)incompleteMoves.Pop();
        foreach (Point location in move.EnumMoves(optionalJumping))
        {
          CheckersMove nextMove = move.Clone();
          if (!nextMove.Move(location)) continue;
          if (nextMove.CanMove)
            incompleteMoves.Push(nextMove);
          if (!nextMove.MustMove)
            moves.Add(nextMove);
        }
      }
      return (CheckersMove [])moves.ToArray(typeof(CheckersMove));
    }
    
    /// <summary>Returns whether or not a move is valid.</summary>
    /// <param name="move">The CheckersMove object to check.</param>
    /// <returns>True if the move is valid.</returns>
    public bool IsValidMove (CheckersMove move)
    {
      if (move == null) return false;
      return IsValidMove(move.Piece, move.Path);
    }
    /// <summary>Returns whether or not a move is valid.</summary>
    /// <param name="move">The CheckersMove object to check.</param>
    /// <returns>True if the move is valid.</returns>
    public bool IsValidMove (CheckersPiece piece, Point [] path)
    {
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      return (IsMoveValidCore(piece, path) != null);
    }
    private CheckersMove IsMoveValidCore (CheckersPiece piece, Point [] path)
    {
      // Failsafe .. be sure piece is valid
      if (piece == null) throw new ArgumentNullException("piece");
      if (!pieces.Contains(piece)) throw new ArgumentException("Argument 'piece' must be a piece in play to the current game.");
      // Be sure piece can be moved
      if (!CanMovePiece(piece)) throw new ArgumentException("Checkers piece cannot be moved on this turn.", "piece");
      CheckersMove move = CheckersMove.FromPath(this, piece, path);
      if (!move.Moved) return null;         // No movement
      if (move.MustMove) return null;       // A move must yet be made
      // Success
      return move;
    }
    
    /// <summary>Moves a Checkers piece on the board.</summary>
    /// <param name="move">The movement object to which the piece will move to.</param>
    /// <returns>True if the piece was moved successfully.</returns>
    public bool MovePiece (CheckersMove move)
    {
      if (isReadOnly) throw new InvalidOperationException("Game is read only.");
      if (move == null) return false;
      return MovePiece(move.Piece, move.Path);
    }
    /// <summary>Moves a Checkers piece on the board.</summary>
    /// <param name="piece">The checkers piece to move.</param>
    /// <param name="path">The the location for the piece to be moved to, and the path taken to get there.</param>
    /// <returns>True if the piece was moved successfully.</returns>
    public bool MovePiece (CheckersPiece piece, Point [] path)
    {
      if (isReadOnly) throw new InvalidOperationException("Game is read only.");
      if (!isPlaying) throw new InvalidOperationException("Operation requires game to be playing.");
      // Check for valid move
      CheckersMove move = IsMoveValidCore(piece, path);
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
      // King a pawn if reached other end of board
      if (move.Kinged) piece.Promoted();
      // Remember last move
      lastMove = move;
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
    
    /// <summary>Begins a move by creating a CheckersMove object to assist in generating a valid movement.</summary>
    /// <param name="piece">The Checkers piece to be moved.</param>
    /// <returns>The CheckersMove object.</returns>
    public CheckersMove BeginMove (CheckersPiece piece)
    {
      if ((!isPlaying) && (winner == 0)) throw new InvalidOperationException("Operation requires game to be playing.");
      // Failsafe .. be sure piece is valid
      if (piece == null) throw new ArgumentNullException("piece");
      if (!pieces.Contains(piece)) throw new ArgumentException("Argument 'piece' must be a piece in play to the current game.");
      // Be sure piece can be moved
      if (!CanMovePiece(piece)) throw new ArgumentException("Checkers piece cannot be moved on this turn.", "piece");
      return new CheckersMove(this, piece, true);
    }
    
    
    public void DeclareStalemate ()
    {
      if ((!isPlaying) && (winner == 0)) throw new InvalidOperationException("Operation requires game to be playing.");
      DeclareWinner(3);
    }
    
    // Declares the winner
    public void DeclareWinner (int winner)
    {
      if (isReadOnly) throw new InvalidOperationException("Game is read only.");
      this.winner = winner;
      isPlaying = false;
      turn = 0;
      if (WinnerDeclared != null) WinnerDeclared(this, EventArgs.Empty);
    }
  }
}
