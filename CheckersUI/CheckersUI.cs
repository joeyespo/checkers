using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Windows.Forms;

internal class ResFinder
{}

namespace Uberware.Gaming.Checkers.UI
{
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof(ResFinder), "Uberware.Gaming.Checkers.UI.Images.ToolboxBitmap.png")]
  [Designer(typeof(CheckersDesigner))]
  public class CheckersUI : System.Windows.Forms.UserControl
  {
    public static readonly Size SquareSize = new Size(32, 32);
    public static readonly Size BoardPixelSize = new Size(SquareSize.Width*CheckersGame.BoardSize.Width, SquareSize.Height*CheckersGame.BoardSize.Height);
    
    public event EventHandler GameStarted;
    public event EventHandler GameStopped;
    
    private Bitmap boardImage;
    
    private CheckersGame game = new CheckersGame();
    private BorderStyle borderStyle = BorderStyle.Fixed3D;
    private int boardMargin = 4;
    private Color boardBackColor = Color.DarkSeaGreen;
    private Color boardForeColor = Color.OldLace;
    private Color boardGridColor = Color.Gray;
    private System.Windows.Forms.ImageList imlPieces;
    private System.ComponentModel.IContainer components;
    private Image [] customPieceImages = new Image [4];
    private Image [] pieceImages = new Image [4];
    private Cursor [] pieceCursors = new Cursor [4];
    private bool player1Active = true;
    private bool player2Active = true;
    private bool highlightSquares = true;
    private CheckersPiece holding = null;
    private Point [] validMoves = new Point [0];
    
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
      boardImage = new Bitmap(ClientSize.Width, ClientSize.Height, PixelFormat.Format32bppArgb);
      Refresh();
    }
    
		#region Component Designer generated code
		
    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.imlPieces = new System.Windows.Forms.ImageList(this.components);
      // 
      // imlPieces
      // 
      this.imlPieces.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      this.imlPieces.ImageSize = new System.Drawing.Size(32, 32);
      this.imlPieces.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // CheckersUI
      // 
      this.BackColor = System.Drawing.Color.White;
      this.Name = "CheckersUI";
      this.Size = new System.Drawing.Size(360, 276);
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
    
    [DefaultValue(typeof(Color), "White")]
    public override Color BackColor
    {
      get { return base.BackColor; }
      set { base.BackColor = value; Refresh(); }
    }
    
    public bool IsPlaying
    { get { return game.IsPlaying; } }
    
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
      set { player1Active = value; }        // !!!!! Drop piece if being held
    }
    [DefaultValue(true)]
    public bool Player2Active
    {
      get { return player2Active; }
      set { player2Active = value; }        // !!!!! Drop piece if being held
    }
    
    [DefaultValue(true)]
    public bool HighlightSquares
    {
      get { return highlightSquares; }
      set { highlightSquares = true; }      // !!!!! Check to see if piece is being held & highlight it
    }
    
    #endregion
    
    
    protected override void SetBoundsCore (int x, int y, int width, int height, BoundsSpecified specified)
    {
      int borderSize = (( borderStyle == BorderStyle.Fixed3D )?( 2 ):( (( borderStyle == BorderStyle.FixedSingle )?( 1 ):( 0 )) ));
      base.SetBoundsCore(x, y, BoardPixelSize.Width+2+(borderSize*2)+(boardMargin*2), BoardPixelSize.Height+2+(borderSize*2)+(boardMargin*2), specified);
    }
    
    private void CheckersUI_Paint (object sender, System.Windows.Forms.PaintEventArgs e)
    { if (boardImage != null) e.Graphics.DrawImage(boardImage, 0, 0, boardImage.Width, boardImage.Height); }
    
    private Point highlightedSquare = Point.Empty;
    private void CheckersUI_MouseMove (object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (!IsPlaying) return;
      // Get piece location (hit-test)
      CheckersPiece piece = PieceAt(new Point(e.X, e.Y));
      if (holding != null) return;  // !!!!!
      bool doHighlight = true;
      if ((game.Turn == 1) && (!player1Active)) doHighlight = false;
      if ((game.Turn == 2) && (!player2Active)) doHighlight = false;
      if ((piece == null) || (!game.CanMovePiece(piece))) doHighlight = false;
      if (!doHighlight)
      {
        if (!highlightedSquare.IsEmpty)
        { highlightedSquare = Point.Empty; Refresh(); }
        return;
      }
      // Highlight board
      if ((!highlightedSquare.IsEmpty) && (highlightedSquare == piece.Location)) return;
      highlightedSquare = piece.Location;
      Refresh();
    }
    private void CheckersUI_MouseLeave (object sender, System.EventArgs e)
    {
      if (highlightedSquare.IsEmpty) return;
      highlightedSquare = Point.Empty;
      Refresh();
    }
    
    private void CheckersUI_MouseDown (object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if ((!IsPlaying) || (e.Button != MouseButtons.Left)) return;
      // Get piece location (hit-test)
      holding = PieceAt(new Point(e.X, e.Y));
      if ((holding == null) || (!game.CanMovePiece(holding))) return;
      highlightedSquare = Point.Empty;
      Capture = true;
      Cursor = pieceCursors[(holding.Player-1)*2 + (( holding.Rank == CheckersRank.Pawn )?( 0 ):( 1 ))];
      CheckersPiece [] pieces = game.EnumMovablePieces();
      validMoves = new Point [pieces.Length];
      for (int i = 0; i < validMoves.Length; i++)
        validMoves[i] = pieces[i].Location;
      Refresh();
    }
    
    private void CheckersUI_MouseUp (object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if ((holding == null) || (e.Button != MouseButtons.Left)) return;
      Cursor = Cursors.Arrow;
      Capture = false;
      holding = null;
      validMoves = new Point [0];
      CheckersUI_MouseMove(sender, e);
      Refresh();
    }
    
    /// <summary> Refreshes the Checkers baord and the control.</summary>
    public override void Refresh ()
    {
      if (boardImage == null) return;
      Graphics g = Graphics.FromImage(boardImage);
      Pen penGridColor = new Pen(boardGridColor);
      Brush brushBackColor = new SolidBrush(boardBackColor);
      Brush brushForeColor = new SolidBrush(boardForeColor);
      
      g.Clear(BackColor);
      int borderSize = 0;
      if (borderStyle == BorderStyle.Fixed3D)
      {
        Pen penLight = new Pen(Color.FromKnownColor(KnownColor.ControlLight)); Pen penLightLight = new Pen(Color.FromKnownColor(KnownColor.ControlLightLight));
        Pen penDark = new Pen(Color.FromKnownColor(KnownColor.ControlDark)); Pen penDarkDark = new Pen(Color.FromKnownColor(KnownColor.ControlDarkDark));
        //
        g.DrawLine(penDark, 0, 0, Width-2, 0); g.DrawLine(penDarkDark, 0, 1, Width-3, 1);
        g.DrawLine(penDark, 0, 1, 0, Height-2); g.DrawLine(penDarkDark, 1, 2, 1, Height-3);
        g.DrawLine(penLightLight, 0, Height-1, Width-1, Height-1); g.DrawLine(penLight, 1, Height-2, Width-1, Height-2);
        g.DrawLine(penLightLight, Width-1, 0, Width-1, Height-1); g.DrawLine(penLight, Width-2, 1, Width-2, Height-2);
        //
        penLight.Dispose(); penLightLight.Dispose();
        penDark.Dispose(); penDarkDark.Dispose();
        borderSize = 2;
      }
      else if (borderStyle == BorderStyle.FixedSingle)
      {
        Pen penFrame = new Pen(Color.FromKnownColor(KnownColor.WindowFrame));
        
        g.DrawLine(penFrame, 0, 0, Width, 0);
        g.DrawLine(penFrame, 0, 0, 0, Height);
        g.DrawLine(penFrame, 0, Height-1, Width, Height-1);
        g.DrawLine(penFrame, Width-1, 0, Width-1, Height);
        
        penFrame.Dispose();
        borderSize = 1;
      }
      
      g.DrawRectangle(penGridColor, boardMargin+borderSize, boardMargin+borderSize, BoardPixelSize.Width+1, BoardPixelSize.Height+1);
      g.FillRectangle(brushBackColor, boardMargin+borderSize+1, boardMargin+borderSize+1, BoardPixelSize.Width, BoardPixelSize.Height);
      for (int y = 0; y < CheckersGame.BoardSize.Height; y++)
      {
        for (int x = 0; x < CheckersGame.BoardSize.Width; x++)
        {
          if ((x % 2) == (y % 2)) continue;
          CheckersPiece piece = game.Board[x, y];
          bool validMoveSquare = false;
          foreach (Point p in validMoves) if ((p.X == x) && (p.Y == y)) { validMoveSquare = true; break; }
          if ((highlightedSquare.X == x) && (highlightedSquare.Y == y) && ((holding == null) || (piece != holding)))
          {
            Brush brushForeColorDarken = new SolidBrush(BlendColor(boardForeColor, Color.FromArgb(49, 106, 197), 50));
            Brush brushForeColorHighlight = new SolidBrush(BlendColor(boardForeColor, Color.FromArgb(193, 210, 238), 50));
            g.FillRectangle( brushForeColorDarken, x*SquareSize.Width + boardMargin+borderSize+1, y*SquareSize.Height + boardMargin+borderSize+1, SquareSize.Width, SquareSize.Height);
            g.FillRectangle( brushForeColorHighlight, x*SquareSize.Width + boardMargin+borderSize+2, y*SquareSize.Height + boardMargin+borderSize+2, SquareSize.Width-2, SquareSize.Height-2);
            brushForeColorHighlight.Dispose(); brushForeColorDarken.Dispose();
          }
          else if (validMoveSquare)
          {
            Brush brushForeColorDarken = new SolidBrush(BlendColor(boardForeColor, Color.ForestGreen, 50));
            Brush brushForeColorHighlight = new SolidBrush(BlendColor(boardForeColor, Color.PaleGreen, 50));
            g.FillRectangle( brushForeColorDarken, x*SquareSize.Width + boardMargin+borderSize+1, y*SquareSize.Height + boardMargin+borderSize+1, SquareSize.Width, SquareSize.Height);
            g.FillRectangle( brushForeColorHighlight, x*SquareSize.Width + boardMargin+borderSize+2, y*SquareSize.Height + boardMargin+borderSize+2, SquareSize.Width-2, SquareSize.Height-2);
            brushForeColorHighlight.Dispose(); brushForeColorDarken.Dispose();
          }
          else
          { g.FillRectangle( brushForeColor, x*SquareSize.Width + boardMargin+borderSize+1, y*SquareSize.Height + boardMargin+borderSize+1, SquareSize.Width, SquareSize.Height); }
          if ((piece != null) && (piece != holding))
          {
            if (piece.Player == 1)
            { g.DrawImage(pieceImages[0], x*SquareSize.Width + boardMargin+borderSize+1, y*SquareSize.Height + boardMargin+borderSize+1, 32, 32); }
            else if (piece.Player == 2)
            { g.DrawImage(pieceImages[2], x*SquareSize.Width + boardMargin+borderSize+1, y*SquareSize.Height + boardMargin+borderSize+1, 32, 32); }
          }
        }
      }
      
      brushForeColor.Dispose();
      brushBackColor.Dispose();
      penGridColor.Dispose();
      g.Dispose();
      base.Refresh();
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
    
    /// <summary> Returns a CheckersPiece object that occupies a location on the control's Checkers board. </summary>
    /// <param name="location">The location (in pixels) to retrieve the piece at.</param>
    /// <returns>The CheckersPiece object that occupies the specified location (or null if out-of-bounds).</returns>
    public CheckersPiece PieceAt (Point location)
    {
      Point p = PointToGame(location);
      if ((p.X < 0) || (p.X >= CheckersGame.BoardSize.Width) || (p.Y < 0) || (p.Y >= CheckersGame.BoardSize.Height))
        //throw new ArgumentOutOfRangeException("location", location, "Location was not a valid location on the board");
        return null;
      return game.Board[p.X, p.Y];
    }
    
    /// <summary> Begins a Checkers game. </summary>
    public void Play ()
    {
      game.Play();
      CreateImages();
      Refresh();
      if (GameStarted != null) GameStarted(this, EventArgs.Empty);
    }
    
    /// <summary> Stops a Checkers game. </summary>
    public void Stop ()
    {
      game.Stop();
      Refresh();
      if (GameStopped != null) GameStopped(this, EventArgs.Empty);
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
  }
}
