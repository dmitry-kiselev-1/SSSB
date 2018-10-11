using System;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.ServiceProcess;
using System.Threading;
using China.ICBC.SWIFT;
using Timer = System.Timers.Timer;

namespace ICBC.Service
{
    public partial class Sender : ServiceBase
    {
        public Sender()
        {
            InitializeComponent();
        }

        readonly Timer _timer = new Timer();

        protected override void OnStart(string[] args)
        {
            string outputDirectory = Settings.Default.outputDirectory;
            string sqlConnectionString = Settings.Default.sqlConnectionString;
            int sendMessageCount = Settings.Default.sendMessageCount;
            int sendDelayInHour = Settings.Default.sendDelayInHour;
            int checkIntervalInSecond = Settings.Default.checkIntervalInSecond;
            string logPath = Settings.Default.logPath;
            int maxLogSizeInKb = Settings.Default.maxLogSizeInKb;

            var log = new FileInfo(logPath);

            if (log.Exists && log.Length > maxLogSizeInKb)
            {
                File.Delete(logPath);
            }

            _timer.Interval = checkIntervalInSecond * 100;
            _timer.Start();

            _timer.Elapsed += (sender, eventArgs) =>
            {
                try
                {
                    MT103.Send(sqlConnectionString, new DirectoryInfo(outputDirectory), sendMessageCount, sendDelayInHour);
                }
                catch (Exception e)
                {
                    File.AppendAllText(logPath, DateTime.Now + "    " + e.ToString() + "\n\n");
                }
            };
        }

        protected override void OnStop()
        {
            _timer.Stop();
        }
    }
}
