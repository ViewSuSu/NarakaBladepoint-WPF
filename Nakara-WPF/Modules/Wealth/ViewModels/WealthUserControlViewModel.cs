using Nakara_WPF.Modules.Wealth.Models;

namespace Nakara_WPF.Modules.Wealth.ViewModels
{
    public partial class WealthUserControlViewModel : BindableBase
    {
        public WealthModel WealthModel { get; } =
            new WealthModel() { GoldBrick = 12500, AncientCoins = 1111 };

        public WealthUserControlViewModel() { }
    }
}
