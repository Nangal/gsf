//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using System.Runtime.CompilerServices;
using GSF.ASN1;
using GSF.ASN1.Attributes;
using GSF.ASN1.Coders;

namespace GSF.MMS.Model
{
    [CompilerGenerated]
    [ASN1PreparedElement]
    [ASN1Sequence(Name = "ReportEventConditionListStatus_Request", IsSet = false)]
    public class ReportEventConditionListStatus_Request : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ReportEventConditionListStatus_Request));
        private Identifier continueAfter_;

        private bool continueAfter_present;
        private ObjectName eventConditionListName_;

        [ASN1Element(Name = "eventConditionListName", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public ObjectName EventConditionListName
        {
            get
            {
                return eventConditionListName_;
            }
            set
            {
                eventConditionListName_ = value;
            }
        }

        [ASN1Element(Name = "continueAfter", IsOptional = true, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public Identifier ContinueAfter
        {
            get
            {
                return continueAfter_;
            }
            set
            {
                continueAfter_ = value;
                continueAfter_present = true;
            }
        }


        public void initWithDefaults()
        {
        }


        public IASN1PreparedElementData PreparedData
        {
            get
            {
                return preparedData;
            }
        }

        public bool isContinueAfterPresent()
        {
            return continueAfter_present;
        }
    }
}