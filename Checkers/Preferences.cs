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
    
    [DefaultValue("EndGame.wav")]
    EndGame = 1,
    
    [DefaultValue("Stalemate.wav")]
    Stalemate = 2,
    
    [DefaultValue("Jump.wav")]
    Jump = 3,
    
    [DefaultValue("Select.wav")]
    Select = 4,
  }
  
  [Serializable()]
  public class CheckersSettings
  {
    // Net settings
    [DefaultValue(true)]
    public bool FlashWindowOnTurn;
    
    [DefaultValue(true)]
    public bool FlashWindowOnMessage;
    
    [DefaultValue(true)]
    public bool ShowNetPanelOnMessage;
    
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
    { Save("Checkers.ini"); }
    public void Save (string fileName)
    {
      FileStream fs = File.OpenWrite("Checkers.ini");
      try
      { (new SoapFormatter()).Serialize(fs, this); }
      catch (SerializationException e)
      { MessageBox.Show("Could not save settings:\n" + e.Message, "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
      fs.Close();
    }
    public static CheckersSettings Load ()
    { return FromFile("Checkers.ini"); }
    public static CheckersSettings FromFile (string fileName)
    {
      if (!File.Exists("Checkers.ini")) return new CheckersSettings();
      FileStream fs = File.OpenRead("Checkers.ini");
      CheckersSettings settings = null;
      try
      { settings = (CheckersSettings)(new SoapFormatter()).Deserialize(fs); }
      catch (SerializationException e)
      { MessageBox.Show("Could not load settings:\n" + e.Message + "\n\nUsing default settings.", "Checkers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
      fs.Close();
      if (settings == null)
      { settings = new CheckersSettings(); settings.Save(); }
      return settings;
    }
  }
}
