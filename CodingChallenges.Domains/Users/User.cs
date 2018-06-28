using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallenges.Domains.Users
{
    public class User : IdentityUser, IAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Password { get; set; }
        public byte Gender { get; set; }

        public string NickName { get; set; }

        private DateTime? createdDate;
        //[DataType(DataType.DateTime)]
        public DateTime CreatedDate
        {
            get { return createdDate ?? DateTime.UtcNow; }
            set { createdDate = value; }
        }

        //[DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }


        public string ModifiedBy { get; set; }

        public int TenantId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public bool IsEmailVerificationSent { get; set; }

        public bool IsEmailVerified { get; set; }

        public bool IsTermsConditionAccepted { get; set; }

        public bool IsWelcomeEmailSent { get; set; }

        public bool ForcePasswordReset { get; set; }

        public bool ForceProfileReview { get; set; }

        public bool ForceToReviewTermsCondition { get; set; }

        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual string FriendlyName
        {
            get
            {
                string friendlyName = string.IsNullOrWhiteSpace(FullName) ? UserName : FullName;

                if (!string.IsNullOrWhiteSpace(JobTitle))
                    friendlyName = $"{JobTitle} {friendlyName}";

                return friendlyName;
            }
        }


        public string JobTitle { get; set; }
        public string FullName { get; set; }
        public string Configuration { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsLockedOut => this.LockoutEnabled && this.LockoutEnd >= DateTimeOffset.UtcNow;
        //[InverseProperty("ApplicationUser")]
        //public ApplicationUserProfile ApplicationUserProfile { get; set; }
        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

    }
}
