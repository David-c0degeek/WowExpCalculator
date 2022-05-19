using System.Windows;
using WowExpCalculator.Core;
using WowExpCalculator.Core.Enums;

namespace WowExpCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCalculate_OnClick(object sender, RoutedEventArgs e)
        {
            var playerLevel = TbcPlayerLevel.From(ushort.Parse(txtCharLevel.Text));
            var mobLevel = uint.Parse(txtMobLevel.Text);
            var continent = (Continents)cbContinent.SelectedValue;
            var highestGroupMemberLevel = TbcPlayerLevel.From(ushort.Parse(txtHighestPartyMemberLevel.Text));
            var groupSize = cbPartySize.SelectedIndex + 1;
            var isElite = cbElite.IsChecked ?? false;
            var isRested = cbRested.IsChecked ?? false;

            txtResult.Text = $"{ExpCalculator.CalculateExp(playerLevel, mobLevel, continent, highestGroupMemberLevel, (byte)groupSize, isElite, isRested)}";
        }
    }
}