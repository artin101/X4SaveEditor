using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace X4SaveEditor
{
    public class Station : Component
    {

        private ObservableCollection<XElement> _orders = new ObservableCollection<XElement>();
        public ObservableCollection<XElement> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged("Orders");
            }
        }

        private ObservableCollection<XElement> _offers = new ObservableCollection<XElement>();
        public ObservableCollection<XElement> Offers
        {
            get { return _offers; }
            set
            {
                _offers = value;
                OnPropertyChanged("Offers");
            }
        }

        private ObservableCollection<CrewMember> _crew = new ObservableCollection<CrewMember>();
        public ObservableCollection<CrewMember> Crew
        {
            get { return _crew; }
            set
            {
                _crew = value;
                OnPropertyChanged("Crew");
            }
        }

        public Station(XElement station)
            : base(station)
        {

            var crewquery = from c in Data.Descendants().Elements("component")
                            where c.Attribute("class").Name == "npc"
                            select (c);

            foreach (var person in crewquery)
            {
                _crew.Add(new CrewMember(person));
            }

            foreach (var order in Data.Elements("trade").Elements("orders").Elements("trade"))
            {
                Orders.Add(order);
            }

            foreach (var offer in Data.Elements("trade").Elements("offers").Descendants().Elements("trade"))
            {
                Offers.Add(offer);
            }
        }

        public void AddCrewAttributeNode(string attribute, CrewMember crewMember)
        {
            if (Crew.Contains(crewMember))
            {
                crewMember.AddAttributeNode(attribute);
            }
        }

    }
}