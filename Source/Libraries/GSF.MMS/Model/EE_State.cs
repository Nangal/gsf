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
    [ASN1BoxedType(Name = "EE_State")]
    public class EE_State : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(EE_State));
        private long val;

        public EE_State()
        {
        }

        public EE_State(long value)
        {
            Value = value;
        }

        [ASN1Integer(Name = "EE-State")]
        public long Value
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
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
    }
}