using System;
using System.Drawing;
using System.Collections;

namespace Uberware.Gaming.Checkers
{
  public class CheckersMove
  {
    private CheckersGame game;
    private CheckersGame initialGame;
    private CheckersPiece piece;
    private CheckersPiece initialPiece;
    private CheckersPiece [,] board;
    private Point currentLocation;
    private ArrayList jumped;
    private ArrayList path;
    private bool kinged;
    private bool cannotMove;
    
    // Will be create indirectly from a CheckersGame object via BeginMove
    internal CheckersMove (CheckersGame game, CheckersPiece piece, bool makeReadOnlyCopy)
    {
      this.game = game;
      this.piece = piece;
      initialGame = (( (makeReadOnlyCopy) && (!game.IsReadOnly) )?( CheckersGame.MakeReadOnly(game) ):( game ));
      initialPiece = initialGame.Pieces[Array.IndexOf(game.Pieces, piece)];
      board = (CheckersPiece [,])game.Board.Clone();
      currentLocation = piece.Location;
      jumped = new ArrayList();
      path = new ArrayList();
      cannotMove = false;
      kinged = false;
    }
    
    /// <summary>Creates a Checkers move object from a given path.</summary>
    /// <param name="game">The Checkers game to create the movement to.</param>
    /// <param name="piece">The Checkers piece which will be moving.</param>
    /// <param name="path">The path to move along.</param>
    /// <returns>The new Checkers move object.</returns>
    internal static CheckersMove FromPath (CheckersGame game, CheckersPiece piece, Point [] path)
    {
      CheckersMove move = new CheckersMove(game, piece, true);
      foreach (Point p in path)
        if (move.Move(p) == false) return null;
      return move;
    }
    
    #region Public Properties
    
    /// <summary>Gets the Checkers game to which the movement is taking place.</summary>
    public CheckersGame Game
    { get { return game; } }
    
    /// <summary>Gets the initial Checkers game to which the movement began.</summary>
    public CheckersGame InitialGame
    { get { return initialGame; } }
    
    /// <summary>Gets the Checkers piece to which is being moved.</summary>
    public CheckersPiece Piece
    { get { return piece; } }
    
    /// <summary>Gets the initial Checkers piece to which was moved.</summary>
    public CheckersPiece InitialPiece
    { get { return initialPiece; } }
    
    /// <summary>Gets the list of Checkers pieces that will be jumped if this path is taken.</summary>
    public CheckersPiece [] Jumped
    { get { return (CheckersPiece [])jumped.ToArray(typeof(CheckersPiece)); } }
    
    /// <summary>Gets whether or not the piece has been kinged as a result of the movement taken place.</summary>
    public bool Kinged
    { get { return kinged; } }
    
    /// <summary>Gets the current location of the movement.</summary>
    public Point CurrentLocation
    { get { return currentLocation; } }
    
    /// <summary>Gets the path of movement.</summary>
    public Point [] Path
    { get { return (Point [])path.ToArray(typeof(Point)); } }
    
    /// <summary>Gets whether or not a movement has taken place.</summary>
    public bool Moved
    { get { return (path.Count != 0); } }
    
    /// <summary>Gets whether or not a move is possible from this point.</summary>
    public bool CanMove
    { get { return (!cannotMove) && (EnumMoves().Length != 0); } }
    
    /// <summary>Gets whether or not a move is required from this point.</summary>
    public bool MustMove
    { get { return ((!Moved) || ((CanMove) && (!game.OptionalJumping))); } }
    
    #endregion
    
    #region Enumerations
    
    /// <summary>Enumerates all possible moves from this point.</summary>
    /// <returns>An array of points which represent valid moves.</returns>
    public Point [] EnumMoves ()
    {
      if (cannotMove) return new Point [0];
      CheckersPiece [] jumpArray;
      return EnumMovesCore(currentLocation, out jumpArray);
    }
    /// <summary>Enumerates all possible moves from this point.</summary>
    /// <param name="optionalJumping">Overrides the game's OptionalJumping parameter for the enumeration.</param>
    /// <returns>An array of points which represent valid moves.</returns>
    public Point [] EnumMoves (bool optionalJumping)
    {
      if (cannotMove) { return new Point [0]; }
      CheckersPiece [] jumpArray;
      return EnumMovesCore(currentLocation, out jumpArray, optionalJumping);
    }
    /// <summary>Enumerates all possible moves from this point.</summary>
    /// <param name="jumped">Returns the array of Checkers piecse that are jumped, with respect to the moves.</param>
    /// <returns>An array of points which represent valid moves.</returns>
    public Point [] EnumMoves (out CheckersPiece [] jumped)
    {
      if (cannotMove) { jumped = new CheckersPiece [0]; return new Point [0]; }
      return EnumMovesCore(currentLocation, out jumped);
    }
    /// <summary>Enumerates all possible moves from this point.</summary>
    /// <param name="optionalJumping">Overrides the game's OptionalJumping parameter for the enumeration.</param>
    /// <param name="jumped">Returns the array of Checkers piecse that are jumped, with respect to the moves.</param>
    /// <returns>An array of points which represent valid moves.</returns>
    public Point [] EnumMoves (bool optionalJumping, out CheckersPiece [] jumped)
    {
      if (cannotMove) { jumped = new CheckersPiece [0]; return new Point [0]; }
      return EnumMovesCore(currentLocation, out jumped, optionalJumping);
    }
    /// <summary>Enumerates all possible single moves from this point.</summary>
    /// <returns>An array of points which represent valid moves.</returns>
    public Point [] EnumSingleMoves ()
    {
      if (cannotMove) return new Point [0];
      return (Point [])EnumSingleMovesCore(currentLocation, game.OptionalJumping).ToArray(typeof(Point));
    }
    /// <summary>Enumerates all possible single moves from this point.</summary>
    /// <param name="optionalJumping">Overrides the game's OptionalJumping parameter for the enumeration.</param>
    /// <returns>An array of points which represent valid moves.</returns>
    public Point [] EnumSingleMoves (bool optionalJumping)
    {
      if (cannotMove) return new Point [0];
      return (Point [])EnumSingleMovesCore(currentLocation, optionalJumping).ToArray(typeof(Point));
    }
    /// <summary>Enumerates all possible jump moves from this point.</summary>
    /// <returns>An array of points which represent valid moves.</returns>
    public Point [] EnumJumpMoves ()
    {
      if (cannotMove) return new Point [0];
      ArrayList jumpArray;
      return (Point [])EnumJumpMovesCore(currentLocation, out jumpArray).ToArray(typeof(Point));
    }
    /// <summary>Enumerates all possible jump moves from this point.</summary>
    /// <param name="jumped">Returns the array of Checkers piecse that are jumped, with respect to the moves.</param>
    /// <returns>An array of points which represent valid moves.</returns>
    public Point [] EnumJumpMoves (out CheckersPiece [] jumped)
    {
      if (cannotMove) { jumped = new CheckersPiece [0]; return new Point [0]; }
      ArrayList jumpedList;
      ArrayList moves = EnumJumpMovesCore(currentLocation, out jumpedList);
      jumped = (CheckersPiece [])jumpedList.ToArray(typeof(CheckersPiece));
      return (Point [])moves.ToArray(typeof(Point));
    }
    
    #endregion
    
    
    /// <summary>Creates a duplicate Checkers move object, so different paths may be tested from a particular point.</summary>
    /// <returns>The new Checkers move object.</returns>
    public CheckersMove Clone ()
    {
      CheckersMove move = new CheckersMove(game, piece, false);
      move.initialGame = initialGame;
      move.initialPiece = initialPiece;
      move.board = (CheckersPiece [,])board.Clone();
      move.currentLocation = currentLocation;
      move.jumped = (ArrayList)jumped.Clone();
      move.kinged = kinged;
      move.path = (ArrayList)path.Clone();
      move.cannotMove = cannotMove;
      return move;
    }
    
    /// <summary>Creates a duplicate Checkers move object from an identical (possibly cloned) game.</summary>
    /// <returns>The new Checkers move object.</returns>
    public CheckersMove Clone (CheckersGame game)
    { return FromPath(game, piece.Clone(game), Path); }
    
    /// <summary>Returns whether or not the move is valid.</summary>
    /// <param name="location">The location of the next move to take place.</param>
    /// <returns>True if the specidied location is a valid move.</returns>
    public bool IsValidMove (Point location)
    {
      foreach (Point p in EnumMoves())
      { if (p == location) return true; }
      return false;
    }
    /// <summary>Returns whether or not the move is valid.</summary>
    /// <param name="location">The location of the next move to take place.</param>
    /// <param name="optionalJumping">Overrides the game's OptionalJumping parameter for the enumeration.</param>
    /// <returns>True if the specidied location is a valid move.</returns>
    public bool IsValidMove (Point location, bool optionalJumping)
    {
      foreach (Point p in EnumMoves(optionalJumping))
      { if (p == location) return true; }
      return false;
    }
    
    /// <summary>Moves the piece to a new location.</summary>
    /// <param name="location">The new location to be moved to.</param>
    /// <returns>True if the move was valid.</returns>
    public bool Move (Point location)
    {
      if (cannotMove) return false;
      CheckersPiece [] jumpedArray;
      Point [] points = EnumMovesCore(currentLocation, out jumpedArray);
      for (int i = 0; i < points.Length; i++)
      {
        if (points[i] == location)
        {
          // Move piece
          if (jumpedArray[i] != null)
          {
            jumped.Add(jumpedArray[i]);
            board[jumpedArray[i].Location.X, jumpedArray[i].Location.Y] = null;
          }
          if ((Math.Abs(currentLocation.X - location.X) == 1) && (Math.Abs(currentLocation.Y - location.Y) == 1))
            cannotMove = true;
          board[currentLocation.X, currentLocation.Y] = null;
          board[location.X, location.Y] = piece;
          currentLocation = location;
          path.Add(location);
          // King a pawn if reached other end of board
          if ((!MustMove) && (piece.Rank == CheckersRank.Pawn))
            kinged = (((piece.Direction == CheckersDirection.Up) && (currentLocation.Y == 0)) || ((piece.Direction == CheckersDirection.Down) && (currentLocation.Y == CheckersGame.BoardSize.Height-1)));
          return true;
        }
      }
      return false;
    }
    
    /// <summary>Returns whether or not piece is in board bounds.</summary>
    /// <param name="location">The location to test.</param>
    /// <returns>True if specified location is in bounds.</returns>
    public bool InBounds (Point location)
    { return game.InBounds(location); }
    /// <summary>Returns whether or not piece is in board bounds.</summary>
    /// <param name="x">The x location to test.</param>
    /// <param name="y">The y location to test.</param>
    /// <returns>True if specified location is in bounds.</returns>
    public bool InBounds (int x, int y)
    { return game.InBounds(x, y); }
    
    private Point [] EnumMovesCore (Point fromLocation, out CheckersPiece [] jumped)
    { return EnumMovesCore(fromLocation, out jumped, game.OptionalJumping); }
    // Returns a list of moves and their respective jumps
    private Point [] EnumMovesCore (Point fromLocation, out CheckersPiece [] jumped, bool optionalJumping)
    {
      ArrayList jumpedList;
      ArrayList jumpMoves = EnumJumpMovesCore(fromLocation, out jumpedList);
      ArrayList singleMoves = EnumSingleMovesCore(fromLocation, optionalJumping, jumpMoves);
      // Append 'nulls' to single jumps
      for (int i = 0; i < singleMoves.Count; i++) jumpedList.Add(null);
      // Return results
      jumped = (CheckersPiece [])jumpedList.ToArray(typeof(CheckersPiece));
      singleMoves.AddRange(jumpMoves);
      return (Point [])singleMoves.ToArray(typeof(Point));
    }
    
    private ArrayList EnumSingleMovesCore (Point fromLocation, bool optionalJumping)
    {
      if (optionalJumping)
      {
        ArrayList jumpedList;
        ArrayList jumpMoves = EnumJumpMovesCore(fromLocation, out jumpedList);
        return EnumSingleMovesCore(fromLocation, optionalJumping, jumpMoves);
      }
      return EnumSingleMovesCore(fromLocation, optionalJumping, null);
    }
    private ArrayList EnumSingleMovesCore (Point fromLocation, bool optionalJumping, ArrayList jumpMoves)
    {
      // Create resizable list of jumpable moves and their respective jumps
      ArrayList moves = new ArrayList();
      // Check for single moves, if not jumping already took place
      if (path.Count > 0) return moves;
      
      bool canSingleMove = true;
      if (!optionalJumping)
      {
        // Determine whether or not a single jump can take place
        canSingleMove = (jumpMoves.Count == 0) || (game.OptionalJumping);
        if (canSingleMove)
        {
          // Further testing for single moves; test all other pieces on the board
          foreach (CheckersPiece testPiece in game.EnumPlayerPieces(game.Turn))
          {
            ArrayList dummy;
            if (EnumJumpMovesCore(testPiece, testPiece.Location, out dummy).Count == 0) continue;
            canSingleMove = false;
            break;
          }
        }
      }
      
      // Check whether or not a single move can take place
      if (!canSingleMove) return moves;
      
      // Append single-move moves in the enumeration (if able to)
      if (piece.Location == fromLocation)
      {
        if ((piece.Direction == CheckersDirection.Up) || (piece.Rank == CheckersRank.King))
        {
          if (InBounds(fromLocation.X-1, fromLocation.Y-1) && (board[fromLocation.X-1, fromLocation.Y-1] == null)) { moves.Add(new Point(fromLocation.X-1, fromLocation.Y-1)); }
          if (InBounds(fromLocation.X+1, fromLocation.Y-1) && (board[fromLocation.X+1, fromLocation.Y-1] == null)) { moves.Add(new Point(fromLocation.X+1, fromLocation.Y-1)); }
        }
        if ((piece.Direction == CheckersDirection.Down) || (piece.Rank == CheckersRank.King))
        {
          if (InBounds(fromLocation.X-1, fromLocation.Y+1) && (board[fromLocation.X-1, fromLocation.Y+1] == null)) { moves.Add(new Point(fromLocation.X-1, fromLocation.Y+1)); }
          if (InBounds(fromLocation.X+1, fromLocation.Y+1) && (board[fromLocation.X+1, fromLocation.Y+1] == null)) { moves.Add(new Point(fromLocation.X+1, fromLocation.Y+1)); }
        }
      }
      return moves;
    }
    
    private ArrayList EnumJumpMovesCore (Point fromLocation, out ArrayList jumped)
    { return EnumJumpMovesCore(piece, fromLocation, out jumped); }
    private ArrayList EnumJumpMovesCore (CheckersPiece piece, Point fromLocation, out ArrayList jumped)
    {
      ArrayList moves = new ArrayList();
      jumped = new ArrayList();
      // Append jumps (not of same team)
      if ((piece.Direction == CheckersDirection.Up) || (piece.Rank == CheckersRank.King))
      {
        if (game.InBounds(fromLocation.X-1, fromLocation.Y-1) && (board[fromLocation.X-1, fromLocation.Y-1] != null) && (board[fromLocation.X-1, fromLocation.Y-1].Player != piece.Player))
          if (InBounds(fromLocation.X-2, fromLocation.Y-2) && (board[fromLocation.X-2, fromLocation.Y-2] == null))
          { moves.Add(new Point(fromLocation.X-2, fromLocation.Y-2)); jumped.Add(board[fromLocation.X-1, fromLocation.Y-1]); }
        if (InBounds(fromLocation.X+1, fromLocation.Y-1) && (board[fromLocation.X+1, fromLocation.Y-1] != null) && (board[fromLocation.X+1, fromLocation.Y-1].Player != piece.Player))
          if (InBounds(fromLocation.X+2, fromLocation.Y-2) && (board[fromLocation.X+2, fromLocation.Y-2] == null))
          { moves.Add(new Point(fromLocation.X+2, fromLocation.Y-2)); jumped.Add(board[fromLocation.X+1, fromLocation.Y-1]); }
      }
      if ((piece.Direction == CheckersDirection.Down) || (piece.Rank == CheckersRank.King))
      {
        if (InBounds(fromLocation.X-1, fromLocation.Y+1) && (board[fromLocation.X-1, fromLocation.Y+1] != null) && (board[fromLocation.X-1, fromLocation.Y+1].Player != piece.Player))
          if (InBounds(fromLocation.X-2, fromLocation.Y+2) && (board[fromLocation.X-2, fromLocation.Y+2] == null))
          { moves.Add(new Point(fromLocation.X-2, fromLocation.Y+2)); jumped.Add(board[fromLocation.X-1, fromLocation.Y+1]); }
        if (InBounds(fromLocation.X+1, fromLocation.Y+1) && (board[fromLocation.X+1, fromLocation.Y+1] != null) && (board[fromLocation.X+1, fromLocation.Y+1].Player != piece.Player))
          if (InBounds(fromLocation.X+2, fromLocation.Y+2) && (board[fromLocation.X+2, fromLocation.Y+2] == null))
          { moves.Add(new Point(fromLocation.X+2, fromLocation.Y+2)); jumped.Add(board[fromLocation.X+1, fromLocation.Y+1]); }
      }
      return moves;
    }
  }
}
