using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Windows.Forms;

internal class ResFinder
{}

namespace Uberware.Gaming.Checkers.UI
{
  public enum CheckersUIState
  {
    Idle = 0,
    IdleMoving = 3,
    DragMoving = 1,
    ClickMoving = 2,
    ShowMove = 4,
  }
  
  public class MoveEventArgs : EventArgs
  {
    bool movedByPlayer;
    bool isWinningMove;
    CheckersMove move;
    public MoveEventArgs (bool movedByPlayer, bool isWinningMove, CheckersMove move)
    { this.movedByPlayer = movedByPlayer; this.isWinningMove = isWinningMove; this.move = move; }
    public bool MovedByPlayer
    { get { return movedByPlayer; } }
    public bool IsWinningMove
    { get { return isWinningMove; } }
    public CheckersMove Move
    { get { return move; } }
  }
  public delegate void MoveEventHandler (object sender, MoveEventArgs e);
  
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof(ResFinder), "Uberware.Gaming.Checkers.UI.ToolboxBitmap.png")]
  [Designer(typeof(CheckersDesigner))]
  public class CheckersUI : System.Windows.Forms.UserControl
  {
    public static readonly Size SquareSize = new Size(32, 32);
    public static readonly Size BoardPixelSize = new Size(SquareSize.Width*CheckersGame.BoardSize.Width, SquareSize.Height*CheckersGame.BoardSize.Height);
    
    public event EventHandler GameStarted;
    public event EventHandler GameStopped;
    public event EventHandler PiecePickedUp;
    public event MoveEventHandler PieceMoved;
    public event MoveEventHandler PieceMovedPartial;
    public event MoveEventHandler PieceBadMove;
    public event EventHandler PieceDeselected;
    public event EventHandler TurnChanged;
    public event EventHandler WinnerDeclared;
    
    private CheckersGame game = null;
    private CheckersUIState state = CheckersUIState.Idle;
    private CheckersMove movePiece = null;
    private CheckersMove destPiece = null;
    private int blinkCount = 0;
    private bool moveAfterShow = false;
    private Point initialDrag = Point.Empty;
    private int initialDragGrace = 0;
    private Bitmap boardImage;
    private Point [] selectedSquares = new Point [0];
    private Point [] darkenedSquares = new Point [0];
    private Point focussedSquare = Point.Empty;
    
    private bool textBorder = true;
    private Color textBorderColor = Color.Black;
    private ContentAlignment textAlign = ContentAlignment.MiddleCenter;
    private BorderStyle borderStyle = BorderStyle.Fixed3D;
    private int boardMargin = 4;
    private Color boardBackColor = Color.DarkSeaGreen;
    private Color boardForeColor = Color.OldLace;
    private Color boardGridColor = Color.Gray;
    private Image [] customPieceImages = new Image [4];
    private bool player1Active = true;
    private bool player2Active = true;
    private bool highlightPossibleMoves = true;
    private bool highlightSelection = true;
    private Image [] pieceImages = new Image [4];
    private Cursor [] pieceCursors = new Cursor [4];
    private bool showJumpMessage = true;
    
    #region Class Controls

    private System.Windows.Forms.Timer tmrShowMove;
    private System.Windows.Forms.Timer tmrBlink;
    private System.ComponentModel.IContainer components;
    
    #endregion
    
    #region Class Construction
    
    public CheckersUI ()
    {
      // This call is required by the Windows.Forms Form Designer.
      InitializeComponent();
      SetStyle(ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.DoubleBuffer, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      SetStyle(ControlStyles.ResizeRedraw, false);
      SetStyle(ControlStyles.FixedWidth, true);
      SetStyle(ControlStyles.FixedHeight, true);
      // Initialize the image
      CreateBoard();
    }
    
		#region Component Designer generated code
		
    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.tmrShowMove = new System.Windows.Forms.Timer(this.components);
      this.tmrBlink = new System.Windows.Forms.Timer(this.components);
      // 
      // tmrShowMove
      // 
      this.tmrShowMove.Tick += new System.EventHandler(this.tmrShowMove_Tick);
      // 
      // tmrBlink
      // 
      this.tmrBlink.Tick += new System.EventHandler(this.tmrBlink_Tick);
      // 
      // CheckersUI
      // 
      this.BackColor = System.Drawing.Color.White;
      this.Name = "CheckersUI";
      this.Size = new System.Drawing.Size(360, 276);
      this.Resize += new System.EventHandler(this.CheckersUI_Resize);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CheckersUI_MouseUp);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.CheckersUI_Paint);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CheckersUI_MouseMove);
      this.MouseLeave += new System.EventHandler(this.CheckersUI_MouseLeave);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckersUI_MouseDown);

    }
		
    #endregion
    
    #endregion
    
    #region Class Properties
    
    [DefaultValue(BorderStyle.Fixed3D)]
    public BorderStyle BorderStyle
    {
      get { return borderStyle; }
      set { borderStyle = value; SetBoundsCore(Left, Top, 0, 0, BoundsSpecified.Size); Refresh(); }
    }
    
    [DefaultValue(""), Browsable(true), DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
      get { return base.Text; }
      set { base.Text = value; Refresh(); }
    }
    
    [DefaultValue(true)]
    public bool TextBorder
    {
      get { return textBorder; }
      set { textBorder = value; Refresh(); }
    }
    
    [DefaultValue(typeof(Color), "Black")]
    public Color TextBorderColor
    {
      get { return textBorderColor; }
      set { textBorderColor = value; Refresh(); }
    }
    
    [DefaultValue(ContentAlignment.MiddleCenter)]
    public ContentAlignment TextAlign
    {
      get { return textAlign; }
      set { textAlign = value; Refresh(); }
    }
    
    [DefaultValue(typeof(Color), "White")]
    public override Color BackColor
    {
      get { return base.BackColor; }
      set { base.BackColor = value; Refresh(); }
    }
    
    [DefaultValue(4)]
    public int BoardMargin
    {
      get { return boardMargin; }
      set { boardMargin = value; SetBoundsCore(Left, Top, 0, 0, BoundsSpecified.Size); Refresh(); }
    }
    
    [Browsable(false)]
    public override Image BackgroundImage
    {
      get { return base.BackgroundImage; }
      set { base.BackgroundImage = value; }
    }
    
    [DefaultValue(typeof(Color), "DarkSeaGreen")]
    public Color BoardBackColor
    {
      get { return boardBackColor; }
      set { boardBackColor = value; Refresh(); }
    }
    
    [DefaultValue(typeof(Color), "OldLace")]
    public Color BoardForeColor
    {
      get { return boardForeColor; }
      set { boardForeColor = value; Refresh(); }
    }
    
    [DefaultValue(typeof(Color), "Gray")]
    public Color BoardGridColor
    {
      get { return boardGridColor; }
      set { boardGridColor = value; Refresh(); }
    }
    
    [DefaultValue(null)]
    public Image CustomPawn1
    {
      get { return customPieceImages[0]; }
      set { customPieceImages[0] = value; }
    }
    [DefaultValue(null)]
    public Image CustomKing1
    {
      get { return customPieceImages[1]; }
      set { customPieceImages[1] = value; }
    }
    [DefaultValue(null)]
    public Image CustomPawn2
    {
      get { return customPieceImages[2]; }
      set { customPieceImages[2] = value; }
    }
    [DefaultValue(null)]
    public Image CustomKing2
    {
      get { return customPieceImages[3]; }
      set { customPieceImages[3] = value; }
    }
    
    [DefaultValue(true)]
    public bool Player1Active
    {
      get { return player1Active; }
      set { player1Active = value; CheckMoving(); }
    }
    [DefaultValue(true)]
    public bool Player2Active
    {
      get { return player2Active; }
      set { player2Active = value; CheckMoving(); }
    }
    
    [DefaultValue(true)]
    public bool HighlightSelection
    {
      get { return highlightSelection; }
      set { highlightSelection = value; Refresh(); }
    }
    
    [DefaultValue(true)]
    public bool HighlightPossibleMoves
    {
      get { return highlightPossibleMoves; }
      set { highlightPossibleMoves = value; Refresh(); }
    }
    
    [DefaultValue(true)]
    public bool ShowJumpMessage
    {
      get { return showJumpMessage; }
      set { showJumpMessage = value; }
    }
    
    
    [Browsable(false)]
    public CheckersGame Game
    { get { return game; } }
    
    [Browsable(false)]
    public CheckersPiece Holding
    { get { return (( IsHolding )?( movePiece.Piece ):( null )); } }
    
    [Browsable(false)]
    public bool IsHolding
    { get { return (IsPlaying) && ((state == CheckersUIState.DragMoving) || (state == CheckersUIState.ClickMoving)); } }
    
    [Browsable(false)]
    public CheckersMove CurrentMove
    { get { return (( IsMoving )?( movePiece.Fork() ):( null )); } }
    
    [Browsable(false)]
    public bool IsMoving
    { get { return (IsMovingByPlayer) || (IsMovingByControl); } }
    
    [Browsable(false)]
    public bool IsMovingByPlayer
    { get { return (IsPlaying) && ((state == CheckersUIState.DragMoving) || (state == CheckersUIState.ClickMoving) || (state == CheckersUIState.IdleMoving)); } }
    
    [Browsable(false)]
    public bool IsMovingByControl
    { get { return (IsPlaying) && (state == CheckersUIState.ShowMove); } }
    
    [Browsable(false)]
    public bool IsPlaying
    { get { return ((game != null) && (game.IsPlaying)); } }
    
    [Browsable(false)]
    public int Winner
    { get { return (( game != null )?( game.Winner ):( 0 )); } }
    
    #endregion
    
    
    #region Control Framework
    
    protected override void SetBoundsCore (int x, int y, int width, int height, BoundsSpecified specified)
    {
      int borderSize = (( borderStyle == BorderStyle.Fixed3D )?( 2 ):( (( borderStyle == BorderStyle.FixedSingle )?( 1 ):( 0 )) ));
      base.SetBoundsCore(x, y, BoardPixelSize.Width+2+(borderSize*2)+(boardMargin*2), BoardPixelSize.Height+2+(borderSize*2)+(boardMargin*2), specified);
    }
    
    private void CheckersUI_Resize (object sender, System.EventArgs e)
    { CreateBoard(); }
    
    private void CheckersUI_Paint (object sender, System.Windows.Forms.PaintEventArgs e)
    {
      // Draw control background and border
      e.Graphics.Clear(BackColor);
      int borderSize = 0;
      if (borderStyle == BorderStyle.Fixed3D)
      {
        Pen penLight = new Pen(Color.FromKnownColor(KnownColor.ControlLight)); Pen penLightLight = new Pen(Color.FromKnownColor(KnownColor.ControlLightLight));
        Pen penDark = new Pen(Color.FromKnownColor(KnownColor.ControlDark)); Pen penDarkDark = new Pen(Color.FromKnownColor(KnownColor.ControlDarkDark));
        //
        e.Graphics.DrawLine(penDark, 0, 0, Width-2, 0); e.Graphics.DrawLine(penDarkDark, 0, 1, Width-3, 1);
        e.Graphics.DrawLine(penDark, 0, 1, 0, Height-2); e.Graphics.DrawLine(penDarkDark, 1, 2, 1, Height-3);
        e.Graphics.DrawLine(penLightLight, 0, Height-1, Width-1, Height-1); e.Graphics.DrawLine(penLight, 1, Height-2, Width-1, Height-2);
        e.Graphics.DrawLine(penLightLight, Width-1, 0, Width-1, Height-1); e.Graphics.DrawLine(penLight, Width-2, 1, Width-2, Height-2);
        //
        penLight.Dispose(); penLightLight.Dispose();
        penDark.Dispose(); penDarkDark.Dispose();
        borderSize = 2;
      }
      else if (borderStyle == BorderStyle.FixedSingle)
      {
        Pen penFrame = new Pen(Color.FromKnownColor(KnownColor.WindowFrame));
        //
        e.Graphics.DrawLine(penFrame, 0, 0, Width, 0);
        e.Graphics.DrawLine(penFrame, 0, 0, 0, Height);
        e.Graphics.DrawLine(penFrame, 0, Height-1, Width, Height-1);
        e.Graphics.DrawLine(penFrame, Width-1, 0, Width-1, Height);
        //
        penFrame.Dispose();
        borderSize = 1;
      }
      e.Graphics.DrawImage(boardImage, boardMargin+borderSize, boardMargin+borderSize, boardImage.Width, boardImage.Height);
      // Draw the text
      int nextLineOffset = 0;
      SizeF textSize = e.Graphics.MeasureString(base.Text, Font);
      foreach (string s in (base.Text + '\n').Split('\n', '\r'))
      {
        if (s == string.Empty) continue;
        SizeF stringSize = e.Graphics.MeasureString(s, Font);
        int x, y;
        switch (textAlign)
        {
          case ContentAlignment.TopLeft: x = 0; y = 0; break;
          case ContentAlignment.TopCenter: x = (int)((this.Size.Width - stringSize.Width) / 2); y = 0; break;
          case ContentAlignment.TopRight: x = (int)(this.Size.Width - stringSize.Width); y = 0; break;
          case ContentAlignment.MiddleLeft: x = 0; y = (int)((this.Size.Height - textSize.Height) / 2); break;
          case ContentAlignment.MiddleCenter: x = (int)((this.Size.Width - stringSize.Width) / 2); y = (int)((this.Size.Height - textSize.Height) / 2); break;
          case ContentAlignment.MiddleRight: x = (int)(this.Size.Width - stringSize.Width); y = (int)((this.Size.Height - textSize.Height) / 2); break;
          case ContentAlignment.BottomLeft: x = 0; y = (int)(this.Size.Height - textSize.Height); break;
          case ContentAlignment.BottomCenter: x = (int)((this.Size.Width - stringSize.Width) / 2); y = (int)(this.Size.Height - textSize.Height); break;
          case ContentAlignment.BottomRight: x = (int)(this.Size.Width - stringSize.Width); y = (int)(this.Size.Height - textSize.Height); break;
          default: x = y = 0; break;
        }
        y += nextLineOffset;
        nextLineOffset += (int)(stringSize.Height);
        if (textBorder)
        {
          Brush borderBrush = new SolidBrush(textBorderColor);
          e.Graphics.DrawString(s, Font, borderBrush, x-1, y-1);
          e.Graphics.DrawString(s, Font, borderBrush, x+1, y-1);
          e.Graphics.DrawString(s, Font, borderBrush, x-1, y+1);
          e.Graphics.DrawString(s, Font, borderBrush, x+1, y+1);
        }
        e.Graphics.DrawString(s, Font, new SolidBrush(ForeColor), x, y);
      }
    }
    
    #endregion
    
    #region Image Functions
    
    private void CreateBoard ()
    {
      boardImage = new Bitmap(ClientSize.Width, ClientSize.Height, PixelFormat.Format32bppArgb);
      Refresh();
    }
    
    private void CreateImages ()
    {
      // Get piece images
      pieceImages[0] = (( customPieceImages[0] != null )?( AdjustPieceImage(customPieceImages[0]) ):( CreatePieceImage(Color.LightGray, false) ));
      pieceImages[1] = (( customPieceImages[1] != null )?( AdjustPieceImage(customPieceImages[1]) ):( CreatePieceImage(Color.LightGray, true) ));
      pieceImages[2] = (( customPieceImages[2] != null )?( AdjustPieceImage(customPieceImages[2]) ):( CreatePieceImage(Color.Firebrick, false) ));
      pieceImages[3] = (( customPieceImages[3] != null )?( AdjustPieceImage(customPieceImages[3]) ):( CreatePieceImage(Color.Firebrick, true) ));
      // Get piece cursors
      pieceCursors[0] = new Cursor((new Bitmap(pieceImages[0])).GetHicon());
      pieceCursors[1] = new Cursor((new Bitmap(pieceImages[1])).GetHicon());
      pieceCursors[2] = new Cursor((new Bitmap(pieceImages[2])).GetHicon());
      pieceCursors[3] = new Cursor((new Bitmap(pieceImages[3])).GetHicon());
    }
    private Image AdjustPieceImage (Image pieceImage)
    {
      if ((pieceImage.Size.Width == 32) && (pieceImage.Size.Height == 32))
        return pieceImage;
      return new Bitmap(pieceImage, 32, 32);
    }
    private Bitmap CreatePieceImage (Color color, bool isKing)
    {
      Bitmap pieceImage = new Bitmap(32, 32);
      pieceImage.MakeTransparent();
      Graphics g = Graphics.FromImage(pieceImage);
      Brush fillBrush = new SolidBrush(color);
      Pen ringColor = new Pen(LightenColor(color, 0x28));
      g.FillEllipse(fillBrush, 2, 2, 32-5, 32-5);
      g.DrawEllipse(ringColor, 3, 3, 32-7, 32-7);
      g.DrawEllipse(Pens.Black, 2, 2, 32-5, 32-5);
      ringColor.Dispose(); fillBrush.Dispose();
      g.Dispose();
      return pieceImage;
    }
    
    private Color LightenColor (Color color, int intensity)
    {
      if (intensity < 0) return DarkenColor(color, -intensity);
      return Color.FromArgb( (( color.R+intensity > 0xFF )?( 0xFF ):( color.R+intensity )), (( color.G+intensity > 0xFF )?( 0xFF ):( color.G+intensity )), (( color.B+intensity > 0xFF )?( 0xFF ):( color.B+intensity )) );
    }
    private Color DarkenColor (Color color, int intensity)
    {
      if (intensity < 0) return LightenColor(color, -intensity);
      return Color.FromArgb( (( color.R-intensity < 0 )?( 0 ):( color.R-intensity )), (( color.G-intensity < 0 )?( 0 ):( color.G-intensity )), (( color.B-intensity < 0 )?( 0 ):( color.B-intensity )) );
    }
    private Color BlendColor (Color color1, Color color2, int blendPercent)
    {
      if ((blendPercent < 0) || (blendPercent > 100)) throw new ArgumentOutOfRangeException("blendPercent", blendPercent, "The parameter 'blendPercent' must be a valid percent range [0 - 100].");
      if (blendPercent == 0) return color1;
      if (blendPercent == 100) return color2;
      int r = (int)((color1.R*((100-blendPercent)/100.0)) + (color2.R*(blendPercent/100.0)));
      int g = (int)((color1.G*((100-blendPercent)/100.0)) + (color2.G*(blendPercent/100.0)));
      int b = (int)((color1.B*((100-blendPercent)/100.0)) + (color2.B*(blendPercent/100.0)));
      return Color.FromArgb(r, g, b);
    }
    
    #endregion
    
    #region Game Functions
    
    private void CheckersGame_GameStarted (object sender, System.EventArgs e)
    { OnGameStarted(); }
    private void CheckersGame_GameStopped (object sender, System.EventArgs e)
    { OnGameStopped(); }
    private void CheckersGame_TurnChanged (object sender, System.EventArgs e)
    { OnTurnChanged(); }
    private void CheckersGame_WinnerDeclared (object sender, System.EventArgs e)
    { OnWinnerDeclared(); }
    
    private void SetGame (CheckersGame g)
    {
      if (game != null)
      {
        if (game.IsPlaying) throw new InvalidOperationException("Game has already started.");
        game.GameStarted -= new EventHandler(CheckersGame_GameStarted);
        game.GameStopped -= new EventHandler(CheckersGame_GameStopped);
        game.TurnChanged -= new EventHandler(CheckersGame_TurnChanged);
        game.WinnerDeclared -= new EventHandler(CheckersGame_WinnerDeclared);
      }
      game = g;
      game.GameStarted += new EventHandler(CheckersGame_GameStarted);
      game.GameStopped += new EventHandler(CheckersGame_GameStopped);
      game.TurnChanged += new EventHandler(CheckersGame_TurnChanged);
      game.WinnerDeclared += new EventHandler(CheckersGame_WinnerDeclared);
    }
    
    private void OnGameStarted ()
    {
      CreateImages();
      Refresh();
      if (GameStarted != null) GameStarted(this, EventArgs.Empty);
    }
    
    private void OnGameStopped ()
    {
      movePiece = null;
      state = CheckersUIState.Idle;
      Cursor = Cursors.Default;
      focussedSquare = Point.Empty;
      selectedSquares = new Point [0];
      darkenedSquares = new Point [0];
      Refresh();
      if (GameStopped != null) GameStopped(this, EventArgs.Empty);
    }
    
    private void OnPiecePickedUp ()
    { if (PiecePickedUp != null) PiecePickedUp(this, EventArgs.Empty); }
    
    private void OnTurnChanged ()
    { if (TurnChanged != null) TurnChanged(this, EventArgs.Empty); }
    
    private void OnWinnerDeclared ()
    { if (WinnerDeclared != null) WinnerDeclared(this, EventArgs.Empty); }
    
    private void CheckMoving ()
    {
      if ((state != CheckersUIState.DragMoving) && (state != CheckersUIState.ClickMoving) && (state != CheckersUIState.IdleMoving)) return;
      if (movePiece == null) return;
      // Be sure moving is possible
      if (((movePiece.Piece.Player == 1) && (!player1Active)) || ((movePiece.Piece.Player == 2) && (!player2Active)))
        StopMove(true);
    }
    private void StopMove (bool badMoveEvent)
    {
      // Must drop piece
      state = CheckersUIState.Idle;
      focussedSquare = Point.Empty;
      selectedSquares = new Point [0];
      darkenedSquares = new Point [0];
      Refresh();
    }
    
    // Moves the piece with optional refreshing
    private bool MovePieceCore (CheckersMove move, bool showMove, bool noDelay)
    {
      // !!!!! Smooth movements
      // Move the piece
      if (showMove)
      {
        if (state != CheckersUIState.Idle) StopMove(false);
        if (!game.IsValidMove(move)) return false;
        ShowMoveCore(move, noDelay, false, true);
        return true;
      }
      if (!game.MovePiece(move)) return false;
      if (showMove) { ShowLastMove(false); return true; }
      Refresh();
      if ((game.Winner != 0) && (WinnerDeclared != null))
      { WinnerDeclared(this, EventArgs.Empty); focussedSquare = Point.Empty; }
      return true;
    }
    
    private void DoFocusSquare (Point location, bool refresh)
    {
      if (!IsPlaying) return;
      if (state == CheckersUIState.ShowMove) return;
      // Get piece location (hit-test)
      CheckersPiece piece = game.PieceAt(location);
      if (IsMovingByPlayer)
      {
        if (((location.X % 2) != (location.Y % 2)) && (((IsMovingByPlayer) && (IsHolding)) || (location == movePiece.CurrentLocation)))
        { focussedSquare = location; if (refresh) Refresh(); }
        else if (!focussedSquare.IsEmpty)
        { focussedSquare = Point.Empty; if (refresh) Refresh(); }
        return;
      }
      bool doHighlight = true;
      if (((game.Turn == 1) && (!player1Active)) || ((game.Turn == 2) && (!player2Active))) doHighlight = false;
      if ((piece == null) || (!game.CanMovePiece(piece))) doHighlight = false;
      if (!doHighlight)
      {
        if (!focussedSquare.IsEmpty)
        { focussedSquare = Point.Empty; if (refresh) Refresh(); }
        return;
      }
      // Highlight board
      if ((!focussedSquare.IsEmpty) && (focussedSquare == piece.Location)) return;
      focussedSquare = piece.Location;
      if (refresh) Refresh();
    }
    
    #endregion
    
    #region UI Control
    
    private void CheckersUI_MouseDown (object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (!IsPlaying) return;
      if (e.Button != MouseButtons.Left) return;
      CheckersPiece piece;
      
      switch (state)
      {
        case CheckersUIState.Idle:
          // Be sure moving is possible on -any- piece this turn
          if (((game.Turn == 1) && (!player1Active)) || ((game.Turn == 2) && (!player2Active))) break;
          // Get the piece to move, and be sure movement is possible
          piece = game.PieceAt(PointToGame(new Point(e.X, e.Y)));
          if (piece == null) break;
          if (piece.Player != game.Turn) break;
          // Begin moving the piece
          movePiece = game.BeginMove(piece);
          goto case CheckersUIState.IdleMoving;
        case CheckersUIState.IdleMoving:
          // Be sure moving is possible on -any- piece this turn
          if (((game.Turn == 1) && (!player1Active)) || ((game.Turn == 2) && (!player2Active))) break;
          if (movePiece == null) break;
          // Get the piece to move, and be sure movement is possible
          piece = movePiece.Piece;
          if (piece == null) break;
          if (piece.Player != game.Turn) break;
          if (movePiece.CurrentLocation != PointToGame(new Point(e.X, e.Y))) break;
          // Set new state and the point of the initial drag
          state = CheckersUIState.DragMoving;
          initialDrag = new Point(e.X, e.Y);
          initialDragGrace = 2;
          // Set cursor and highlight the squares
          Cursor = pieceCursors[(piece.Player-1)*2 + (( piece.Rank == CheckersRank.Pawn )?( 0 ):( 1 ))];
          selectedSquares = movePiece.EnumMoves();
          darkenedSquares = new Point [0];
          Refresh();
          if (PiecePickedUp != null) PiecePickedUp(this, EventArgs.Empty);
          break;
        case CheckersUIState.DragMoving:
          // Should never reach here (unless UI/focus glitch occurred)
          goto case CheckersUIState.ClickMoving;
        case CheckersUIState.ClickMoving:
          // Drop the piece
          state = CheckersUIState.DragMoving;
          CheckersUI_MouseUp(sender, e);
          break;
        default: break;
      }
    }
    
    private void CheckersUI_MouseUp (object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (!IsPlaying) return;
      if (e.Button != MouseButtons.Left) return;
      blinkCount = 0;
      
      switch (state)
      {
        case CheckersUIState.Idle:
          // Do nothing
          break;
        case CheckersUIState.IdleMoving:
          // Do nothing
          break;
        case CheckersUIState.DragMoving:
          // Check for drag proximity
          if ((!initialDrag.IsEmpty) && (initialDragGrace > 0))
            if ((e.X >= initialDrag.X - 1) && (e.X <= initialDrag.X + 1) && (e.Y >= initialDrag.Y - 1) && (e.Y <= initialDrag.Y + 1))
            {
              state = CheckersUIState.ClickMoving;
              break;
            }
          // Drop the piece
          Cursor = Cursors.Default;
          Point location = PointToGame(new Point(e.X, e.Y));
          // Move the piece
          bool dropSuccess = false, partialSuccess = false, invalidJump = false;
          CheckersPiece piece = movePiece.Piece;
          CheckersMove move = movePiece;
          if (movePiece.Move(location))
          {
            if (movePiece.MustMove)
            {
              state = CheckersUIState.IdleMoving;
              partialSuccess = true;
              darkenedSquares = new Point [] { movePiece.CurrentLocation };
            }
            else
            {
              // Move the piece on the gameboard
              dropSuccess = MovePieceCore(movePiece, false, false);
              movePiece = null;
              state = CheckersUIState.Idle;
            }
          }
          else
          {
            invalidJump = ((!game.OptionalJumping) && (movePiece.IsValidMove(location, true)));
            movePiece = null;
            state = CheckersUIState.Idle;
          }
          selectedSquares = new Point [0];
          DoFocusSquare(location, false);
          Refresh();
          bool isWinningMove = !IsPlaying;
          if (dropSuccess)
          { if (PieceMoved != null) PieceMoved(this, new MoveEventArgs(true, isWinningMove, move.Fork())); }
          else if (partialSuccess)
          { if (PieceMovedPartial != null) PieceMovedPartial(this, new MoveEventArgs(true, false, move.Fork())); }
          else
          {
            if (location == piece.Location) if (PieceDeselected != null) PieceDeselected(this, EventArgs.Empty);
            if (location != piece.Location) if (PieceBadMove != null) PieceBadMove(this, new MoveEventArgs(true, false, move.Fork()));
          }
          if (invalidJump)
          {
            if (showJumpMessage)
              MessageBox.Show(this, "You must jump your opponent's piece.", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BlinkPieces(game.EnumMovablePieces());
          }
          break;
        case CheckersUIState.ClickMoving:
          // Do nothing
          break;
        default: break;
      }
    }
    
    private void CheckersUI_MouseMove (object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (!IsPlaying) return;
      if (initialDragGrace > 0) initialDragGrace--;
      DoFocusSquare(PointToGame(new Point(e.X, e.Y)), true);
    }
    
    private void CheckersUI_MouseLeave (object sender, System.EventArgs e)
    {
      if (!IsPlaying) return;
      if (focussedSquare.IsEmpty) return;
      focussedSquare = Point.Empty;
      Refresh();
    }
    
    private void tmrBlink_Tick (object sender, System.EventArgs e)
    {
      if (state != CheckersUIState.Idle) { tmrBlink.Stop(); return; }
      if (blinkCount == 0)
      {
        darkenedSquares = new Point [0];
        tmrBlink.Stop();
      }
      if (blinkCount > 0) blinkCount--;
      Refresh();
    }
    
    private void BlinkPieces (CheckersPiece [] pieces)
    {
      Point [] squares = new Point [pieces.Length];
      for (int i = 0; i < pieces.Length; i++)
        squares[i] = pieces[i].Location;
      BlinkSquares(squares);
    }
    private void BlinkSquares (Point [] squares)
    {
      if (state != CheckersUIState.Idle) return;
      darkenedSquares = squares;
      blinkCount = 2;
      tmrBlink.Interval = 200;
      tmrBlink.Start();
      Refresh();
    }
    
    private void tmrShowMove_Tick (object sender, System.EventArgs e)
    {
      if (state != CheckersUIState.ShowMove) return;
      if (blinkCount > 0)
      {
        focussedSquare = (( (blinkCount % 2) == 0 )?( movePiece.CurrentLocation ):( Point.Empty ));
        blinkCount--;
        Refresh();
        tmrShowMove.Interval = (( blinkCount == 0 )?( 600 ):( 100 ));
        return;
      }
      
      bool failed = false;
      if (movePiece.Path.Length < destPiece.Path.Length)
      {
        tmrShowMove.Interval = 500;
        failed = !movePiece.Move(destPiece.Path[movePiece.Path.Length]);
        if ((!failed) && (movePiece.Path.Length < destPiece.Path.Length))
        {
          Refresh();
          if (PieceMovedPartial != null) PieceMovedPartial(this, new MoveEventArgs(false, false, movePiece.Fork()));
          return;
        }
      }
      // Completed the path
      CheckersMove move = destPiece;
      destPiece = null;
      movePiece = null;
      state = CheckersUIState.Idle;
      tmrShowMove.Stop();
      bool winner = (Winner != 0);
      if ((moveAfterShow) && (!failed))
        MovePieceCore(move, false, false);
      else
        Refresh();
      if (PieceMoved != null) PieceMoved(this, new MoveEventArgs(false, !((IsPlaying) || ((Winner != 0) && (winner))), move.Fork()));
      // If move was invalid, show msgbox now that movement has stopped
      if (failed) MessageBox.Show(this, "Could not show piece movement .. path seems to be invalid!\n\nAborting move.", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    
    private void ShowMoveCore (CheckersMove move, bool noDelay, bool blink, bool moveWhenDone)
    {
      if (state != CheckersUIState.Idle) return;
      // Remember movement and begin a new movement to follow the same path
      destPiece = move;
      movePiece = destPiece.InitialGame.BeginMove(destPiece.InitialPiece);
      state = CheckersUIState.ShowMove;
      moveAfterShow = moveWhenDone;
      if (blink)
      {
        focussedSquare = movePiece.CurrentLocation;
        blinkCount = 3;
        Refresh();
        tmrShowMove.Interval = 100;
      }
      else
      {
        focussedSquare = Point.Empty;
        Refresh();
        blinkCount = 0;
        tmrShowMove.Interval = ( noDelay )?( 10 ):( 600 );
      }
      tmrShowMove.Start();
    }
    
    #endregion
    
    /// <summary>Begins the checkers game with a new CheckersGame object if none is selected.</summary>
    public void Play ()
    {
      if (IsPlaying) throw new InvalidOperationException("Game has already started.");
      if (game == null) SetGame(new CheckersGame());
      game.Play();
    }
    /// <summary>Begins the checkers game.</summary>
    /// <param name="game">The game to play.</param>
    public void Play (CheckersGame game)
    {
      if (IsPlaying) throw new InvalidOperationException("Game has already started.");
      SetGame(game);
      Play();
    }
    
    /// <summary>Stops a decided game or forces a game-in-progress to stop prematurely with no winner.</summary>
    public void Stop ()
    {
      if (game == null) return;
      game.Stop();
    }
    
    /// <summary> Computes the location of the specified point into Checkers game board coordinates. </summary>
    /// <param name="p">The screen coordinate to convert.</param>
    /// <returns>The location in Checkers game board coorinates.</returns>
    public Point PointToGame (Point p)
    {
      int borderSize;
      if (borderStyle == BorderStyle.Fixed3D) borderSize = 2;
      else if (borderStyle == BorderStyle.FixedSingle) borderSize = 1;
      else borderSize = 0;
      int x = (p.X-boardMargin-borderSize-1) / SquareSize.Width;
      int y = (p.Y-boardMargin-borderSize-1) / SquareSize.Height;
      return new Point(x, y);
    }
    
    /// <summary>Moves a Checkers piece on the board.</summary>
    /// <param name="move">The movement object to which the piece will move to.</param>
    /// <returns>True if the piece was moved successfully.</returns>
    public bool MovePiece (CheckersPiece piece, Point [] path)
    { return MovePiece(piece, path, true); }
    /// <summary>Moves a Checkers piece on the board.</summary>
    /// <param name="move">The movement object to which the piece will move to.</param>
    /// <returns>True if the piece was moved successfully.</returns>
    public bool MovePiece (CheckersPiece piece, Point [] path, bool showMove)
    { return MovePiece(piece, path, showMove, false); }
    /// <summary>Moves a Checkers piece on the board.</summary>
    /// <param name="move">The movement object to which the piece will move to.</param>
    /// <returns>True if the piece was moved successfully.</returns>
    public bool MovePiece (CheckersPiece piece, Point [] path, bool showMove, bool noDelay)
    {
      if (!IsPlaying) return false;
      CheckersMove move = game.BeginMove(piece);
      foreach (Point point in path)
        if (!move.Move(point)) return false;
      return MovePiece(move, showMove, noDelay);
    }
    /// <summary>Moves a Checkers piece on the board.</summary>
    /// <param name="move">The movement object to which the piece will move to.</param>
    /// <returns>True if the piece was moved successfully.</returns>
    public bool MovePiece (CheckersMove move)
    { return MovePiece(move, true); }
    /// <summary>Moves a Checkers piece on the board.</summary>
    /// <param name="move">The movement object to which the piece will move to.</param>
    /// <param name="showMove">Decides whether or not the move should be shown.</param>
    /// <returns>True if the piece was moved successfully.</returns>
    public bool MovePiece (CheckersMove move, bool showMove)
    { return MovePiece(move, showMove, false); }
    /// <summary>Moves a Checkers piece on the board.</summary>
    /// <param name="move">The movement object to which the piece will move to.</param>
    /// <param name="showMove">Decides whether or not the move should be shown.</param>
    /// <returns>True if the piece was moved successfully.</returns>
    public bool MovePiece (CheckersMove move, bool showMove, bool noDelay)
    {
      if (!IsPlaying) return false;
      return MovePieceCore(move, showMove, noDelay);
    }
    /// <summary>Moves a Checkers piece on the board using the Checkers agent.</summary>
    /// <param name="agent">The Checkers agent to calculate the next move.</param>
    /// <returns>True if the piece was moved successfully.</returns>
    public bool MovePiece (CheckersAgent agent)
    { return MovePiece(agent, true); }
    /// <summary>Moves a Checkers piece on the board using the Checkers agent.</summary>
    /// <param name="agent">The Checkers agent to calculate the next move.</param>
    /// <param name="showMove">Decides whether or not the move should be shown.</param>
    /// <returns>True if the piece was moved successfully.</returns>
    public bool MovePiece (CheckersAgent agent, bool showMove)
    {
      if (!IsPlaying) return false;
      return MovePieceCore(agent.NextMove(game), showMove, false);
    }
    
    /// <summary>Shows the last move that was made.</summary>
    public void ShowLastMove ()
    { ShowLastMove(true); }
    /// <summary>Shows the last move that was made.</summary>
    /// <param name="blink">Decides whether or not to make the piece to be moved blink before moving.</param>
    public void ShowLastMove (bool blink)
    { ShowLastMove(blink, false); }
    /// <summary>Shows the last move that was made.</summary>
    /// <param name="blink">Decides whether or not to make the piece to be moved blink before moving.</param>
    /// <param name="noDelay">True for no delay in movement.</param>
    public void ShowLastMove (bool blink, bool noDelay)
    {
      if ((!IsPlaying) && (Winner == 0)) return;
      if (game.LastMove == null) return;
      ShowMoveCore(game.LastMove, noDelay, blink, false);
    }
    
    /// <summary> Refreshes the Checkers baord and the control.</summary>
    public override void Refresh ()
    {
      if (boardImage == null) return;
      Graphics g = Graphics.FromImage(boardImage);
      Pen penGridColor = new Pen(boardGridColor);
      Brush brushBackColor = new SolidBrush(boardBackColor);
      Brush brushForeColor = new SolidBrush(boardForeColor);
      
      // Draw the grid and the board background
      g.DrawRectangle(penGridColor, 0, 0, BoardPixelSize.Width+1, BoardPixelSize.Height+1);
      g.FillRectangle(brushBackColor, 1, 1, BoardPixelSize.Width, BoardPixelSize.Height);
      
      // Draw the squares and pieces
      for (int y = 0; y < CheckersGame.BoardSize.Height; y++)
      {
        for (int x = 0; x < CheckersGame.BoardSize.Width; x++)
        {
          if ((x % 2) == (y % 2)) continue;
          // Get whether or not square is a 'valid move' square
          bool isDarkenedSquare = false, isFocussedSquare = false, isValidMoveSquare = false;
          isFocussedSquare = ((highlightSelection) && (focussedSquare.X == x) && (focussedSquare.Y == y));
          if (highlightPossibleMoves) foreach (Point p in selectedSquares) if ((p.X == x) && (p.Y == y)) { isValidMoveSquare = true; break; }
          if ((blinkCount % 2) == 0) foreach (Point p in darkenedSquares) if ((p.X == x) && (p.Y == y)) { isDarkenedSquare = true; break; }
          // Draw the square
          if (isFocussedSquare)
          {
            g.FillRectangle( new SolidBrush(BlendColor(boardForeColor, Color.FromArgb(49, 106, 197), 50)), x*SquareSize.Width + 1, y*SquareSize.Height + 1, SquareSize.Width, SquareSize.Height);
            g.FillRectangle( new SolidBrush(BlendColor(boardForeColor, Color.FromArgb(193, 210, 238), 50)), x*SquareSize.Width + 2, y*SquareSize.Height + 2, SquareSize.Width-2, SquareSize.Height-2);
          }
          else if (isDarkenedSquare)
          {
            g.FillRectangle( new SolidBrush(BlendColor(boardForeColor, Color.FromArgb(197, 106, 49), 50)), x*SquareSize.Width + 1, y*SquareSize.Height + 1, SquareSize.Width, SquareSize.Height);
            g.FillRectangle( new SolidBrush(BlendColor(boardForeColor, Color.FromArgb(238, 210, 193), 50)), x*SquareSize.Width + 2, y*SquareSize.Height + 2, SquareSize.Width-2, SquareSize.Height-2);
          }
          else if (isValidMoveSquare)
          {
            g.FillRectangle( new SolidBrush(BlendColor(boardForeColor, Color.FromArgb(152, 180, 226), 50)), x*SquareSize.Width + 1, y*SquareSize.Height + 1, SquareSize.Width, SquareSize.Height);
            g.FillRectangle( new SolidBrush(BlendColor(boardForeColor, Color.FromArgb(224, 232, 246), 50)), x*SquareSize.Width + 2, y*SquareSize.Height + 2, SquareSize.Width-2, SquareSize.Height-2);
          }
          else
          { g.FillRectangle( brushForeColor, x*SquareSize.Width + 1, y*SquareSize.Height + 1, SquareSize.Width, SquareSize.Height); }
          
          // Do no further drawing if not playing
          if ((!IsPlaying) && (Winner == 0)) continue;
          
          // Draw game pieces
          CheckersPiece piece = game.Board[x, y];
          switch (state)
          {
            case CheckersUIState.Idle: break;
            case CheckersUIState.IdleMoving:
              if (piece == movePiece.Piece)
                piece = null;               // Hide piece being moved
              if (movePiece.CurrentLocation == new Point(x, y))
                piece = movePiece.Piece;    // Show piece in new location
              foreach (CheckersPiece jumped in movePiece.Jumped)
                if (piece == jumped) piece = null;  // Hide jumped (in-progress)
              break;
            case CheckersUIState.DragMoving:
              if (piece == movePiece.Piece)
                piece = null;               // Hide piece being moved
              foreach (CheckersPiece jumped in movePiece.Jumped)
                if (piece == jumped) piece = null;  // Hide jumped (in-progress)
              break;
            case CheckersUIState.ClickMoving:
              goto case CheckersUIState.DragMoving;
            case CheckersUIState.ShowMove:
              piece = movePiece.Game.Board[x, y];
              if (piece == movePiece.Piece)
                piece = null;               // Hide piece being moved
              if (movePiece.CurrentLocation == new Point(x, y))
                piece = movePiece.Piece;    // Show piece in new location
              foreach (CheckersPiece jumped in movePiece.Jumped)
                if (piece == jumped) piece = null;  // Hide jumped (in-progress)
              break;
          }
          // Draw the piece
          if (piece != null)
          {
            Image image = null;
            if (piece.Player == 1)
              image = pieceImages[(( piece.Rank == CheckersRank.Pawn )?( 0 ):( 1 ))];
            else if (piece.Player == 2)
              image = pieceImages[(( piece.Rank == CheckersRank.Pawn )?( 2 ):( 3 ))];
            if (image == null) continue;
            g.DrawImage(image, x*SquareSize.Width + 1, y*SquareSize.Height + 1, 32, 32);
          }
        }
      }
      
      g.Dispose();
      brushForeColor.Dispose();
      brushBackColor.Dispose();
      penGridColor.Dispose();
      base.Refresh();
    }
    
  }
}
