using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Domain.Entities
{
    public class Entity<TKey>
    {
        public virtual TKey Id { get; protected set; } = default!;

        protected Entity(TKey id)
        {
            Id = id;
        }
        public Entity()
        {
                
        }

    }
}
