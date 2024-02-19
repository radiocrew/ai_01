using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace rm_login.Tool
{
    public class TextTester
    {
        static private readonly Lazy<TextTester> s_lazy = new Lazy<TextTester>(() => new TextTester());
        static public TextTester Instance { get { return s_lazy.Value; } }

        private TextTester()
        {

        }

        static private void OnTimer(object status)
        {

        }

           
    }
}
