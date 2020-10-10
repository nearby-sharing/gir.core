using System;
using GObject;

namespace Gtk
{
    public partial class Widget
    {
        public EventHandler<SignalArgs> this[Signal signal]
        {
            set => signal.Connect(this, value, true);
        }
        
        public void ShowAll() => Native.show_all(Handle);
    }
}