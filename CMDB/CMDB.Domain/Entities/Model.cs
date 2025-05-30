﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDB.Domain.Entities
{
    public class Model
    {
        public int active { get; set; }
        public Model()
        {
            Logs = new List<Log>();
        }
        public virtual ICollection<Log> Logs { get; set; }
        [NotMapped]
        public virtual State Active
        {
            get
            {
                return active switch
                {
                    1 => State.Active,
                    0 => State.Inactive,
                    _ => State.Unknown
                };
            }
            set
            {
                active = value switch
                {
                    State.Active => 1,
                    State.Inactive => 0,
                    _ => 1,
                };
            }
        }
        [Column("Deactivate_reason")]
        public string DeactivateReason { get; set; }
        public virtual Admin LastModifiedAdmin { get; set; }
        public int? LastModifiedAdminId { get; set; }
    }
    public enum State
    {
        Inactive,
        Active,
        Unknown
    }
}
