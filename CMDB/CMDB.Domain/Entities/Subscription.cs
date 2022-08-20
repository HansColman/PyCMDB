﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMDB.Domain.Entities
{
    public class Subscription : Model
    {
        [Key]
        public int SubscriptionId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public int? SubsctiptionTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public Identity Identity { get; set; }
        public int? IdentityId { get; set; }
        public Mobile Mobile { get; set; }
        public int? MobileId { get; set; }
        public AssetCategory Category { get; set; }
        public int? AssetCategoryId { get; set; }
    }
}
