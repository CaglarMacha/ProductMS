using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Domain.Entities
{
    public class AuditedAggregateRoot<TKey>: Entity<TKey>
    {
        protected AuditedAggregateRoot(TKey id) : base(id)
        {

        }
        public AuditedAggregateRoot()
        {
                
        }
        public virtual DateTime CreationTime { get; set; } = DateTime.Now;
        public virtual Guid? CreatorId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual Guid? DeleterId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual Guid? LastModifierId { get; set; }
    }
}
