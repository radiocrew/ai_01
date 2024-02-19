using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using rm_login.Network;
using rm_login.Tool;
using rm_login.Tool.Script;
using rm_login.Tool.Contents;

namespace rm_login
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("[{0}] Tool preparing...", System.DateTime.Now.ToString("hh:mm:ss.fff"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form1 = new Form1();

            //-manager initialize
            LogDelegate.Instance.Initialize();
            ArenaServerSession.Instance.Initialize();
            Player.Instance.Initialize();
            ArenaObjectManager.Instance.Initialize();
            MapManager.Instance.Initialize(form1.mapBox);
            DrawManager.Instance.Initialize(form1.mapBox);
            FormDelegate.Instance.Initialize();
            ResourceManager.Instance.Initialize();
            LevelManager.Instance.Initialize();
            TestCommandManager.Instance.Initialize();

            Console.WriteLine("[{0}] Tool Initialize completed", System.DateTime.Now.ToString("hh:mm:ss.fff"));
            
            //-delegate
            LogDelegate.Instance.LogEvent += form1.LogFunction;
            FormDelegate.Instance.UIEvent += form1.UIEvent;




            form1.Initialize();

            Application.Run(form1);
        }
    }
}
