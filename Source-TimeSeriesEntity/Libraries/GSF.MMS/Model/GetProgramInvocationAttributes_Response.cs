//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using System.Runtime.CompilerServices;
using System.Collections.Generic;
using GSF.ASN1;
using GSF.ASN1.Attributes;
using GSF.ASN1.Coders;

namespace GSF.MMS.Model
{
    [CompilerGenerated]
    [ASN1PreparedElement]
    [ASN1Sequence(Name = "GetProgramInvocationAttributes_Response", IsSet = false)]
    public class GetProgramInvocationAttributes_Response : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(GetProgramInvocationAttributes_Response));
        private Identifier accessControlList_;

        private bool accessControlList_present;
        private ExecutionArgumentChoiceType executionArgument_;
        private ICollection<Identifier> listOfDomainNames_;
        private bool mmsDeletable_;
        private bool monitor_;
        private bool reusable_;
        private ProgramInvocationState state_;

        [ASN1Element(Name = "state", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public ProgramInvocationState State
        {
            get
            {
                return state_;
            }
            set
            {
                state_ = value;
            }
        }


        [ASN1SequenceOf(Name = "listOfDomainNames", IsSetOf = false)]
        [ASN1Element(Name = "listOfDomainNames", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public ICollection<Identifier> ListOfDomainNames
        {
            get
            {
                return listOfDomainNames_;
            }
            set
            {
                listOfDomainNames_ = value;
            }
        }


        [ASN1Boolean(Name = "")]
        [ASN1Element(Name = "mmsDeletable", IsOptional = false, HasTag = true, Tag = 2, HasDefaultValue = false)]
        public bool MmsDeletable
        {
            get
            {
                return mmsDeletable_;
            }
            set
            {
                mmsDeletable_ = value;
            }
        }


        [ASN1Boolean(Name = "")]
        [ASN1Element(Name = "reusable", IsOptional = false, HasTag = true, Tag = 3, HasDefaultValue = false)]
        public bool Reusable
        {
            get
            {
                return reusable_;
            }
            set
            {
                reusable_ = value;
            }
        }


        [ASN1Boolean(Name = "")]
        [ASN1Element(Name = "monitor", IsOptional = false, HasTag = true, Tag = 4, HasDefaultValue = false)]
        public bool Monitor
        {
            get
            {
                return monitor_;
            }
            set
            {
                monitor_ = value;
            }
        }


        [ASN1Element(Name = "executionArgument", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public ExecutionArgumentChoiceType ExecutionArgument
        {
            get
            {
                return executionArgument_;
            }
            set
            {
                executionArgument_ = value;
            }
        }


        [ASN1Element(Name = "accessControlList", IsOptional = true, HasTag = true, Tag = 6, HasDefaultValue = false)]
        public Identifier AccessControlList
        {
            get
            {
                return accessControlList_;
            }
            set
            {
                accessControlList_ = value;
                accessControlList_present = true;
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

        public bool isAccessControlListPresent()
        {
            return accessControlList_present;
        }

        [ASN1PreparedElement]
        [ASN1Choice(Name = "executionArgument")]
        public class ExecutionArgumentChoiceType : IASN1PreparedElement
        {
            private static IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(ExecutionArgumentChoiceType));
            private MMSString simpleString_;
            private bool simpleString_selected;


            [ASN1Element(Name = "simpleString", IsOptional = false, HasTag = true, Tag = 5, HasDefaultValue = false)]
            public MMSString SimpleString
            {
                get
                {
                    return simpleString_;
                }
                set
                {
                    selectSimpleString(value);
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

            public bool isSimpleStringSelected()
            {
                return simpleString_selected;
            }


            public void selectSimpleString(MMSString val)
            {
                simpleString_ = val;
                simpleString_selected = true;
            }
        }
    }
}