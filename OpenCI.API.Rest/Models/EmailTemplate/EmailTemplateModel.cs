using System;
using System.ComponentModel.DataAnnotations;

namespace OpenCI.API.Rest.Models.EmailTemplate
{
    public abstract class EmailTemplateModel
    {
        [Required]
        public Guid Guid { get; set; }
    }
}