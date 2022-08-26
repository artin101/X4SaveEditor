using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Xml.Linq;

namespace X4SaveEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Savegame saveXML;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void MenuItemLoad_Click(object sender, RoutedEventArgs e)
        {
            //Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = "*.xml";
            dlg.Filter = "eXtensible Markup Language file (.xml)|*.xml";

            bool result = Convert.ToBoolean(dlg.ShowDialog());
            if ((result))
            {
                //saveXML = new Savegame(dlg.FileName);
                saveXML = new Savegame(dlg.FileName, true);
                ParentGrid.DataContext = saveXML;
                SaveMenuItem.IsEnabled = true;
                ModMenuItem.IsEnabled = true;
                MainTabControl.IsEnabled = true;

            }

        }

        private void ButtonRemoveMod_Click(object sender, RoutedEventArgs e)
        {
            saveXML.RemoveMod((XElement)ModListBox.SelectedItem);
        }

        private void ButtonRepairShip_Click(object sender, RoutedEventArgs e)
        {
            saveXML.RepairShip((Ship)PlayerShipListBox.SelectedItem);
        }

        private void ButtonRemoveCargo_Click(object sender, RoutedEventArgs e)
        {
            saveXML.RemoveCargo((Ship)PlayerShipListBox.SelectedItem, (XElement)BulkCargoListBox.SelectedItem);
        }

        private void ButtonAddFuelCargo_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCargo((Ship)PlayerShipListBox.SelectedItem, "FuelCargo");
        }
        private void ButtonAddUniversalCargo_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCargo((Ship)PlayerShipListBox.SelectedItem, "UniversalCargo");
        }
        private void ButtonAddBulkCargo_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCargo((Ship)PlayerShipListBox.SelectedItem, "BulkCargo");
        }
        private void ButtonAddContainerCargo_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCargo((Ship)PlayerShipListBox.SelectedItem, "ContainerCargo");
        }
        private void ButtonAddEnergyCargo_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCargo((Ship)PlayerShipListBox.SelectedItem, "EnergyCargo");
        }
        private void ButtonAddLiquidCargo_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCargo((Ship)PlayerShipListBox.SelectedItem, "LiquidCargo");
        }
        private void ButtonRemoveFuelCargo_Click(object sender, RoutedEventArgs e)
        {
            saveXML.RemoveCargo((Ship)PlayerShipListBox.SelectedItem, (XElement)FuelCargoListBox.SelectedItem);
        }
        private void ButtonRemoveUniversalCargo_Click(object sender, RoutedEventArgs e)
        {
            saveXML.RemoveCargo((Ship)PlayerShipListBox.SelectedItem, (XElement)UniversalCargoListBox.SelectedItem);
        }
        private void ButtonRemoveBulkCargo_Click(object sender, RoutedEventArgs e)
        {
            saveXML.RemoveCargo((Ship)PlayerShipListBox.SelectedItem, (XElement)BulkCargoListBox.SelectedItem);
        }
        private void ButtonRemoveContainerCargo_Click(object sender, RoutedEventArgs e)
        {
            saveXML.RemoveCargo((Ship)PlayerShipListBox.SelectedItem, (XElement)ContainerCargoListBox.SelectedItem);
        }
        private void ButtonRemoveEnergyCargo_Click(object sender, RoutedEventArgs e)
        {
            saveXML.RemoveCargo((Ship)PlayerShipListBox.SelectedItem, (XElement)EnergyCargoListBox.SelectedItem);
        }
        private void ButtonRemoveLiquidCargo_Click(object sender, RoutedEventArgs e)
        {
            saveXML.RemoveCargo((Ship)PlayerShipListBox.SelectedItem, (XElement)ShipTradeListBox.SelectedItem);
        }

        private void ButtonRemoveTrade_Click(object sender, RoutedEventArgs e)
        {
            saveXML.RemoveTrade((Ship)PlayerShipListBox.SelectedItem, (XElement)ShipTradeListBox.SelectedItem);
        }

        private void ButtonFinishShip_Click(object sender, RoutedEventArgs e)
        {
            saveXML.FinishShip((Shipyard)ShipyardListBox.SelectedItem, (Ship)shipyardShipsListBox.SelectedItem);
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            PlayerNameTextBox.Focusable = true;
            PlayerNameTextBox.Focus();

            saveXML.Save();
            MessageBox.Show("File saved successfully");
        }

        private void ButtonDeleteShip_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Really Delete Ship?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes))
            {
                saveXML.DeleteShip((Ship)PlayerShipListBox.SelectedItem);
            }
        }

        private void MenuItemMod_Click(object sender, RoutedEventArgs e)
        {
            saveXML.DeleteAllMods();
        }

        private void PlayerMoneyTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            saveXML.AdjustMoney(PlayerMoneyTextBox.Text);
        }

        private void ButtonAddBoarding_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeNode("boarding",(Ship)PlayerShipListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }

        private void ButtonAddCombat_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeNode("combat",(Ship)PlayerShipListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }

        private void ButtonAddEngineering_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeNode("engineering",(Ship)PlayerShipListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }

        private void ButtonAddNavigation_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeNode("navigation",(Ship)PlayerShipListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }

        private void ButtonAddLeadership_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeNode("leadership",(Ship)PlayerShipListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }

        private void ButtonAddMorale_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeNode("morale",(Ship)PlayerShipListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }

        private void ButtonAddScience_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeNode("science",(Ship)PlayerShipListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }

        private void ButtonAddManagement_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeNode("management",(Ship)PlayerShipListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }
        private void ButtonAddBoardingStation_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeStationNode("boarding", (Station)StationListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }

        private void ButtonAddCombatStation_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeStationNode("combat", (Station)StationListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }

        private void ButtonAddEngineeringStation_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeStationNode("engineering", (Station)StationListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }

        private void ButtonAddNavigationStation_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeStationNode("navigation", (Station)StationListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }

        private void ButtonAddLeadershipStation_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeStationNode("leadership", (Station)StationListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }

        private void ButtonAddMoraleStation_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeStationNode("morale", (Station)StationListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }

        private void ButtonAddScienceStation_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeStationNode("science", (Station)StationListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }

        private void ButtonAddManagementStation_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddCrewAttributeStationNode("management", (Station)StationListBox.SelectedItem, (CrewMember)ShipCrewListBox.SelectedItem);
        }

        private void ButtonAddDroneShip_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddDroneShip((Ship)PlayerShipListBox.SelectedItem);
        }
        private void ButtonRemoveDroneShip_Click(object sender, RoutedEventArgs e)
        {
            saveXML.RemoveDroneShip((Ship)PlayerShipListBox.SelectedItem, (XElement)ShipDronesListBox.SelectedItem);
        }

        private void ButtonAddGravidarShip_Click(object sender, RoutedEventArgs e)
        {
            saveXML.AddGravidar((Ship)PlayerShipListBox.SelectedItem);
        }


        private bool dotNetCheck()
        {
            bool bInstalled = false;
            foreach (object Version_loopVariable in GetVersionFromRegistry())
            {
                var Version =(string)Version_loopVariable;
                if (Version.Contains("Client  4.5") | Version.Contains("Full  4.5"))
                {
                    bInstalled = true;
                }
            }
            return bInstalled;
        }

        private List<string> GetVersionFromRegistry()
        {
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();

            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\"))
            {

                foreach (string versionKeyName in ndpKey.GetSubKeyNames())
                {
                    if (versionKeyName.StartsWith("v"))
                    {
                        RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
                        string name = (string)versionKey.GetValue("Version", "");
                        string sp = versionKey.GetValue("SP", "").ToString();
                        string install = versionKey.GetValue("Install", "").ToString();
                        if (string.IsNullOrEmpty(install))
                        {
                            //no install info, ust be later
                            //Console.WriteLine(versionKeyName & "  " & name)
                            list.Add(versionKeyName + "  " + name);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(sp) && install == "1")
                            {
                                list.Add(versionKeyName + "  " + name + "  SP" + sp);
                                //Console.WriteLine(versionKeyName & "  " & name & "  SP" & sp)

                            }
                        }
                        if (!string.IsNullOrEmpty(name))
                        {
                            continue;
                        }
                        foreach (string subKeyName in versionKey.GetSubKeyNames())
                        {
                            RegistryKey subKey = versionKey.OpenSubKey(subKeyName);
                            name = (string)subKey.GetValue("Version", "");
                            if (!string.IsNullOrEmpty(name))
                            {
                                sp = subKey.GetValue("SP", "").ToString();
                            }
                            install = subKey.GetValue("Install", "").ToString();
                            if (string.IsNullOrEmpty(install))
                            {
                                //no install info, ust be later
                                list.Add(versionKeyName + "  " + name);
                                // Console.WriteLine(versionKeyName & "  " & name)
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(sp) && install == "1")
                                {
                                    list.Add("  " + subKeyName + "  " + name + "  SP" + sp);
                                    //Console.WriteLine("  " & subKeyName & "  " & name & "  SP" & sp)
                                }
                                else if (install == "1")
                                {
                                    list.Add("  " + subKeyName + "  " + name);
                                    //Console.WriteLine("  " & subKeyName & "  " & name)

                                }
                            }
                        }
                    }
                }
            }
            return list;
        }
    }
}
