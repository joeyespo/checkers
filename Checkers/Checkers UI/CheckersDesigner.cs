using System;
using System.Windows.Forms.Design;

namespace Uberware.Gaming.Checkers.UI
{
  class CheckersDesigner : ControlDesigner
  {
    public override SelectionRules SelectionRules
    { get { return SelectionRules.Visible | SelectionRules.Moveable; } }
  }
}
