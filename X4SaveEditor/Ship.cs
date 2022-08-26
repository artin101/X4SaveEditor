using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace X4SaveEditor
{
    public class Ship : Component
    {
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

        private ObservableCollection<XElement> _tradeQueue = new ObservableCollection<XElement>();
        public ObservableCollection<XElement> TradeQueue
        {
            get { return _tradeQueue; }
            set
            {
                _tradeQueue = value;
                OnPropertyChanged("TradeQueue");
            }
        }

        private ObservableCollection<XElement> _constructionWares = new ObservableCollection<XElement>();
        public ObservableCollection<XElement> ConstructionWares
        {
            get { return _constructionWares; }
            set
            {
                _constructionWares = value;
                OnPropertyChanged("ConstructionWares");
            }
        }

        private ObservableCollection<XElement> _drones = new ObservableCollection<XElement>();
        public ObservableCollection<XElement> Drones
        {
            get { return _drones; }
            set
            {
                _drones = value;
                OnPropertyChanged("Drones");
            }
        }

        private XElement _gravidar;
        public XElement Gravidar
        {
            get { return _gravidar; }
            set
            {
                _gravidar = value;
                OnPropertyChanged("Gravidar");
            }
        }

        private XElement _fuelCargo;
        public XElement FuelCargo
        {
            get { return _fuelCargo; }
            set
            {
                _fuelCargo = value;
                OnPropertyChanged("FuelCargo");
            }
        }
        private XElement _bulkCargo;
        public XElement BulkCargo
        {
            get { return _bulkCargo; }
            set
            {
                _bulkCargo = value;
                OnPropertyChanged("BulkCargo");
            }
        }
        private XElement _containerCargo;
        public XElement ContainerCargo
        {
            get { return _containerCargo; }
            set
            {
                _containerCargo = value;
                OnPropertyChanged("ContainerCargo");
            }
        }
        private XElement _energyCargo;
        public XElement EnergyCargo
        {
            get { return _energyCargo; }
            set
            {
                _energyCargo = value;
                OnPropertyChanged("EnergyCargo");
            }
        }
        private XElement _liquidCargo;
        public XElement LiquidCargo
        {
            get { return _liquidCargo; }
            set
            {
                _liquidCargo = value;
                OnPropertyChanged("LiquidCargo");
            }
        }
        private XElement _universalCargo;
        public XElement UniversalCargo
        {
            get { return _universalCargo; }
            set
            {
                _universalCargo = value;
                OnPropertyChanged("UniversalCargo");
            }
        }



        public Ship(XElement ship) : base(ship)
        {
            if (ship.Attribute("name") == null)
            {
                var conv = new EntityValueConverter();

                var name = new XAttribute("name", conv.Convert(ship.Attribute("macro").Value, null, null, null));
                ship.Add(name);
            }

            var crewquery = from crew in Data.Descendants().Elements("person")
                            select crew;

            foreach (var person in crewquery)
            {
                Crew.Add(new CrewMember(person));
            }

            foreach (var trade in Data.Elements("trade").Elements("shopping").Elements("trade"))
            {
                TradeQueue.Add(trade);
            }

            var buildmodulesQuery = from buildmodules in Data.Descendants().Elements("connection")
                                    where buildmodules.Attribute("macro") != null && buildmodules.Attribute("macro").Value == "connection_buildmodule01"
                                    select buildmodules;

            var constructionWaresQuery = from wares in buildmodulesQuery.Descendants().Elements("resources").Elements("ware") select wares;

            foreach (var ware in constructionWaresQuery)
            {
                ConstructionWares.Add(ware);
            }

            foreach (var drone in Data.Elements("ammunition").Elements("available").Elements("item"))
            {
                Drones.Add(drone);
            }


            dynamic cargoList = from cargo in Data.Descendants().Elements("component")
                                where
                                    cargo.Attribute("class") != null && cargo.Attribute("class").Value == "storage" &&
                                    cargo.Attribute("macro") != null && cargo.Attribute("macro").Value != "unit_player_ship_storage_macro"
                                select cargo;

            foreach (var cargoType in cargoList)
            {
                string cargoTypeName = cargoType.Attribute("macro").Value;

                switch (cargoTypeName)
                {
                    case "storage_ship_l_bulk_01_macro":
                    case "storage_ship_m_bulk_01_macro":
                    case "storage_ship_l_bulk_02_macro":
                    case "storage_ship_l_bulk_03_macro":
                    case "storage_ship_l_bulk_04_macro":
                    case "storage_ship_xl_bulk_01_macro":
                        BulkCargo = cargoType;
                        break;
                    case "storage_ship_l_container_01_macro":
                    case "storage_ship_l_container_02_macro":
                    case "storage_ship_m_container_01_macro":
                    case "storage_ship_xl_container_01_macro":
                        ContainerCargo = cargoType;
                        break;
                    case "storage_ship_l_energy_01_macro":
                    case "storage_ship_l_energy_02_macro":
                    case "storage_ship_xl_energy_01_macro":
                    case "storage_ship_m_energy_01_macro":
                        EnergyCargo = cargoType;
                        break;
                    case "storage_ship_l_fuel_01_macro":
                    case "storage_ship_xl_fuel_01_macro":
                        FuelCargo = cargoType;
                        break;
                    case "storage_ship_l_liquid_01_macro":
                    case "storage_ship_l_liquid_02_macro":
                    case "storage_ship_l_liquid_03_macro":
                    case "storage_ship_xl_liquid_01_macro":
                    case "storage_ship_m_liquid_01_macro":
                        LiquidCargo = cargoType;
                        break;
                    case "storage_ship_xl_universal_01_macro":
                    case "storage_ship_xs_universal_01_macro":
                    case "storage_temp_huge_macro":
                        UniversalCargo = cargoType;
                        break;
                }
            }

            Gravidar = Data.Element("gravidar");

        }


        public void RemoveCargo(XElement cargo)
        {
            if ((cargo != null))
            {
                cargo.Remove();
            }
        }

        public void RemoveTrade(XElement trade)
        {
            if (!TradeQueue.Contains(trade)) return;
            TradeQueue.Remove(trade);
            trade.Remove();
        }

        private static XElement CheckCargoTags(XElement cargoconnection)
        {

            var cargoTag = cargoconnection.Element("cargo");

            if (cargoTag == null)
            {
                cargoTag = new XElement("cargo", new XElement("summary", new XAttribute("state", "collapsed"), new XAttribute("connection", "cargo")));
                cargoconnection.AddFirst(cargoTag);
            }
            else if (cargoconnection.Element("cargo") != null && cargoconnection.Element("cargo").Element("summary") == null)
            {
                var summaryTag = new XElement("summary", new XAttribute("state", "collapsed"), new XAttribute("connection", "cargo"));
                cargoconnection.Element("cargo").AddFirst(summaryTag);
            }
            return cargoconnection;
        }

        public void AddCargo(string cargoType)
        {
            XElement newCargo;
            switch (cargoType)
            {
                case "FuelCargo":
                    newCargo = new XElement("ware", new XAttribute("ware", "fuelcells"), new XAttribute("amount", "0"));

                    CheckCargoTags(FuelCargo).Element("cargo").Element("summary").Add(newCargo);
                    break;
                case "UniversalCargo":
                    newCargo = new XElement("ware", new XAttribute("ware", "antimattercells"), new XAttribute("amount", "0"));
                    CheckCargoTags(UniversalCargo).Element("cargo").Element("summary").Add(newCargo);
                    break;
                case "BulkCargo":
                    newCargo = new XElement("ware", new XAttribute("ware", "crystals"), new XAttribute("amount", "0"));
                    CheckCargoTags(BulkCargo).Element("cargo").Element("summary").Add(newCargo);
                    break;
                case "ContainerCargo":
                    newCargo = new XElement("ware", new XAttribute("ware", "bioelectricneurongel"), new XAttribute("amount", "0"));
                    CheckCargoTags(ContainerCargo).Element("cargo").Element("summary").Add(newCargo);
                    break;
                case "EnergyCargo":
                    newCargo = new XElement("ware", new XAttribute("ware", "antimattercells"), new XAttribute("amount", "0"));
                    CheckCargoTags(EnergyCargo).Element("cargo").Element("summary").Add(newCargo);
                    break;
                case "LiquidCargo":
                    newCargo = new XElement("ware", new XAttribute("ware", "hydrogen"), new XAttribute("amount", "0"));
                    CheckCargoTags(LiquidCargo).Element("cargo").Element("summary").Add(newCargo);
                    break;
            }
        }

        public void RepairShip()
        {
            var damagedParts =
                Data.Descendants()
                    .Attributes("state")
                    .Where(parts => parts != null && (parts.Value.Contains("wrecked") || parts.Value.Contains("wreck")));
            foreach (var part in damagedParts)
            {
                part.Remove();
            }
            MessageBox.Show("Components Repaired");
        }

        public void AddCrewAttributeNode(string attribute, CrewMember crewMember)
        {
            if (Crew.Contains(crewMember))
            {
                crewMember.AddAttributeNode(attribute);
            }

        }

        public void AddDrone()
        {
            AddDrone("units_size_xs_transp_empty_macro", "0");
        }

        public void AddDrone(string macro, string amount)
        {
            XElement droneAmmo;
            var ammo = Data.Elements("ammunition").ToList();
            if (ammo.Count == 0)
            {
                droneAmmo = new XElement("ammunition", new XElement("available"));

                //check after which tag to add the drone Ammo
                var tag = (Data.Element("survace") ?? (Data.Element("turret") ?? Data.Element("shields"))) ??
                          Data.FirstNode;

                tag.AddAfterSelf(droneAmmo);
            }
            else
            {
                droneAmmo = ammo.First();
            }


            var newDrone = new XElement("item", new XAttribute("macro", macro), new XAttribute("amount", amount));
            droneAmmo.Elements("available").First().Add(newDrone);
            Drones.Add(newDrone);
        }
        public void RemoveDrone(XElement drone)
        {
            if (!Drones.Contains(drone)) return;
            Drones.Remove(drone);
            drone.Remove();
        }

        public void AddGravidar()
        {
            if (Gravidar == null)
            {
                Gravidar = new XElement("gravidar");

                Data.Add(Gravidar);
                MessageBox.Show("Gravidar successfully added to ship");
            }
            else
            {
                MessageBox.Show("Ship has a gravidar");
            }
        }
    }
}
