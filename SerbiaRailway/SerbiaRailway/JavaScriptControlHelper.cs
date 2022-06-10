using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SerbiaRailway.help
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class JavaScriptControlHelper
    {
        Window window;
        Page page;
        public JavaScriptControlHelper(Window w)
        {
            window = w;
        }
        public JavaScriptControlHelper(Page p)
        {
            page = p;
        }

        public void RunFromJavascript(string param)
        {
            //prozor.doThings(param);
        }
    }
}
