using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Data.Enums
{
    public enum ActionType
    {
        NoAction,
        Input,
        Approve,
        Reject
    }
    public enum ProcessStatus
    {

        Pending,
        Active,
        Completed
    }
    public enum FlowStage
    {
        New,
        InProgress,
        Closed
    }
    public enum FlowStatus
    {
        UnderReview,
        Accepted,
        Rejected
    }
}
