using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallenges.Domains
{
    public abstract class AuditableEntity
    {
        //string? CreatedBy { get; set; }
        //string? UpdatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
    }

    public interface IAuditableEntity
    {
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
