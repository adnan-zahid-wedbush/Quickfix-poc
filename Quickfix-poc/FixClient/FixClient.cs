using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickFix;
using QuickFix.Fields;

namespace FixClient
{
    class FixClient : IApplication
    {
        public void OnCreate(SessionID sessionID) => Console.WriteLine("Client: Session created");

        public void OnLogon(SessionID sessionID)
        {
            Console.WriteLine("Client: Logged on");
            SendTestOrder(sessionID);
        }

        public void OnLogout(SessionID sessionID) => Console.WriteLine("Client: Logged out");

        public void ToAdmin(Message message, SessionID sessionID) => Console.WriteLine("Client ToAdmin: " + message);

        public void FromAdmin(Message message, SessionID sessionID) => Console.WriteLine("Client FromAdmin: " + message);

        public void ToApp(Message message, SessionID sessionID) => Console.WriteLine("Client ToApp: " + message);

        public void FromApp(Message message, SessionID sessionID) => Console.WriteLine("Client FromApp: " + message);

        private void SendTestOrder(SessionID sessionID)
        {
            var order = new QuickFix.FIX42.NewOrderSingle(
                new ClOrdID("TEST001"),
                new HandlInst('1'),
                new Symbol("AAPL"),
                new Side(Side.BUY),
                new TransactTime(DateTime.UtcNow),
                new OrdType(OrdType.MARKET)
            );
            order.Set(new OrderQty(100));

            Console.WriteLine("Sending NewOrderSingle...");
            Session.SendToTarget(order, sessionID);
        }
    }
}
