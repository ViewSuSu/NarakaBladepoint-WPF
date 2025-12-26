using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nakara.Modules.StartGame.Domain.EventCenter.Events
{
    internal class LoadEventCenterEvent : PubSubEvent<string> { }
    internal class RemoveEventCenterEvent : PubSubEvent{ }
}
