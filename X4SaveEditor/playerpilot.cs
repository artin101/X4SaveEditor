using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace X4SaveEditor
{

    public class PlayerPilot : Component
    {

        private List<XElement> _blueprints = new List<XElement>();
        public List<XElement> Blueprints
        {
            get { return _blueprints; }
            set
            {
                _blueprints = value;
                OnPropertyChanged("Blueprints");
            }
        }

        private List<XElement> _inventory = new List<XElement>();
        public List<XElement> Inventory
        {
            get { return _inventory; }
            set
            {
                _inventory = value;
                OnPropertyChanged("Inventory");
            }
        }

        public PlayerPilot(XElement component) : base(component)
        {

            var inventory = from wares in Data.Descendants().Elements("inventory").Elements("ware")
                            select wares;

            foreach (var ware in inventory)
            {
                Inventory.Add(ware);
            }

            var blueprints = from blueprint in Data.Descendants().Elements("blueprints").Elements("blueprint")
                             select blueprint;

            foreach (var blueprint in blueprints)
            {
                Blueprints.Add(blueprint);
            }
        }
    }
}
