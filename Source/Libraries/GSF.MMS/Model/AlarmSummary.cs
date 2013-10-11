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
    [ASN1Sequence(Name = "AlarmSummary", IsSet = false)]
    public class AlarmSummary : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(AlarmSummary));
        private EC_State currentState_;


        private EN_Additional_Detail displayEnhancement_;

        private bool displayEnhancement_present;
        private ObjectName eventConditionName_;
        private Unsigned8 severity_;


        private EventTime timeOfLastTransitionToActive_;

        private bool timeOfLastTransitionToActive_present;


        private EventTime timeOfLastTransitionToIdle_;

        private bool timeOfLastTransitionToIdle_present;
        private long unacknowledgedState_;

        [ASN1Element(Name = "eventConditionName", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public ObjectName EventConditionName
        {
            get
            {
                return eventConditionName_;
            }
            set
            {
                eventConditionName_ = value;
            }
        }

        [ASN1Element(Name = "severity", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public Unsigned8 Severity
        {
            get
            {
                return severity_;
            }
            set
            {
                severity_ = value;
            }
        }

        [ASN1Element(Name = "currentState", IsOptional = false, HasTag = true, Tag = 2, HasDefaultValue = false)]
        public EC_State CurrentState
        {
            get
            {
                return currentState_;
            }
            set
            {
                currentState_ = value;
            }
        }

        [ASN1Integer(Name = "")]
        [ASN1Element(Name = "unacknowledgedState", IsOptional = false, HasTag = true, Tag = 3, HasDefaultValue = false)]
        public long UnacknowledgedState
        {
            get
            {
                return unacknowledgedState_;
            }
            set
            {
                unacknowledgedState_ = value;
            }
        }

        [ASN1Element(Name = "displayEnhancement", IsOptional = true, HasTag = true, Tag = 4, HasDefaultValue = false)]
        public EN_Additional_Detail DisplayEnhancement
        {
            get
            {
                return displayEnhancement_;
            }
            set
            {
                displayEnhancement_ = value;
                displayEnhancement_present = true;
            }
        }

        [ASN1Element(Name = "timeOfLastTransitionToActive", IsOptional = true, HasTag = true, Tag = 5, HasDefaultValue = false)]
        public EventTime TimeOfLastTransitionToActive
        {
            get
            {
                return timeOfLastTransitionToActive_;
            }
            set
            {
                timeOfLastTransitionToActive_ = value;
                timeOfLastTransitionToActive_present = true;
            }
        }

        [ASN1Element(Name = "timeOfLastTransitionToIdle", IsOptional = true, HasTag = true, Tag = 6, HasDefaultValue = false)]
        public EventTime TimeOfLastTransitionToIdle
        {
            get
            {
                return timeOfLastTransitionToIdle_;
            }
            set
            {
                timeOfLastTransitionToIdle_ = value;
                timeOfLastTransitionToIdle_present = true;
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


        public bool isDisplayEnhancementPresent()
        {
            return displayEnhancement_present;
        }

        public bool isTimeOfLastTransitionToActivePresent()
        {
            return timeOfLastTransitionToActive_present;
        }

        public bool isTimeOfLastTransitionToIdlePresent()
        {
            return timeOfLastTransitionToIdle_present;
        }
    }
}