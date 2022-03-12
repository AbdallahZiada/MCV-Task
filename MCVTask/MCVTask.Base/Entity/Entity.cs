using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCVTask.Base.Entity
{
    public class Entity<T>
    {
        public virtual T Id { get; protected set; }

        public DateTime CreationDate { get; protected set; }

        public bool IsDeleted { get; protected set; }

        public Entity()
        {
            CreationDate = DateTime.Now;
            IsDeleted = false;
        }
    }
}
