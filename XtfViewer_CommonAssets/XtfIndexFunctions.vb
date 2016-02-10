Partial Public Class XtfIndex
    ''' <summary>
    ''' Compare the value with a min/max value and update the min or max if necessary
    ''' </summary>
    ''' <param name="ValueToCompare">Value to compare</param>
    ''' <param name="MinValue">Minimum value</param>
    ''' <param name="MaxValue">Maximum value</param>
    Private Sub UpdateMinMax(ValueToCompare As Byte, ByRef MinValue As Byte, ByRef MaxValue As Byte)
        If ValueToCompare < MinValue Then MinValue = ValueToCompare
        If ValueToCompare > MaxValue Then MaxValue = ValueToCompare
    End Sub

    ''' <summary>
    ''' Compare the value with a min/max value and update the min or max if necessary
    ''' </summary>
    ''' <param name="ValueToCompare">Value to compare</param>
    ''' <param name="MinValue">Minimum value</param>
    ''' <param name="MaxValue">Maximum value</param>
    Private Sub UpdateMinMax(ValueToCompare As UInt16, ByRef MinValue As UInt16, ByRef MaxValue As UInt16)
        If ValueToCompare < MinValue Then MinValue = ValueToCompare
        If ValueToCompare > MaxValue Then MaxValue = ValueToCompare
    End Sub

    ''' <summary>
    ''' Compare the value with a min/max value and update the min or max if necessary
    ''' </summary>
    ''' <param name="ValueToCompare">Value to compare</param>
    ''' <param name="MinValue">Minimum value</param>
    ''' <param name="MaxValue">Maximum value</param>
    Private Sub UpdateMinMax(ValueToCompare As UInt32, ByRef MinValue As UInt32, ByRef MaxValue As UInt32)
        If ValueToCompare < MinValue Then MinValue = ValueToCompare
        If ValueToCompare > MaxValue Then MaxValue = ValueToCompare
    End Sub

    ''' <summary>
    ''' Compare the value with a min/max value and update the min or max if necessary
    ''' </summary>
    ''' <param name="ValueToCompare">Value to compare</param>
    ''' <param name="MinValue">Minimum value</param>
    ''' <param name="MaxValue">Maximum value</param>
    Private Sub UpdateMinMax(ValueToCompare As Date, ByRef MinValue As Date, ByRef MaxValue As Date)
        If ValueToCompare < MinValue Then MinValue = ValueToCompare
        If ValueToCompare > MaxValue Then MaxValue = ValueToCompare
    End Sub

    ''' <summary>
    ''' Compare the value with a min/max value and update the min or max if necessary
    ''' </summary>
    ''' <param name="ValueToCompare">Value to compare</param>
    ''' <param name="MinValue">Minimum value</param>
    ''' <param name="MaxValue">Maximum value</param>
    Private Sub UpdateMinMax(ValueToCompare As Integer, ByRef MinValue As Integer, ByRef MaxValue As Integer)
        If ValueToCompare < MinValue Then MinValue = ValueToCompare
        If ValueToCompare > MaxValue Then MaxValue = ValueToCompare
    End Sub

End Class
