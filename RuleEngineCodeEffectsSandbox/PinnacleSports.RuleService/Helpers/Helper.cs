
using System.Collections.Generic;
using CodeEffects.Rule.Attributes;
using CodeEffects.Rule.Common;
using PinnacleSports.RuleService.Helpers;
using RuleEngineCodeEffectsSandbox.Ioc;

namespace PinnacleSports.RuleService.Models.Helpers
{
    public class Helper
    {
        [Method(DisplayName = "Count")]
        public int Count<T>(List<T> list)
        {
            return list.Count;
        }

        public Helper()
        {
            RuleInjectorInitializer.Initialize();
        }

        public List<DataSourceItem> GetRoles()
        {
            var clientHelper = RuleInjectorInitializer.Container.GetInstance<IClientHelper>();
            var region = clientHelper.GetClientRegion();

            var roles = new Dictionary<int, string> {
                { 1, "Management" },
                { 2, "Network Admins" },
                { 3, "Developers" },
                { 4, "Designers" } };

            var options = new List<DataSourceItem>();
            foreach (var item in roles)
            {
                options.Add(new DataSourceItem { ID = item.Key, Name = string.Format("{0} {1}", region, item.Value) });
            }

            return options;
        }
    }
}