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
    [ASN1Choice(Name = "Address")]
    public class Address : IASN1PreparedElement
    {
        private static readonly IASN1PreparedElementData preparedData = CoderFactory.getInstance().newPreparedElementData(typeof(Address));
        private Unsigned32 numericAddress_;
        private bool numericAddress_selected;


        private MMSString symbolicAddress_;
        private bool symbolicAddress_selected;


        private byte[] unconstrainedAddress_;
        private bool unconstrainedAddress_selected;

        [ASN1Element(Name = "numericAddress", IsOptional = false, HasTag = true, Tag = 0, HasDefaultValue = false)]
        public Unsigned32 NumericAddress
        {
            get
            {
                return numericAddress_;
            }
            set
            {
                selectNumericAddress(value);
            }
        }

        [ASN1Element(Name = "symbolicAddress", IsOptional = false, HasTag = true, Tag = 1, HasDefaultValue = false)]
        public MMSString SymbolicAddress
        {
            get
            {
                return symbolicAddress_;
            }
            set
            {
                selectSymbolicAddress(value);
            }
        }


        [ASN1OctetString(Name = "")]
        [ASN1Element(Name = "unconstrainedAddress", IsOptional = false, HasTag = true, Tag = 2, HasDefaultValue = false)]
        public byte[] UnconstrainedAddress
        {
            get
            {
                return unconstrainedAddress_;
            }
            set
            {
                selectUnconstrainedAddress(value);
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


        public bool isNumericAddressSelected()
        {
            return numericAddress_selected;
        }


        public void selectNumericAddress(Unsigned32 val)
        {
            numericAddress_ = val;
            numericAddress_selected = true;


            symbolicAddress_selected = false;

            unconstrainedAddress_selected = false;
        }


        public bool isSymbolicAddressSelected()
        {
            return symbolicAddress_selected;
        }


        public void selectSymbolicAddress(MMSString val)
        {
            symbolicAddress_ = val;
            symbolicAddress_selected = true;


            numericAddress_selected = false;

            unconstrainedAddress_selected = false;
        }


        public bool isUnconstrainedAddressSelected()
        {
            return unconstrainedAddress_selected;
        }


        public void selectUnconstrainedAddress(byte[] val)
        {
            unconstrainedAddress_ = val;
            unconstrainedAddress_selected = true;


            numericAddress_selected = false;

            symbolicAddress_selected = false;
        }
    }
}