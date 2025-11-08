using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Data.Entites
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        public virtual ICollection<WorkFlowStep> AssignedSteps { get; set; }
    }
}
