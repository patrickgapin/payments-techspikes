using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeEffects.Rule;
using CodeEffects.Rule.Attributes;

namespace codeeffects_sample01.Models
{
    public class User
    {
        // The following shows the use of dynamic menu sources. We reference the GetRoles() method from Helper.cs
        [Field(DisplayName = "Client Role", DataSourceName = "roles")]
        public int RoleID { get; set; }

        [Field(DisplayName = "Customer ID", Description = "Unique ID for each customer")]
        public string ID { get; set; }

        [Field(DisplayName = "Customer First Name")]
        public string FirstName { get; set; }

        [Field(DisplayName = "Customer Last Name")]
        public string LastName { get; set; }

        [Method(DisplayName = "Authorize User")]
        public void Authorize([Parameter(DataSourceName = "statuses")] int statusID)
        {

        }
    }
}