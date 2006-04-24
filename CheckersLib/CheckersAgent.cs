using System;

namespace Uberware.Gaming.Checkers
{
  #region AgentTick Class and Delegate
  
  public class AgentTickEventArgs : EventArgs
  {
    CheckersGame game;
    object tag;
    
    public AgentTickEventArgs ()
    { this.game = null; }
    public AgentTickEventArgs (CheckersGame game)
    { this.game = game; }
    
    public CheckersGame Game
    {
      get { return game; }
      set { game = value; }
    }
    
    public object Tag
    {
      get { return tag; }
      set { tag = value; }
    }
  }
  
  public delegate void AgentTickEventHandler (object sender, AgentTickEventArgs e);
  
  #endregion
  
  public abstract class CheckersAgent
  {
    public event AgentTickEventHandler Tick;
    
    public CheckersAgent ()
    {}
    
    /// <summary>Returns the best possible next move using the current move-finding technique.</summary>
    /// <returns>The next best possible move.</returns>
    public abstract CheckersMove NextMove (CheckersGame game);
    
    protected void OnTick (CheckersGame game)
    { if (Tick != null) Tick(this, new AgentTickEventArgs(game)); }
  }
}
