using System;
using System.Configuration;
using InRule.Runtime;
using InRuleSandbox.Factories.Interfaces;
using InRuleSandbox.Helpers;
using InRuleSandbox.Models;
using InRuleSandbox.Services.Interfaces;

namespace InRuleSandbox.Services
{
    public class InRuleService<T> : IInRuleService<T> 
        where T : new()
    {
        private readonly IInRuleFactory _inRuleFactory;

        public InRuleService(IInRuleFactory inRuleFactory)
        {
            _inRuleFactory = inRuleFactory;
        }

        public void ExecuteRuleRequest(RuleExecutionModel<T> model)
        {
            // get rule application using settings from web.config
            var ruleApp =
                _inRuleFactory.CatalogRuleApplicationCreate(
                    ConfigurationManager.AppSettings[Constants.InRuleCatalogUri],
                    model.RuleApp,
                    ConfigurationManager.AppSettings[Constants.InRuleCatalogUser],
                    ConfigurationManager.AppSettings[Constants.InRuleCatalogPassword]);
            
            using (var session = _inRuleFactory.RuleSessionCreate(ruleApp))
            {
                if (string.IsNullOrEmpty(model.Entity)) throw new ArgumentException("Entity must be set!");

                // if an entity was specified, this is an entity based rule set
                var entity = session.CreateEntity(model.Entity, model.EntityObject);
                
                session.Settings.LogOptions = EngineLogOptions.Execution | EngineLogOptions.StateChanges;

                session.ApplyRules();

                //// set the response text based on the request type
                if (entity == null) return;

                foreach (var message in session.LastRuleExecutionLog.AllMessages)
                {
                    model.Response.Add(message.Description);
                }
            }
        }
    }
}