using BackEndTask.Data.BaseModels;
using BackEndTask.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Data.Entites
{
    public class WorkFlow: FullAudit
    {
     
        public string Name { get; set; }
        public string Description { get; set; }
        public FlowStage Stage { get; set; }
        public FlowStatus Status { get; set; }
        public virtual ICollection<WorkFlowStep> FlowSteps { get; set; }


    }
}
