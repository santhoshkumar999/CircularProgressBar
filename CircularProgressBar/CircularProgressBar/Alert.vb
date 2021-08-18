Imports System.ComponentModel

Public Class ALERT

    Dim g_Min As Integer = 66
    Dim g_Max As Integer = 75
    Public Sub New(ByVal From_Value As Integer, ByVal To_Value As Integer)
        g_Min = From_Value
        g_Max = To_Value
    End Sub
    Public Sub New()
        g_Min = 66
        g_Max = 75
    End Sub
    <TypeConverter(GetType(ALERT))> _
    <NotifyParentProperty(True)> _
    Public Property From_Value() As Integer
        Get
            Return g_Min
        End Get

        Set(ByVal value As Integer)
            g_Min = value

        End Set
    End Property
    <TypeConverter(GetType(ALERT))> _
     <NotifyParentProperty(True)> _
    Public Property To_Value() As Integer
        Get
            Return g_Max
        End Get

        Set(ByVal value As Integer)
            g_Max = value

        End Set
    End Property

End Class



Public Class ALERTConverter
    Inherits ExpandableObjectConverter
    Public Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext, ByVal sourceType As Type) As Boolean
        If sourceType = GetType(String) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function
    Public Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext, ByVal destinationType As Type) As Boolean
        If destinationType = GetType(String) Then
            Return True
        End If
        Return MyBase.CanConvertTo(context, destinationType)
    End Function
    Public Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
        If destinationType <> GetType(String) Then
            Return MyBase.ConvertTo(context, culture, value, destinationType)
        End If
        Dim A_MnMx As ALERT = TryCast(value, ALERT)
        If A_MnMx Is Nothing Then
            Return String.Empty
        End If

        Dim x As Integer
        If A_MnMx.From_Value > A_MnMx.To_Value Then
            x = A_MnMx.From_Value
            A_MnMx.From_Value = A_MnMx.To_Value
            A_MnMx.To_Value = x
        End If

        Select Case A_MnMx.From_Value
            Case Is < 0
                A_MnMx.From_Value = 0
            Case Is > 100
                A_MnMx.From_Value = 100
        End Select

        Select Case A_MnMx.To_Value
            Case Is < 0
                A_MnMx.To_Value = 0
            Case Is > 100
                A_MnMx.To_Value = 100
        End Select




        Return String.Format("{0} {1}", A_MnMx.From_Value.ToString + " To ", A_MnMx.To_Value.ToString)
    End Function

    Public Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        Dim StrValue As String = TryCast(value, String)
        If StrValue Is Nothing Then
            Return MyBase.ConvertFrom(context, culture, value)
        End If


        'Dim x As Integer = 55
        'Dim y As Integer = 66
        Dim p As Integer() = {61, 75}


        If StrValue.Contains("To") Or StrValue.Contains("to") Or StrValue.Contains(",") Or StrValue.Contains("-") Then
            If StrValue.Contains("To") Then

                p = GetNumbers(StrValue, "To")
            End If

            If StrValue.Contains(",") Then
                p = GetNumbers(StrValue, ",")
            End If

            If StrValue.Contains("-") Then
                p = GetNumbers(StrValue, "-")
            End If



 
        End If

        Return New ALERT With {.From_Value = p(0), .To_Value = p(1)}
    End Function

    Private Function GetNumbers(ByVal Str As String, ByVal Token As String) As Integer()

        Dim P(2) As Integer
        Str = Str.ToUpper
        Dim Q As String() = Split(Str, Token.ToUpper)


        Try
            If IsNumeric(Q(0).Trim) Then
                P(0) = Math.Abs(CInt(Q(0)))
            Else
                P(0) = 0
            End If

            If IsNumeric(Q(1).Trim) Then
                P(1) = Math.Abs(CInt(Q(1)))
            Else
                P(1) = 0
            End If

            Dim x As Integer
            If P(0) - P(1) > 0 Then
                x = P(0)
                P(0) = P(1)
                P(1) = x
            End If

            x = P(0)



            Select Case P(0)
                Case Is < 0
                    P(0) = 0
                Case Is > 100
                    P(0) = 100
            End Select

            Select Case P(1)
                Case Is < 0
                    P(1) = 0
                Case Is > 100
                    P(1) = 100
            End Select


            Return P
        Catch ex As Exception
            MessageBox.Show(ex.Message, "AlertClass")
        End Try

        Return P
    End Function
End Class
