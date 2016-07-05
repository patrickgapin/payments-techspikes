using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using codeeffects_sample01.Models;
using codeeffects_sample01.Models.CreditCard;
using CodeEffects.Rule;
using CodeEffects.Rule.Attributes;
using CodeEffects.Rule.Common;

namespace codeeffects_sample01.Models.Helpers
{
    public class Helper
    {       
        [Method(DisplayName = "Count")]
        public int Count<T> (List<T> list )
        {
            return list.Count;
        }

        public static List<DataSourceItem> GetRoles()
        {
            return new List<DataSourceItem>
            {
                new DataSourceItem {ID = 1, Name = "Management"},
                new DataSourceItem {ID = 2, Name = "Network Admins"},
                new DataSourceItem {ID = 3, Name = "Developers"},
                new DataSourceItem {ID = 4, Name = "Designers"}
            };
        }
    }
}