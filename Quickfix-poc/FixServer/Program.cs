using QuickFix.Logger;
using QuickFix.Store;
using QuickFix;

namespace FixServer
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var settings = new SessionSettings("server.cfg");
            var app = new FixServer();
            var storeFactory = new FileStoreFactory(settings);
            var logFactory = new FileLogFactory(settings);
            var acceptor = new ThreadedSocketAcceptor(app, storeFactory, settings, logFactory);

            acceptor.Start();
            Console.WriteLine("FIX Server running. Press Enter to stop.");
            Console.ReadLine();
            acceptor.Stop();
        }
    }
}
