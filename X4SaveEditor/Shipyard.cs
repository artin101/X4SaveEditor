using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace X4SaveEditor
{
    public class Shipyard : Station
    {

        private ObservableCollection<Ship> _shipBuildQueue = new ObservableCollection<Ship>();
        public ObservableCollection<Ship> ShipBuildQueue
        {
            get { return _shipBuildQueue; }
            set
            {
                _shipBuildQueue = value;
                OnPropertyChanged("ShipBuildQueue");
            }
        }

        public Shipyard(XElement shipyard)
            : base(shipyard)
        {
            var buildmodules =
                Data.Descendants()
                    .Elements("connection")
                    .Where(
                        buildmodule =>
                            buildmodule.Attribute("connection") != null &&
                            buildmodule.Attribute("connection").Value.Contains("buildmodule"));

            var shipQueue =
                buildmodules.Descendants()
                    .Elements("component")
                    .Where(ship => ship.Attribute("class") != null && ship.Attribute("class").Value.Contains("ship"));

            foreach (var ship in shipQueue)
            {
                ShipBuildQueue.Add(new Ship(ship));
            }
        }


        public void FinishShip(Ship shipyardQueueShip)
        {
            if (!ShipBuildQueue.Contains(shipyardQueueShip)) return;
            shipyardQueueShip.AddDrone("units_size_xs_transp_empty_macro", "10");
            var ship = shipyardQueueShip.Data;
            var shipConnection = new XElement("connection");
            var shipConnectionAttribute = new XAttribute("connection", "ships");
            var shipparent = shipyardQueueShip.Data.Parent;
            shipConnection.Add(shipConnectionAttribute);
            ship.Attribute("connection").Value = "space";
            var newOffset = new XElement(Data.Element("offset"));

            double d;
            const NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            var culture = CultureInfo.CreateSpecificCulture("en-GB");
            double.TryParse(newOffset.Element("position").Attribute("z").ToString(), style, culture, out d);
            d += 100;
            newOffset.Element("position").Attribute("z").Value = d.ToString();

            ship.Add(newOffset);

            if ((ship.Element("controltexture") != null))
            {
                ship.Element("controltexture").Remove();
            }
            if ((ship.Element("hull") != null))
            {
                ship.Element("hull").Remove();
            }
            if ((ship.Element("storage") != null))
            {
                ship.Element("storage").Remove();
            }
            if ((ship.Attribute("state") != null))
            {
                ship.Attribute("state").Remove();
            }

            var constrElements =
                ship.Descendants()
                    .Where(
                        ele =>
                            ele.Attribute("state") != null &&
                            ele.Attribute("state").Value.ToString().Contains("constructing"));

            foreach (var element in constrElements)
            {
                element.Attribute("state").Value = "collapsed";
            }

            var constrElements2 =
                ship.Descendants()
                    .Where(
                        ele => ele.Attribute("state") != null && ele.Attribute("state").Value.Contains("construction"));

            foreach (var element in constrElements2)
            {
                element.Attribute("state").Remove();
            }

            var gravidar = ship.Elements("gravidar");
            if (gravidar == null)
            {
                var newGravidar = new XElement("gravidar");
                ship.Add(newGravidar);
            }

            Data.Parent.Parent.Add(shipConnection);
            shipConnection.Add(ship);
            shipparent.Parent.RemoveNodes();

            ShipBuildQueue.Remove(shipyardQueueShip);
            MessageBox.Show("Ship successfully transferred to space");
        }
    }
}
