using Area_51.Models.Enums;

namespace Area_51.Models;

public class Floor
{
    public Floor(FloorTypes floorType)
    {
        FloorType = floorType;
    }

    public FloorTypes FloorType { get; set; }

    public bool CanAccess(Agent agent)
    {
        bool result = false;

        if (agent.SecurityLevel == SecurityLevels.Secret)
        {
            if (FloorType == FloorTypes.S || FloorType == FloorTypes.G)
            {
                result = true;
            }
        }
        else if (agent.SecurityLevel == SecurityLevels.Confidential)
        {
            if (FloorType == FloorTypes.G)
            {
                result = true;
            }
        }
        else if (agent.SecurityLevel == SecurityLevels.TopSecret)
        {
            result = true;
        }

        return result;
    }


}