using Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;
using System.Xml.Serialization;

namespace BluffCityCaseDay10A
{
    internal class Program
    {
        private static BaggageTransaction _transaction;

        private static void Main(string[] args)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BaggageTransaction));
            _transaction = (BaggageTransaction)serializer.Deserialize(new XmlTextReader("BaggageTransaction.xml"));

            GateListener[] listeners = new GateListener[5];

            MessageQueuesManager.Instance.CreateQueue(DynamicRouter.Instance, "Dyn");

            listeners[0] = new GateListener("gt1");
            listeners[1] = new GateListener("gt2");
            listeners[2] = new GateListener("gt3");
            listeners[3] = new GateListener("gt4");
            listeners[4] = new GateListener("gt5");

            for (int i = 0; i < listeners.Length; i++)
            {
                MessageQueuesManager.Instance.CreateQueue(listeners[i], listeners[i].Name);
                listeners[i].Setup();
            }

            Console.ReadKey();

            DynamicRouter.Instance.Send(_transaction, 3);

            Timer timer = new Timer(1500);
            timer.AutoReset = true;
            timer.Elapsed += OnTimed;
            timer.Enabled = true;

            Console.ReadKey();
        }

        private static void OnTimed(object sender, ElapsedEventArgs e)
        {
            DynamicRouter.Instance.Send(_transaction, RandomManager.Instance._Randy.Next(1, 42));
        }
    }
}