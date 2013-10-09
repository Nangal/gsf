//
// This file was generated by the BinaryNotes compiler.
// See http://bnotes.sourceforge.net 
// Any modifications to this file will be lost upon recompilation of the source ASN.1. 
//

using GSF.ASN1;
using GSF.ASN1.Attributes;
using GSF.ASN1.Coders;

namespace GSF.MMS.Model
{
    [ASN1PreparedElement]
    [ASN1BoxedType(Name = "DeleteVariableAccess_Error")]
    public class DeleteVariableAccess_Error : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(DeleteVariableAccess_Error));
        private Unsigned32 val;


        [ASN1Element(Name = "DeleteVariableAccess-Error", IsOptional = false, HasTag = false, HasDefaultValue = false)]
        public Unsigned32 Value
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