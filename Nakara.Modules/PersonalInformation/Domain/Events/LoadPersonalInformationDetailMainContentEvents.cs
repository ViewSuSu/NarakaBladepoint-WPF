using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nakara.Modules.PersonalInformation.Domain.Events
{
    internal class LoadPersonalInformationDetailMainContentEvents : PubSubEvent<string> { }
    internal class RemovePersonalInformationDetailMainContentEvents : PubSubEvent{ }

}
