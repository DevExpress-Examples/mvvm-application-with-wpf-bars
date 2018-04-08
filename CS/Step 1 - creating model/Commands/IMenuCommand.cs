using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Data;
using DevExpress.Xpf.Bars.Native;

namespace DXBarsAndMVVM.Commands {
    public interface IMenuCommand {
        string Caption { get; }
        Uri Glyph { get; }
        Binding Binding { get; }
        ICommand Command { get; }
        object CommandParameter { get; }
        
    }
}
