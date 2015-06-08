using System;
using System.Diagnostics;

namespace XQShelfLauncher
{
    public class ProcessStarter
    {
        public void Start(string path)
        {
            Process myProcess = new Process();

            try
            {
                myProcess.StartInfo.UseShellExecute = false;
                // You can start any process, HelloWorld is a do-nothing example.
                myProcess.StartInfo.FileName = path;
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.Start();
                // This code assumes the process you are starting will terminate itself.  
                // Given that is is started without a window so you cannot terminate it  
                // on the desktop, it must terminate itself or you can do it programmatically 
                // from this application using the Kill method.
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
