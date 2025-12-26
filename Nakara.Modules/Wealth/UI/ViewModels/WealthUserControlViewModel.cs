using Nakara.Modules.Wealth.UI.Models;

namespace Nakara.Modules.Wealth.UI.ViewModels
{
    public partial class WealthUserControlViewModel : BindableBase
    {
        public WealthModel WealthModel { get; } =
            new WealthModel() { GoldBrick = 12500, AncientCoins = 1111 };

        public WealthUserControlViewModel() { }
    }
}
