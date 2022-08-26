using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace X4SaveEditor
{
    public class CrewMember : INotifyPropertyChanged
    {

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private XElement _member;
        public XElement Member
        {
            get { return _member; }
            set
            {
                _member = value;
                OnPropertyChanged("Member");
            }
        }

        private XElement _boarding;
        public XElement Boarding
        {
            get { return _boarding; }
            set
            {
                _boarding = value;
                OnPropertyChanged("Boarding");
            }
        }
        private XElement _combat;
        public XElement Combat
        {
            get { return _combat; }
            set
            {
                _combat = value;
                OnPropertyChanged("Combat");
            }
        }
        private XElement _engineering;
        public XElement Engineering
        {
            get { return _engineering; }
            set
            {
                _engineering = value;
                OnPropertyChanged("Engineering");
            }
        }
        private XElement _navigation;
        public XElement Navigation
        {
            get { return _navigation; }
            set
            {
                _navigation = value;
                OnPropertyChanged("Navigation");
            }
        }
        private XElement _leadership;
        public XElement Leadership
        {
            get { return _leadership; }
            set
            {
                _leadership = value;
                OnPropertyChanged("Leadership");
            }
        }
        private XElement _morale;
        public XElement Morale
        {
            get { return _morale; }
            set
            {
                _morale = value;
                OnPropertyChanged("Morale");
            }
        }
        private XElement _science;
        public XElement Science
        {
            get { return _science; }
            set
            {
                _science = value;
                OnPropertyChanged("Science");
            }
        }
        private XElement _management;
        public XElement Management
        {
            get { return _management; }
            set
            {
                _management = value;
                OnPropertyChanged("Management");
            }
        }


        public CrewMember(XElement member)
        {
            Member = member;

            var skillquery = from skill in Member.Elements("skill") select skill;

            foreach (XElement skill in skillquery)
            {
                switch (skill.Attribute("type").Value)
                {
                    case "boarding":
                        Boarding = skill;
                        break;
                    case "combat":
                        Combat = skill;
                        break;
                    case "engineering":
                        Engineering = skill;
                        break;
                    case "navigation":
                        Navigation = skill;
                        break;
                    case "leadership":
                        Leadership = skill;
                        break;
                    case "morale":
                        Morale = skill;
                        break;
                    case "science":
                        Science = skill;
                        break;
                    case "management":
                        Management = skill;
                        break;
                }
            }
        }

        public void AddAttributeNode(string attribute)
        {
            var skill = new XElement("skill", new XAttribute("type", attribute), new XAttribute("value", "0"));
            Member.Elements("skills").First().Add(skill);

            switch (attribute)
            {
                case "boarding":
                    Boarding = skill;
                    break;
                case "combat":
                    Combat = skill;
                    break;
                case "engineering":
                    Engineering = skill;
                    break;
                case "navigation":
                    Navigation = skill;
                    break;
                case "leadership":
                    Leadership = skill;
                    break;
                case "morale":
                    Morale = skill;
                    break;
                case "science":
                    Science = skill;
                    break;
                case "management":
                    Management = skill;
                    break;
            }
        }
    }
}
