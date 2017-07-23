using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCI.API.Rest.Models.Registration
{
    public class ConfirmEmailModel
    {
        public int Id  { get; set; }
        public string Token { get; set; }
    }
}