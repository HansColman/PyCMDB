﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDB.Domain.Entities
{
    public class SubscriptionType : Model
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
        public AssetCategory Category { get; set; }
        public int? AssetCategoryId { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }


    }
}