using BackEndTask.Data.BaseModels;
using BackEndTask.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Data.Entites
{
    public class WorkFlowStep:FullAudit
    {
        [ForeignKey(nameof(WorkFlow))]
        public Guid? WorkFlowId { get; set; }
        [ForeignKey(nameof(Step))]
        public Guid? StepId { get; set; }
     
        public ActionType ActionType { get; set; }
        public ProcessStatus Status { get; set; }
        public int Order { get; set; }
        [ForeignKey(nameof(AssignedUser))]
        public Guid? UserId { get; set; }
        public virtual WorkFlow WorkFlow { get; set; }
        public virtual Step Step { get; set; }
        public virtual ApplicationUser AssignedUser { get; set; }

    }
}
