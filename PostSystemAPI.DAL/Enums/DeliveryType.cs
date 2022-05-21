using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Enums
{
    public enum DeliveryType: byte
    {
        [Description("Parcel")]
        Parcel = 0,

        [Description("Document")]
        Document,

        [Description("Letter")]
        Letter
    }
}
