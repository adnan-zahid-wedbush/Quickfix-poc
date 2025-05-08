using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickFix;
using QuickFix.Fields;

namespace FixServer
{
    public class FixServer : IApplication
    {
        public void OnCreate(SessionID sessionID) => Console.WriteLine("Server: Session created");
        public void OnLogon(SessionID sessionID) => Console.WriteLine("Server: Client logged on");
        public void OnLogout(SessionID sessionID) => Console.WriteLine("Server: Client logged out");

        public void FromAdmin(Message message, SessionID sessionID) => Console.WriteLine("Server FromAdmin: " + message);
        public void ToAdmin(Message message, SessionID sessionID) => Console.WriteLine("Server ToAdmin: " + message);
        public void ToApp(Message message, SessionID sessionID) => Console.WriteLine("Server ToApp: " + message);

        public void FromApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("Server FromApp: " + message);

            if (message.Header.GetString(Tags.MsgType) == MsgType.ORDER_SINGLE)
            {
                // Extract fields using Tags

                var clOrdID = new ClOrdID(message.GetString(Tags.ClOrdID));
                var symbol = new Symbol(message.GetString(Tags.Symbol));
                var side = new Side(message.GetChar(Tags.Side));
                var qty = new OrderQty(message.GetDecimal(Tags.OrderQty));

                // Build ExecutionReport
                var execReport = new QuickFix.FIX42.ExecutionReport();
                execReport.Set(new OrderID("ORDER123"));
                execReport.Set(new ExecID("EXEC123"));
                execReport.Set(new ExecTransType(ExecTransType.NEW));
                execReport.Set(new ExecType(ExecType.FILL));
                execReport.Set(new OrdStatus(OrdStatus.FILLED));
                execReport.Set(clOrdID);
                execReport.Set(symbol);
                execReport.Set(side);
                execReport.Set(new LeavesQty(0));
                execReport.Set(new CumQty(qty.getValue()));
                execReport.Set(new AvgPx(150));

                Session.SendToTarget(execReport, sessionID);
            }
        }
    }
}
