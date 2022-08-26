using System.ComponentModel;
using System.Xml.Linq;

namespace X4SaveEditor
{
    public class Component : INotifyPropertyChanged
    {

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        private string _image;
        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        public Component(XElement component)
        {
            Data = component;
            if (component.Attribute("macro") != null)
                Image = getImage(component.Attribute("macro").Value);
        }

        private string getImage(string type)
        {
            string imagePath;

            switch (type)
            {
                //Size XS
                case "units_size_xs_wardrone_macro":
                    imagePath = "Images\\XS\\AssaultURV.jpg";
                    break;
                case "units_size_xs_boarding_ship_macro":
                    imagePath = "Images\\XS\\BoardingPod.jpg";
                    break;
                case "units_size_xs_albion_transport_1_macro":
                case "units_size_xs_albion_transport_2_macro":
                case "units_size_xs_albion_personal_transporter_macro":
                case "units_size_xs_devris_personal_transporter_macro":
                case "units_size_xs_devris_transport_01_macro":
                case "units_size_xs_devris_transport_02_macro":
                    imagePath = "Images\\XS\\BulkCarrier.jpg";
                    break;
                case "units_size_xs_transp_empty_macro":
                    imagePath = "Images\\XS\\CargolifterURV.jpg";
                    break;
                case "units_size_xs_welder_drone_macro":
                    imagePath = "Images\\XS\\ConstructionURV.jpg";
                    break;
                case "units_size_xs_albion_pv_1_blue_macro":
                case "units_size_xs_albion_pv_1_grey_macro":
                case "units_size_xs_albion_pv_1_red_macro":
                case "units_size_xs_albion_pv_1_yellow_macro":
                case "units_size_xs_albion_pv_2_blue_macro":
                case "units_size_xs_devris_pv_01_brown_macro":
                    imagePath = "Images\\XS\\ConsumerCompactCraft.jpg";
                    break;
                case "units_size_xs_albion_police_transport_macro":
                case "units_size_xs_devris_police_transport_macro":
                    imagePath = "Images\\XS\\CrewEquipmentCarrier.jpg";
                    break;
                case "unit_size_xs_escapepod_macro":
                    imagePath = "Images\\XS\\EscapePod.jpg";
                    break;
                case "units_size_xs_albion_police_car_macro":
                case "units_size_xs_devris_police_car_macro":
                    imagePath = "Images\\XS\\IndustrialSurveillanceCraft.jpg";
                    break;
                case "units_size_xs_albion_construction_macro":
                case "units_size_xs_albion_mining_macro":
                case "units_size_xs_devris_construction_macro":
                    imagePath = "Images\\XS\\MaintenanceEngineeringCraft.jpg";
                    break;
                case "units_size_xs_xenon_drone_1_macro":
                case "units_size_xs_xenon_drone_2_macro":
                case "units_size_xs_xenon_drone_3_macro":
                case "units_size_xs_xenon_drone_4_macro":
                case "units_size_xs_xenon_drone_5_macro":
                    imagePath = "Images\\XS\\TFXUtilityCraft.jpg";
                    break;
                case "units_size_xs_albion_police_apc_macro":
                case "units_size_xs_devris_police_apc_macro":
                    imagePath = "Images\\XS\\TrafficRiotControlCraft.jpg";

                    break;
                //Size S
                case "units_size_s_ship_04_macro":
                    imagePath = "Images\\S\\Artio.jpg";
                    break;
                case "units_size_s_ship_23_macro":
                    imagePath = "Images\\S\\Birog.jpg";
                    break;
                case "units_size_s_split_m3_macro":
                    imagePath = "Images\\S\\Bonescout.jpg";
                    break;
                case "units_size_s_ship_ar_military_01_macro":
                    imagePath = "Images\\S\\CamulosRaider.jpg";
                    break;
                case "units_size_s_ship_ar_military_03_macro":
                    imagePath = "Images\\S\\CamulosSentinel.jpg";
                    break;
                case "units_size_s_ship_ar_military_02_macro":
                    imagePath = "Images\\S\\CamulosVanguard.jpg";
                    break;
                case "units_size_s_ship_pirate_02_macro":
                    imagePath = "Images\\S\\Cennelath.jpg";
                    break;
                case "units_size_s_ship_pirate_01_macro":
                    imagePath = "Images\\S\\Domelch.jpg";
                    break;
                case "units_size_s_torpedo_bomber_macro":
                    imagePath = "Images\\S\\Drostan.jpg";
                    break;
                case "units_size_s_ship_ar_military_07_macro":
                    imagePath = "Images\\S\\EterscelRaider.jpg";
                    break;
                case "units_size_s_ship_ar_military_09_macro":
                    imagePath = "Images\\S\\EterscelSentinel.jpg";
                    break;
                case "units_size_s_ship_ar_military_08_macro":
                    imagePath = "Images\\S\\EterscelVanguard.jpg";
                    break;
                case "units_size_s_ship_ar_military_04_macro":
                    imagePath = "Images\\S\\FoltorRaider.jpg";
                    break;
                case "units_size_s_ship_ar_military_06_macro":
                    imagePath = "Images\\S\\FoltorSentinel.jpg";
                    break;
                case "units_size_s_ship_ar_military_05_macro":
                    imagePath = "Images\\S\\FoltorVanguard.jpg";
                    break;
                case "units_size_s_canteran_fighter_01_macro":
                    imagePath = "Images\\S\\Hayabusa.jpg";
                    break;
                case "units_size_s_ship_19_macro":
                    imagePath = "Images\\S\\Hesus.jpg";
                    break;
                case "units_size_s_xenon_swarm_attack_drone_01_macro":
                    imagePath = "Images\\S\\M.jpg";
                    break;
                case "units_size_s_ship_pirate_03_macro":
                    imagePath = "Images\\S\\Maelchon.jpg";
                    break;
                case "units_size_s_pmc_xen_01_macro":
                    imagePath = "Images\\S\\Moebius.jpg";
                    break;
                case "units_size_s_xenon_swarm_attack_drone_02_macro":
                    imagePath = "Images\\S\\N.jpg";
                    break;
                case "units_size_s_ship_07_macro":
                    imagePath = "Images\\S\\Nechtan.jpg";
                    break;
                case "units_size_s_ship_01_macro":
                    imagePath = "Images\\S\\Orlam.jpg";
                    break;
                case "units_size_s_ship_03_macro":
                    imagePath = "Images\\S\\Ossian.jpg";
                    break;
                case "units_size_s_split_m4_macro":
                    imagePath = "Images\\S\\SkullCrusher.jpg";
                    break;
                case "units_size_s_ship_02_macro":
                    imagePath = "Images\\S\\Talorcan.jpg";
                    break;
                case "units_size_s_ship_pmc_01_macro":
                    imagePath = "Images\\S\\TriathRaider.jpg";
                    break;
                case "units_size_s_ship_pmc_03_macro":
                    imagePath = "Images\\S\\TriathSentinel.jpg";
                    break;
                case "units_size_s_ship_pmc_02_macro":
                    imagePath = "Images\\S\\TriathVanguard.jpg";
                    break;
                case "units_size_s_ship_05_macro":
                    imagePath = "Images\\S\\Vasio.jpg";

                    break;
                //Size L
                case "units_size_l_single_attack_ship_macro":
                    imagePath = "Images\\L\\Balor.jpg";
                    break;
                case "units_size_l_hydrogen_collector_macro":
                    imagePath = "Images\\L\\Boann.jpg";
                    break;
                case "units_size_l_crystal_collector_macro":
                case "units_size_l_nividium_collector_macro":
                    imagePath = "Images\\L\\Fedhelm.jpg";
                    break;
                case "units_size_l_kit_carrier_02_macro":
                    imagePath = "Images\\L\\HeavySul.jpg";
                    break;
                case "units_size_l_liquid_freighter_macro":
                    imagePath = "Images\\L\\Hermod.jpg";
                    break;
                case "units_size_l_xenon_01_macro":
                    imagePath = "Images\\L\\K.jpg";
                    break;
                case "units_size_l_canteran_transporter_macro":
                    imagePath = "Images\\L\\Lepton.jpg";
                    break;
                case "units_size_l_kit_carrier_01_macro":
                    imagePath = "Images\\L\\LightSul.jpg";
                    break;
                case "units_size_l_ions_collector_macro":
                case "units_size_l_plasma_collector_macro":
                    imagePath = "Images\\L\\Midir.jpg";
                    break;
                case "units_size_l_kit_bulk_01_macro":
                    imagePath = "Images\\L\\RahanasBulk.jpg";
                    break;
                case "units_size_l_kit_container_01_macro":
                case "units_size_l_kit_container_02_macro":
                case "units_size_l_kit_hybrid_01_macro":
                case "units_size_l_kit_hybrid_02_macro":
                    imagePath = "Images\\L\\RahanasContainer.jpg";
                    break;
                case "units_size_l_kit_energy_01_macro":
                    imagePath = "Images\\L\\RahanasEnergy.jpg";
                    break;
                case "units_size_l_kit_liquid_01_macro":
                    imagePath = "Images\\L\\RahanasLiquid.jpg";
                    break;
                case "units_size_l_ice_collector_macro":
                    imagePath = "Images\\L\\Sequana.jpg";

                    break;
                //Size XL
                case "units_size_xl_builder_ship_dv_macro":
                case "units_size_xl_builder_ship_macro":
                case "units_size_xl_builder_ship_ol_macro":
                case "units_size_xl_builder_ship_plot_01_macro":
                    imagePath = "Images\\XL\\ConstructionVessel.jpg";
                    break;
                case "units_size_xl_transporter_1_macro":
                case "units_size_xl_transporter_2_macro":
                case "units_size_xl_transporter_3_macro":
                    imagePath = "Images\\XL\\SuperFreighter.jpg";
                    break;
                case "units_size_xl_capital_destroyer_1_macro":
                    imagePath = "Images\\XL\\Arawn.jpg";
                    break;
                case "units_size_xl_capital_destroyer_2_macro":
                    imagePath = "Images\\XL\\Taranis.jpg";
                    break;
                case "units_size_xl_split_m1_macro":
                    imagePath = "Images\\XL\\GangreneChaser.jpg";
                    break;
                case "units_size_xl_cargo_hauler_3_macro":
                    imagePath = "Images\\XL\\Scaldis.jpg";
                    break;
                case "units_size_xl_red_destroyer_macro":
                    imagePath = "Images\\XL\\Sucellus.jpg";
                    break;
                case "units_size_xl_cargo_hauler_2_macro":
                    imagePath = "Images\\XL\\Titurel.jpg";
                    break;
                case "unit_player_ship_macro":
                    imagePath = "Images\\PL\\Skunk.jpg";
                    break;
                default:
                    imagePath = "";
                    break;
            }
            return imagePath;
        }
    }
}
