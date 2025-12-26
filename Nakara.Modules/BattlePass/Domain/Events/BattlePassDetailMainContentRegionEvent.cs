using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nakara.Modules.BattlePass.Domain.Events
{
    internal class LoadBattlePassDetailMainContentRegionEvent : PubSubEvent<string> { }
    internal class RemoveBattlePassDetailMainContentRegionEvent : PubSubEvent { }

}
