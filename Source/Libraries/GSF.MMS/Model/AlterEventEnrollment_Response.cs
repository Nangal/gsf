//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using System.Runtime.CompilerServices;
using GSF.ASN1;
using GSF.ASN1.Attributes;
using GSF.ASN1.Coders;
using GSF.ASN1.Types;

namespace GSF.MMS.Model
{
    [CompilerGenerated]
    [ASN1PreparedElement]
    [ASN1Sequence(Name = "AlterEventEnrollment_Response", IsSet = false)]
    public class AlterEventEnrollment_Response : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(AlterEventEnrollment_Response));
        private CurrentStateChoiceType currentState_;


        private EventTime transitionTime_;

        [ASN1Element(Name = "currentState", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public CurrentStateChoiceType CurrentState
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

        [ASN1Element(Name = "transitionTime", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public EventTime TransitionTime
        {
            get
            {
                return transitionTime_;
            }
            set
            {
                transitionTime_ = value;
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

        [ASN1PreparedElement]
        [ASN1Choice(Name = "currentState")]
        public class CurrentStateChoiceType : IASN1PreparedElement
        {
            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(CurrentStateChoiceType));
            private EE_State state_;
            private bool state_selected;


            private NullObject undefined_;
            private bool undefined_selected;

            [ASN1Element(Name = "state", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
            public EE_State State
            {
                get
                {
                    return state_;
                }
                set
                {
                    selectState(value);
                }
            }


            [ASN1Null(Name = "undefined")]
            [ASN1Element(Name = "undefined", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
            public NullObject Undefined
            {
                get
                {
                    return undefined_;
                }
                set
                {
                    selectUndefined(value);
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


            public bool isStateSelected()
            {
                return state_selected;
            }


            public void selectState(EE_State val)
            {
                state_ = val;
                state_selected = true;


                undefined_selected = false;
            }


            public bool isUndefinedSelected()
            {
                return undefined_selected;
            }


            public void selectUndefined()
            {
                selectUndefined(new NullObject());
            }


            public void selectUndefined(NullObject val)
            {
                undefined_ = val;
                undefined_selected = true;


                state_selected = false;
            }
        }
    }
}