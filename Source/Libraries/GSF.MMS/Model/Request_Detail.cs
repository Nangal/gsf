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
    [ASN1Choice(Name = "Request_Detail")]
    public class Request_Detail : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Request_Detail));
        private CS_AlterEventConditionMonitoring_Request alterEventConditionMonitoring_;
        private bool alterEventConditionMonitoring_selected;
        private CS_AlterEventEnrollment_Request alterEventEnrollment_;
        private bool alterEventEnrollment_selected;
        private CS_CreateProgramInvocation_Request createProgramInvocation_;
        private bool createProgramInvocation_selected;
        private CS_DefineEventCondition_Request defineEventCondition_;
        private bool defineEventCondition_selected;
        private CS_DefineEventEnrollment_Request defineEventEnrollment_;
        private bool defineEventEnrollment_selected;
        private NullObject otherRequests_;
        private bool otherRequests_selected;
        private CS_Resume_Request resume_;
        private bool resume_selected;


        private CS_Start_Request start_;
        private bool start_selected;

        [ASN1Null(Name = "otherRequests")]
        [ASN1Element(Name = "otherRequests", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public NullObject OtherRequests
        {
            get
            {
                return otherRequests_;
            }
            set
            {
                selectOtherRequests(value);
            }
        }

        [ASN1Element(Name = "createProgramInvocation", IsOptional = false, HasTag = true, Tag = 38, HasDefaultValue = false)]
        public CS_CreateProgramInvocation_Request CreateProgramInvocation
        {
            get
            {
                return createProgramInvocation_;
            }
            set
            {
                selectCreateProgramInvocation(value);
            }
        }


        [ASN1Element(Name = "start", IsOptional = false, HasTag = true, Tag = 40, HasDefaultValue = false)]
        public CS_Start_Request Start
        {
            get
            {
                return start_;
            }
            set
            {
                selectStart(value);
            }
        }


        [ASN1Element(Name = "resume", IsOptional = false, HasTag = true, Tag = 42, HasDefaultValue = false)]
        public CS_Resume_Request Resume
        {
            get
            {
                return resume_;
            }
            set
            {
                selectResume(value);
            }
        }


        [ASN1Element(Name = "defineEventCondition", IsOptional = false, HasTag = true, Tag = 47, HasDefaultValue = false)]
        public CS_DefineEventCondition_Request DefineEventCondition
        {
            get
            {
                return defineEventCondition_;
            }
            set
            {
                selectDefineEventCondition(value);
            }
        }


        [ASN1Element(Name = "alterEventConditionMonitoring", IsOptional = false, HasTag = true, Tag = 51, HasDefaultValue = false)]
        public CS_AlterEventConditionMonitoring_Request AlterEventConditionMonitoring
        {
            get
            {
                return alterEventConditionMonitoring_;
            }
            set
            {
                selectAlterEventConditionMonitoring(value);
            }
        }


        [ASN1Element(Name = "defineEventEnrollment", IsOptional = false, HasTag = true, Tag = 57, HasDefaultValue = false)]
        public CS_DefineEventEnrollment_Request DefineEventEnrollment
        {
            get
            {
                return defineEventEnrollment_;
            }
            set
            {
                selectDefineEventEnrollment(value);
            }
        }


        [ASN1Element(Name = "alterEventEnrollment", IsOptional = false, HasTag = true, Tag = 59, HasDefaultValue = false)]
        public CS_AlterEventEnrollment_Request AlterEventEnrollment
        {
            get
            {
                return alterEventEnrollment_;
            }
            set
            {
                selectAlterEventEnrollment(value);
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


        public bool isOtherRequestsSelected()
        {
            return otherRequests_selected;
        }


        public void selectOtherRequests()
        {
            selectOtherRequests(new NullObject());
        }


        public void selectOtherRequests(NullObject val)
        {
            otherRequests_ = val;
            otherRequests_selected = true;


            createProgramInvocation_selected = false;

            start_selected = false;

            resume_selected = false;

            defineEventCondition_selected = false;

            alterEventConditionMonitoring_selected = false;

            defineEventEnrollment_selected = false;

            alterEventEnrollment_selected = false;
        }


        public bool isCreateProgramInvocationSelected()
        {
            return createProgramInvocation_selected;
        }


        public void selectCreateProgramInvocation(CS_CreateProgramInvocation_Request val)
        {
            createProgramInvocation_ = val;
            createProgramInvocation_selected = true;


            otherRequests_selected = false;

            start_selected = false;

            resume_selected = false;

            defineEventCondition_selected = false;

            alterEventConditionMonitoring_selected = false;

            defineEventEnrollment_selected = false;

            alterEventEnrollment_selected = false;
        }


        public bool isStartSelected()
        {
            return start_selected;
        }


        public void selectStart(CS_Start_Request val)
        {
            start_ = val;
            start_selected = true;


            otherRequests_selected = false;

            createProgramInvocation_selected = false;

            resume_selected = false;

            defineEventCondition_selected = false;

            alterEventConditionMonitoring_selected = false;

            defineEventEnrollment_selected = false;

            alterEventEnrollment_selected = false;
        }


        public bool isResumeSelected()
        {
            return resume_selected;
        }


        public void selectResume(CS_Resume_Request val)
        {
            resume_ = val;
            resume_selected = true;


            otherRequests_selected = false;

            createProgramInvocation_selected = false;

            start_selected = false;

            defineEventCondition_selected = false;

            alterEventConditionMonitoring_selected = false;

            defineEventEnrollment_selected = false;

            alterEventEnrollment_selected = false;
        }


        public bool isDefineEventConditionSelected()
        {
            return defineEventCondition_selected;
        }


        public void selectDefineEventCondition(CS_DefineEventCondition_Request val)
        {
            defineEventCondition_ = val;
            defineEventCondition_selected = true;


            otherRequests_selected = false;

            createProgramInvocation_selected = false;

            start_selected = false;

            resume_selected = false;

            alterEventConditionMonitoring_selected = false;

            defineEventEnrollment_selected = false;

            alterEventEnrollment_selected = false;
        }


        public bool isAlterEventConditionMonitoringSelected()
        {
            return alterEventConditionMonitoring_selected;
        }


        public void selectAlterEventConditionMonitoring(CS_AlterEventConditionMonitoring_Request val)
        {
            alterEventConditionMonitoring_ = val;
            alterEventConditionMonitoring_selected = true;


            otherRequests_selected = false;

            createProgramInvocation_selected = false;

            start_selected = false;

            resume_selected = false;

            defineEventCondition_selected = false;

            defineEventEnrollment_selected = false;

            alterEventEnrollment_selected = false;
        }


        public bool isDefineEventEnrollmentSelected()
        {
            return defineEventEnrollment_selected;
        }


        public void selectDefineEventEnrollment(CS_DefineEventEnrollment_Request val)
        {
            defineEventEnrollment_ = val;
            defineEventEnrollment_selected = true;


            otherRequests_selected = false;

            createProgramInvocation_selected = false;

            start_selected = false;

            resume_selected = false;

            defineEventCondition_selected = false;

            alterEventConditionMonitoring_selected = false;

            alterEventEnrollment_selected = false;
        }


        public bool isAlterEventEnrollmentSelected()
        {
            return alterEventEnrollment_selected;
        }


        public void selectAlterEventEnrollment(CS_AlterEventEnrollment_Request val)
        {
            alterEventEnrollment_ = val;
            alterEventEnrollment_selected = true;


            otherRequests_selected = false;

            createProgramInvocation_selected = false;

            start_selected = false;

            resume_selected = false;

            defineEventCondition_selected = false;

            alterEventConditionMonitoring_selected = false;

            defineEventEnrollment_selected = false;
        }
    }
}