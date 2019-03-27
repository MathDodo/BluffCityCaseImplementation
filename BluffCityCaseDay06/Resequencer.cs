using Data;
using System;
using System.Messaging;
using System.Collections.Generic;
using Setup;

namespace BluffCityCaseDay06
{
    public sealed class Resequencer : SingletonBase<Resequencer>, IReceiver<Tuple<int, int, Flight, Passenger>>, IReceiver<Tuple<int, int, string, Flight, Luggage>>
    {
        private List<AirlineCompany> _airline;
        private Dictionary<Type, MessageQueue> _queues;
        private Dictionary<string, List<Tuple<int, bool, object, MessageQueue>>> _messageSequences;

        public string Name => "Resequencer";

        public MessageQueue this[Type targetType]
        {
            get
            {
                return _queues[targetType];
            }
        }

        private Resequencer()
        {
            _airline = new List<AirlineCompany>();
            _queues = new Dictionary<Type, MessageQueue>();
            _messageSequences = new Dictionary<string, List<Tuple<int, bool, object, MessageQueue>>>();
        }

        public void AddAirline(AirlineCompany company)
        {
            _airline.Add(company);
        }

        public void ReceivingQueue(Type queueReceiveType, MessageQueue messageQueue)
        {
            _queues.Add(queueReceiveType, messageQueue);
        }

        void IReceiver<Tuple<int, int, Flight, Passenger>>.OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            var value = (Tuple<int, int, Flight, Passenger>)m.Body;

            if (!_messageSequences.ContainsKey(value.Item4.ReservationNumber))
            {
                _messageSequences.Add(value.Item4.ReservationNumber, new List<Tuple<int, bool, object, MessageQueue>>());
            }

            _messageSequences[value.Item4.ReservationNumber].Add(new Tuple<int, bool, object, MessageQueue>(value.Item2, false, value, _airline.Find(a => a.CompanyName == m.Label)
                [
                    typeof(Tuple<int, int, Flight, Passenger>)
                ]));

            _messageSequences[value.Item4.ReservationNumber].Sort((x, y) => x.Item1.CompareTo(y.Item1));

            for (int i = 0; i < _messageSequences[value.Item4.ReservationNumber].Count; i++)
            {
                if (_messageSequences[value.Item4.ReservationNumber][i].Item1 == i && !_messageSequences[value.Item4.ReservationNumber][i].Item2)
                {
                    _messageSequences[value.Item4.ReservationNumber][i] = new Tuple<int, bool, object, MessageQueue>(_messageSequences[value.Item4.ReservationNumber][i].Item1, true,
                        _messageSequences[value.Item4.ReservationNumber][i].Item3, _messageSequences[value.Item4.ReservationNumber][i].Item4);

                    MessageQueuesManager.Instance.SendMessage(_messageSequences[value.Item4.ReservationNumber][i].Item3, m.Label, _messageSequences[value.Item4.ReservationNumber][i].Item4);
                }
            }

            if (_messageSequences[value.Item4.ReservationNumber].Count == value.Item1)
            {
                _messageSequences.Remove(value.Item4.ReservationNumber);
            }

            mq.BeginReceive();
        }

        void IReceiver<Tuple<int, int, string, Flight, Luggage>>.OnReceive(object messageQueue, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)messageQueue;

            // End the asynchronous receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            var value = (Tuple<int, int, string, Flight, Luggage>)m.Body;

            if (!_messageSequences.ContainsKey(value.Item3))
            {
                _messageSequences.Add(value.Item3, new List<Tuple<int, bool, object, MessageQueue>>());
            }

            _messageSequences[value.Item3].Add(new Tuple<int, bool, object, MessageQueue>(value.Item2, false, value, _airline.Find(a => a.CompanyName == m.Label)
                        [
                            typeof(Tuple<int, int, string, Flight, Luggage>)
                        ]));

            _messageSequences[value.Item3].Sort((x, y) => x.Item1.CompareTo(y.Item1));

            for (int i = 0; i < _messageSequences[value.Item3].Count; i++)
            {
                if (_messageSequences[value.Item3][i].Item1 == i && !_messageSequences[value.Item3][i].Item2)
                {
                    _messageSequences[value.Item3][i] = new Tuple<int, bool, object, MessageQueue>(_messageSequences[value.Item3][i].Item1, true,
                       _messageSequences[value.Item3][i].Item3, _messageSequences[value.Item3][i].Item4);

                    MessageQueuesManager.Instance.SendMessage(_messageSequences[value.Item3][i].Item3, m.Label, _messageSequences[value.Item3][i].Item4);
                }
            }

            if (_messageSequences[value.Item3].Count == value.Item1)
            {
                _messageSequences.Remove(value.Item3);
            }

            mq.BeginReceive();
        }
    }
}