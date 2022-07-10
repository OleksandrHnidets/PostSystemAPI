using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Enums
{
    public enum DeliveryStatus : byte
    {
        [Description("Delivering")]
        Delivering = 0,

        [Description("Waiting To Accept")]
        WaitingToAccept,

        [Description("Received")]
        Received,

        [Description("Declined")]
        Declined
    }
}
