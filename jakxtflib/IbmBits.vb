Module IbmBits

    '        static readonly int _ibmBase = 16;
    '        static readonly byte _exponentBias = 64;
    '        static readonly int _threeByteShift = 16777216;
    Const ibmBase As Integer = 16
    Const exponentBias As Byte = 64
    Const threeByteShift As Integer = 1677216

    '        /// <summary>
    '        /// Returns a 32-bit IEEE single precision floating point number from four bytes encoding
    '        /// a single precision number in IBM System/360 Floating Point format
    '        /// </summary>
    '        public static Single ToSingle(byte[] value)
    '        {
    Public Function ToSingle(value As Byte()) As Single
        '            if (ReferenceEquals(null, value))
        '                throw new ArgumentNullException("value");
        '            if (0 == BitConverter.ToInt32(value, 0))
        '                return 0;
        If value Is Nothing Then
            Return 0
        End If
        If BitConverter.ToInt32(value, 0) = 0 Then
            Return 0
        End If
        '            // The first bit is the sign.  The next 7 bits are the exponent.
        '            byte exponentBits = value[0];
        '            var sign = +1.0;
        Dim exponentBits As Byte = value(0)
        Dim sign As Double = 1.0

        '            // Remove sign from first bit
        '            if (exponentBits >= 128)
        '            {
        '                sign = -1.0;
        '                exponentBits -= 128;
        '            }
        If exponentBits >= 128 Then
            sign = -1.0
            exponentBits = CByte(exponentBits - 128)
        End If
        '            // Remove the bias from the exponent
        '            exponentBits -= _exponentBias;
        '            var exponent = Math.Pow(_ibmBase, exponentBits);
        exponentBits = exponentBits - exponentBias
        Dim exponent As Double = Math.Pow(ibmBase, exponentBits)
        '            // The fractional part is Big Endian unsigned int to the right of the radix point
        '            // So we reverse the bytes and pack them back into an int
        '            var fractionBytes = new byte[] { value[3], value[2], value[1], 0 };
        Dim fractionBytes As Byte() = {value(3), value(2), value(1), 0}

        '            // Note: The sign bit for int32 is in the last byte of the array, which is zero, so we don't have to convert to uint
        '            var mantissa = BitConverter.ToInt32(fractionBytes, 0);
        Dim mantissa As Integer = BitConverter.ToInt32(fractionBytes, 0)
        '            // And divide by 2^(8 * 3) to move the decimal all the way to the left
        '            var fraction = mantissa / (float)_threeByteShift;
        Dim fraction As Double = mantissa / threeByteShift
        '            return (float)(sign * exponent * fraction);
        Return CSng(sign * exponent * fraction)
        '        }
    End Function
    '
    '        /// <summary>
    '        /// Given a 32-bit IEEE single precision floating point number, returns four bytes encoding
    '        /// a single precision number in IBM System/360 Floating Point format
    '        /// </summary>
    '        public static byte[] GetBytes(Single value)
    '        {
    '            var bytes = new byte[4];
    '            if (value == 0)
    '                return bytes;
    '
    '            // Sign
    '            if (value < 0)
    '                bytes[0] = 128;
    '            var v = Math.Abs(value);
    '
    '            // Fraction
    '            // Find the number of digits (in the IBM base) we need to move the radix point to get a value that is less than 1
    '            var moveRadix = (int)Math.Log(v, _ibmBase) + 1;
    '            var fraction = v / (Math.Pow(_ibmBase, moveRadix));
    '            var fractionInt = (int)(_threeByteShift * fraction);
    '            var fractionBytes = BitConverter.GetBytes(fractionInt);
    '            bytes[3] = fractionBytes[0];
    '            bytes[2] = fractionBytes[1];
    '            bytes[1] = fractionBytes[2];
    '
    '            // Exponent
    '            var exponent = moveRadix + _exponentBias;
    '            bytes[0] += (byte)exponent;
    '            return bytes;
    '        }
    '
End Module
