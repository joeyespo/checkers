using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Checkers
{
  public enum CheckersSounds : int
  {
    [DefaultValue("Begin.wav")]
    Begin = 0,
    
    [DefaultValue("ForceEnd.wav")]
    ForceEnd = 1,
    
    [DefaultValue("Winner.wav")]
    Winner = 2,
    
    [DefaultValue("EndGame.wav")]
    EndGame = 3,
    
    [DefaultValue("Select.wav")]
    Select = 4,
    
    [DefaultValue("Deselect.wav")]
    Deselect = 5,
    
    [DefaultValue("BadMove.wav")]
    BadMove = 6,
    
    [DefaultValue("Drop.wav")]
    Drop = 7,
    
    [DefaultValue("Jump.wav")]
    Jump = 8,
    
    [DefaultValue("JumpMultiple.wav")]
    JumpMultiple = 9,
    
    [DefaultValue("King.wav")]
    King = 10,
    
    [DefaultValue("Lost.wav")]
    Lost = 11,
    
    [DefaultValue("MsgSend.wav")]
    SendMessage = 12,
    
    [DefaultValue("MsgRecv.wav")]
    ReceiveMessage = 13,
  }
  
  [Serializable()]
  public class CheckersSettings
  {
    public static readonly int Port = 6000;
    
    // General settings
    [DefaultValue(true)]
    public bool HighlightSelection;
    
    [DefaultValue(true)]
    public bool HighlightPossibleMoves;
    
    [DefaultValue(true)]
    public bool ShowJumpMessage;
    
    // Net settings
    [DefaultValue(true)]
    public bool FlashWindowOnGameEvents;
    
    [DefaultValue(true)]
    public bool FlashWindowOnTurn;
    
    [DefaultValue(true)]
    public bool FlashWindowOnMessage;
    
    [DefaultValue(true)]
    public bool ShowNetPanelOnMessage;
    
    [DefaultValue(true)]
    public bool ShowTextFeedback;
    
    // Board appearance
    [DefaultValue(typeof(Color), "White")]
    public Color BackColor;
    
    [DefaultValue(typeof(Color), "DarkSeaGreen")]
    public Color BoardBackColor;
    
    [DefaultValue(typeof(Color), "OldLace")]
    public Color BoardForeColor;
    
    [DefaultValue(typeof(Color), "Gray")]
    public Color BoardGridColor;
    
    // Sounds
    public string [] sounds;
    
    [DefaultValue(false)]
    public bool MuteSounds;
    
    
    public CheckersSettings ()
    {
      // Properties
      foreach (FieldInfo field in GetType().GetFields())
      {
        if ((!field.IsPublic) || (field.IsSpecialName)) continue;
        object [] attr = field.GetCustomAttributes(typeof(DefaultValueAttribute), true);
        if (attr.Length == 0) continue;
        field.SetValue(this, ((DefaultValueAttribute)attr[0]).Value);
      }
      // Sounds
      ArrayList soundList = new ArrayList();
      foreach (FieldInfo field in typeof(CheckersSounds).GetFields())
      {
        if ((!field.IsPublic) || (field.IsSpecialName)) continue;
        object [] attr = field.GetCustomAttributes(typeof(DefaultValueAttribute), true);
        if (attr.Length == 0) continue;
        soundList.Add(((DefaultValueAttribute)attr[0]).Value);
      }
      sounds = (string [])soundList.ToArray(typeof(string));
    }
    
    
    public void Save ()
    { Save(Path.GetDirectoryName(Application.ExecutablePath) + "\\Checkers.ini"); }
    public void Save (string fileName)
    {
      FileStream fs = File.OpenWrite(fileName);
      try
      { (new SoapFormatter()).Serialize(fs, this); }
      catch (SerializationException e)
      { MessageBox.Show("Could not save settings:\n" + e.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
      fs.Close();
    }
    public static CheckersSettings Load ()
    { return FromFile(Path.GetDirectoryName(Application.ExecutablePath) + "\\Checkers.ini"); }
    public static CheckersSettings FromFile (string fileName)
    {
      CheckersSettings settings = null;
      if (File.Exists(fileName))
      {
        FileStream fs = File.OpenRead(fileName);
        try
        { settings = (CheckersSettings)(new SoapFormatter()).Deserialize(fs); }
        catch (SerializationException e)
        { MessageBox.Show("Could not load settings:\n" + e.Message + "\n\nUsing default settings.", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        fs.Close();
      }
      if ((settings != null) && ((settings.sounds == null) || (settings.sounds.Length != (new CheckersSettings()).sounds.Length)))
      {
        MessageBox.Show("Settings are corrupt.\n\nUsing default settings.", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        settings = null;
      }
      if (settings == null)
      { settings = new CheckersSettings(); settings.Save(); }
      return settings;
    }
  }
}
