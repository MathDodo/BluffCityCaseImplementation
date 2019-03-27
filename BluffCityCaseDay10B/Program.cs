using Data;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace BluffCityCaseDay10B
{
    internal class Program
    {
        private static CBPArrivalInfo _cbpa;

        private static void Main(string[] args)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CBPArrivalInfo));
            _cbpa = (CBPArrivalInfo)serializer.Deserialize(new XmlTextReader("CBPA.xml"));

            MessageQueuesManager.Instance.CreateQueue(RecipientsList.Instance, "RecipientList");

            var recipients = new Recipient[8];
            recipients[0] = new Recipient("DK");
            recipients[1] = new Recipient("US");
            recipients[2] = new Recipient("GE");
            recipients[3] = new Recipient("SE");
            recipients[4] = new Recipient("NO");
            recipients[5] = new Recipient("GB");
            recipients[6] = new Recipient("US");
            recipients[7] = new Recipient("DK");

            for (int i = 0; i < recipients.Length; i++)
            {
                MessageQueuesManager.Instance.CreateQueue(recipients[i], recipients[i].Name + i.ToString());
                recipients[i].Setup();
            }

            Console.ReadKey();

            RecipientsList.Instance.Send(_cbpa);

            Console.ReadKey();
        }
    }
}