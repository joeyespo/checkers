using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace Uberware.Gaming.Checkers.UI
{
	[Designer(typeof(CheckersDesigner))]
  public class CheckersUI : System.Windows.Forms.UserControl
	{
    
    public static readonly int SquareSize = 32;
    public static readonly int BoardSize = SquareSize*CheckersGame.SquareCount;
    
    #region Class Variables
    
    private CheckersGame game = new CheckersGame();
    private BorderStyle borderStyle = BorderStyle.Fixed3D;
    private int boardMargin = 4;
    private Color boardBackColor = Color.DarkSeaGreen;
    private Color boardForeColor = Color.OldLace;
    private Color boardGridColor = Color.Gray;
    
    /// <summary> Required designer variable. </summary>
		private System.ComponentModel.Container components = null;
    
    #endregion
    
    #region Class Construction
    
		public CheckersUI ()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
      SetStyle(ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.DoubleBuffer, true);
      SetStyle(ControlStyles.FixedWidth, false);
      SetStyle(ControlStyles.FixedHeight, true);
      SetStyle(ControlStyles.ResizeRedraw, false);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    }
    
		#region Component Designer generated code
    
    /// <summary> Clean up any resources being used. </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if(components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }
		
    /// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      // 
      // CheckersUI
      // 
      this.BackColor = System.Drawing.Color.White;
      this.Name = "CheckersUI";
      this.Size = new System.Drawing.Size(360, 276);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.CheckersUI_Paint);

    }
		
    #endregion
    
    #endregion
    
    #region Class Properties
    
    public event EventHandler GameStarted
    {
      add { game.Started += value; }
      remove { game.Started -= value; }
    }
    public event EventHandler GameStopped
    {
      add { game.Started += value; }
      remove { game.Started -= value; }
    }
    
    [DefaultValue(BorderStyle.Fixed3D)]
    public BorderStyle BorderStyle
    {
      get { return borderStyle; }
      set { borderStyle = value; this.SetBoundsCore(this.Location.X, this.Location.Y, 0, 0, BoundsSpecified.Size); this.Refresh(); }
    }
    
    [DefaultValue(typeof(Color), "White")]
    public override Color BackColor
    {
      get { return base.BackColor; }
      set { base.BackColor = value; }
    }
    
    public bool IsPlaying
    { get { return game.IsPlaying; } }
    
    [DefaultValue(4)]
    public int BoardMargin
    {
      get { return boardMargin; }
      set { boardMargin = value; this.SetBoundsCore(this.Location.X, this.Location.Y, 0, 0, BoundsSpecified.Size); this.Refresh(); }
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
      set { boardBackColor = value; this.Refresh(); }
    }
    
    [DefaultValue(typeof(Color), "OldLace")]
    public Color BoardForeColor
    {
      get { return boardForeColor; }
      set { boardForeColor = value; this.Refresh(); }
    }
    
    [DefaultValue(typeof(Color), "Gray")]
    public Color BoardGridColor
    {
      get { return boardGridColor; }
      set { boardGridColor = value; this.Refresh(); }
    }
    
    #endregion
    
    
    protected override void SetBoundsCore (int x, int y, int width, int height, BoundsSpecified specified)
    {
      int borderSize = (( borderStyle == BorderStyle.Fixed3D )?( 2 ):( (( borderStyle == BorderStyle.FixedSingle )?( 1 ):( 0 )) ));
      base.SetBoundsCore(x, y, BoardSize+2+(borderSize*2)+(boardMargin*2), BoardSize+2+(borderSize*2)+(boardMargin*2), specified);
    }
    
    private void CheckersUI_Paint (object sender, System.Windows.Forms.PaintEventArgs e)
    {
      Pen penGridColor = new Pen(boardGridColor);
      Brush brushBackColor = new SolidBrush(boardBackColor);
      Brush brushForeColor = new SolidBrush(boardForeColor);
      
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
        
        e.Graphics.DrawLine(penFrame, 0, 0, Width, 0);
        e.Graphics.DrawLine(penFrame, 0, 0, 0, Height);
        e.Graphics.DrawLine(penFrame, 0, Height-1, Width, Height-1);
        e.Graphics.DrawLine(penFrame, Width-1, 0, Width-1, Height);
        
        penFrame.Dispose();
        borderSize = 1;
      }
      
      e.Graphics.DrawRectangle(penGridColor, boardMargin+borderSize, boardMargin+borderSize, BoardSize+1, BoardSize+1);
      e.Graphics.FillRectangle(brushBackColor, boardMargin+borderSize+1, boardMargin+borderSize+1, BoardSize, BoardSize);
      
      for (int y = 0; y < CheckersGame.SquareCount; y++)
      {
        for (int x = 0; x < CheckersGame.SquareCount; x++)
        {
          if ((x % 2) == (y % 2)) continue;
          e.Graphics.FillRectangle(brushForeColor, x*SquareSize + boardMargin+borderSize+1, y*SquareSize + boardMargin+borderSize+1, SquareSize, SquareSize);
          CheckersPiece piece = game.GetPiece(new Point(x, y));
          if (piece != null)
          {
            if (piece.Player == 1)
            {
              // !!!!!
              e.Graphics.FillEllipse(Brushes.DarkRed, x*SquareSize + boardMargin+borderSize+1, y*SquareSize + boardMargin+borderSize+1, SquareSize-1, SquareSize-1);
            }
            else if (piece.Player == 2)
            {
              // !!!!!
              e.Graphics.FillEllipse(Brushes.DarkRed, x*SquareSize + boardMargin+borderSize+1, y*SquareSize + boardMargin+borderSize+1, SquareSize-1, SquareSize-1);
            }
          }
        }
      }
      
      brushForeColor.Dispose();
      brushBackColor.Dispose();
      penGridColor.Dispose();
    }
    
    public void Play ()
    { game.Play(); }
    
    public void Stop ()
    { game.Stop(); }
    
  }
}
