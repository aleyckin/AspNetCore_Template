using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Utility
{
    public abstract class IId
    {
        public Guid Id { get; set; }
    }
}
