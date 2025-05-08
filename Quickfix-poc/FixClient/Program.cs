using QuickFix.Logger;
using QuickFix.Store;
using QuickFix;
using QuickFix.Transport;

namespace FixClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var settings = new SessionSettings("C:\\Users\\ZahidA.WEDBUSH\\Documents\\GitHub\\Quickfix-poc\\Quickfix-poc\\FixClient\\client.cfg");
                var app = new FixClient();
                var storeFactory = new FileStoreFactory(settings);
                var logFactory = new FileLogFactory(settings);
                var initiator = new SocketInitiator(app, storeFactory, settings, logFactory);

                initiator.Start();
                Console.WriteLine("FIX Client running. Press Enter to quit.");
                Console.ReadLine();
                initiator.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
