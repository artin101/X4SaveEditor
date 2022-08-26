using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;


namespace X4SaveEditor
{
    public class Savegame : INotifyPropertyChanged
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


        private PlayerPilot _player;
        public PlayerPilot Player
        {
            get { return _player; }
            set
            {
                _player = value;
                OnPropertyChanged("PlayerPilot");
            }
        }

        private XElement _data;
        public XElement Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        private List<Ship> _playerShips = new List<Ship>();
        public List<Ship> PlayerShips
        {
            get { return _playerShips; }
            set
            {
                _playerShips = value;
                OnPropertyChanged("PlayerShips");
            }
        }

        private List<Shipyard> _shipyards = new List<Shipyard>();
        public List<Shipyard> Shipyards
        {
            get { return _shipyards; }
            set
            {
                _shipyards = value;
                OnPropertyChanged("Shipyards");
            }
        }

        private List<Station> _stations = new List<Station>();
        public List<Station> Stations
        {
            get { return _stations; }
            set
            {
                _stations = value;
                OnPropertyChanged("Stations");
            }
        }

        public string Path { get; set; }


        public Savegame(string fileLoc)
        {
            Path = fileLoc;
            Data = XElement.Load(Path, LoadOptions.PreserveWhitespace);

            var playerPilotDate = Data.Descendants().Elements("connection")
                    .FirstOrDefault(x => x.Attribute("connection") != null && x.Attribute("connection").Value == "player");
            Player = new PlayerPilot(playerPilotDate);

            var ships = Data
                .Descendants()
                .Elements("component")
                .Where(
                    ship =>
                        (ship.Attribute("owner") != null && ship.Attribute("owner").Value == "player")
                        && (ship.Attribute("state") == null || ship.Attribute("state").Value != "wreck")
                        && (ship.Attribute("class") != null && ship.Attribute("class").Value.Contains("ship_"))
                        ).ToList();

            foreach (var ship in ships)
            {
                PlayerShips.Add(new Ship(ship));
            }


            var shipyards =
                Data
                .Elements("component")
                .Where(
                    shipyard => shipyard.Attribute("macro") != null && shipyard.Attribute("macro").Value.Contains("shipyard"));

            foreach (var shipyard in shipyards)
            {
                Shipyards.Add(new Shipyard(shipyard));
            }


            var playerStations =
                Data.Descendants()
                    .Elements("component")
                    .Where(
                        ele =>
                            ele.Attribute("owner") != null && ele.Attribute("class") != null &&
                            ele.Attribute("owner").Value == "player" && ele.Attribute("class").Value.Contains("station"));

            foreach (var station in playerStations)
            {
                Stations.Add(new Station(station));
            }
        }

        public Savegame(string fileLoc, bool useReader)
        {
            Stream stream = new MemoryStream();
            Path = fileLoc;
            var settings = new XmlReaderSettings { Async = false, IgnoreWhitespace = true };
            XmlDocument doc = new XmlDocument();


            using (var xmlReader = XmlReader.Create(Path, settings))
            {
                var keepReading = true;
                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            //Console.WriteLine("Start Element {0}", );
                            using (var stanzaReader = xmlReader.ReadSubtree())
                            {
                                Data = XElement.Load(stanzaReader);
                            }

                            keepReading = false;
                            //var name = xmlReader.Name;
                            break;
                        case XmlNodeType.Text:
                            //Console.WriteLine("Text Node: {0}",
                            var text = xmlReader.GetValueAsync().Result;
                            break;
                        case XmlNodeType.EndElement:
                            //Console.WriteLine("End Element {0}", reader.Name);
                            var name1 = xmlReader.Name;
                            break;
                        default:
                            //Console.WriteLine("Other node {0} with value {1}",
                            var node = xmlReader.NodeType;
                            var value = xmlReader.Value;
                            break;
                    }

                    if (!keepReading)
                    {
                        break;
                    }
                }

                //while (xmlReader.Read())
                //{
                //    switch (xmlReader.NodeType)
                //    {
                //        case XmlNodeType.Element:
                //            //Console.WriteLine("Start Element {0}", );
                //            var name = xmlReader.Name;
                //            break;
                //        case XmlNodeType.Text:
                //            //Console.WriteLine("Text Node: {0}",
                //            var text = xmlReader.GetValueAsync().Result;
                //            break;
                //        case XmlNodeType.EndElement:
                //            //Console.WriteLine("End Element {0}", reader.Name);
                //            var name1 = xmlReader.Name;
                //            break;
                //        default:
                //            //Console.WriteLine("Other node {0} with value {1}",
                //            var node = xmlReader.NodeType;
                //            var value = xmlReader.Value;
                //            break;
                //    }
                //}
            }

            var playerPilotDate = Data.Descendants().Elements("connection")
                .FirstOrDefault(x => x.Attribute("connection") != null && x.Attribute("connection").Value == "player");
            Player = new PlayerPilot(playerPilotDate);

            var ships = Data
                .Descendants()
                .Elements("component")
                .Where(
                    ship =>
                        (ship.Attribute("owner") != null && ship.Attribute("owner").Value == "player")
                        && (ship.Attribute("state") == null || ship.Attribute("state").Value != "wreck")
                        && (ship.Attribute("class") != null && ship.Attribute("class").Value.Contains("ship_"))
                ).ToList();

            foreach (var ship in ships)
            {
                PlayerShips.Add(new Ship(ship));
            }


            var shipyards =
                Data
                    .Elements("component")
                    .Where(
                        shipyard => shipyard.Attribute("macro") != null && shipyard.Attribute("macro").Value.Contains("shipyard"));

            foreach (var shipyard in shipyards)
            {
                Shipyards.Add(new Shipyard(shipyard));
            }


            var playerStations =
                Data.Descendants()
                    .Elements("component")
                    .Where(
                        ele =>
                            ele.Attribute("owner") != null && ele.Attribute("class") != null &&
                            ele.Attribute("owner").Value == "player" && ele.Attribute("class").Value.Contains("station"));

            foreach (var station in playerStations)
            {
                Stations.Add(new Station(station));
            }
        }




        public void RemoveMod(XElement modElement)
        {
            if ((modElement != null))
            {
                modElement.Remove();
            }
        }

        public void RepairShip(Ship ship)
        {
            ship.RepairShip();
        }

        public void RemoveCargo(Ship ship, XElement cargo)
        {
            ship.RemoveCargo(cargo);
        }

        public void AddCargo(Ship ship, string cargoType)
        {
            ship.AddCargo(cargoType);
        }
        public void RemoveTrade(Ship ship, XElement trade)
        {
            ship.RemoveTrade(trade);
        }


        public void FinishShip(Shipyard shipyard, Ship ship)
        {
            shipyard.FinishShip(ship);

            //refresh ship list
            PlayerShips = new List<Ship>();
            var ships =
                Data
                    .Elements("component")
                    .Where(
                        element =>
                            element.Attribute("owner").Value == "player" && (element.Attribute("state").Value != "wreck") && element.Attribute("class").Value == "ship_");
            foreach (var element in ships)
            {
                PlayerShips.Add(new Ship(element));
            }
        }

        public void DeleteShip(Ship ship)
        {
            if (ship.Data.Attribute("macro").Value != "unit_player_ship_macro")
            {
                if (!PlayerShips.Contains(ship)) return;
                if (ship.Data.Parent != null) ship.Data.Parent.Remove();
                PlayerShips.Remove(ship);
            }
            else
            {
                MessageBox.Show("You don't want to delete the skunk, will you? :)");
            }

        }


        public void Save()
        {
            Data.Save(Path, SaveOptions.DisableFormatting);
        }

        public void DeleteAllMods()
        {
            var info = Data.Element("info");
            if (info == null) return;
            var gameMods = info.Element("patches");
            if ((gameMods != null))
            {
                gameMods.Remove();
            }
        }


        public void AdjustMoney(string money)
        {
            var account =
                Data.Descendants()
                    .Elements("factions")
                    .Elements("faction")
                    .Where(ele => ele.Attribute("id") != null && ele.Attribute("id").Value == "player")
                    .Elements("account")
                    .First();

            var accountId = account.Attribute("id").Value;
            account.Attribute("amount").Value = money;

            foreach (var node in Data.Descendants().Elements("account").Where(ele => ele.Attribute("id") != null && ele.Attribute("id").Value == accountId))
            {
                node.Attribute("amount").Value = money;
            }

            foreach (var node in Data.Elements("stats").Elements("stat").Where(ele => ele.Attribute("id") != null && ele.Attribute("id").Value == "money_player"))
            {
                node.Attribute("value").Value = money;
            }
        }

        public void AddCrewAttributeNode(string attribute, Ship ship, CrewMember crewMember)
        {
            if (PlayerShips.Contains(ship))
            {
                ship.AddCrewAttributeNode(attribute, crewMember);
            }
        }
        public void AddCrewAttributeStationNode(string attribute, Station station, CrewMember crewMember)
        {
            if (Stations.Contains(station))
            {
                station.AddCrewAttributeNode(attribute, crewMember);
            }
        }

        public void AddDroneShip(Ship ship)
        {
            ship.AddDrone();
        }
        public void RemoveDroneShip(Ship ship, XElement drone)
        {
            ship.RemoveDrone(drone);
        }
        public void AddGravidar(Ship ship)
        {
            ship.AddGravidar();
        }
    }
}
