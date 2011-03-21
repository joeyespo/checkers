using System;
using System.Windows.Forms.Design;

namespace Checkers.UI
{
    class CheckersDesigner : ControlDesigner
    {
        public override SelectionRules SelectionRules
        {
            get
            {
                return SelectionRules.Visible | SelectionRules.Moveable;
            }
        }
    }
}
