﻿using ASP_MEMBER_WORKSHOP.Entitiy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP_MEMBER_WORKSHOP.Models
{
    public class UpdateMemberModel
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        public string password { get; set; }
        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public string position { get; set; }
        [Required]
        public RoleAccount role { get; set; }
        public string image { get; set; }
    }
}