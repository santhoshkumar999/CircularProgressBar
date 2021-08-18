Imports System.ComponentModel


Public Class Temperature
    Private m_Value As Double
    Private m_Unit As TemperatureUnit
    Public Property Value() As Double
        Get
            Return m_Value
        End Get
        Set(ByVal value As Double)
            m_Value = value
        End Set
    End Property

    Public Property Unit() As TemperatureUnit
        Get
            Return m_Unit
        End Get
        Set(ByVal value As TemperatureUnit)
            m_Unit = value
        End Set
    End Property
    Public Property SKValue() As Double
        Get
            Return 123.0
        End Get
        Set(ByVal value As Double)
            m_Value = value
        End Set
    End Property

End Class
Public Enum TemperatureUnit
    Kelvin
    Celsius
    Fahrenheit
End Enum
Public Class TemperatureTypeConverter
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
        Dim temp As Temperature = TryCast(value, Temperature)
        If temp Is Nothing Then
            Return String.Empty
        End If
        Return String.Format("{0} {1} {2}", temp.Value, temp.Unit.ToString().Substring(0, 1), "10.0")
    End Function
    Public Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        Dim stringValue As String = TryCast(value, String)
        If stringValue Is Nothing Then
            Return MyBase.ConvertFrom(context, culture, value)
        End If
        Dim number As String = ""
        For i As Integer = 0 To stringValue.Length - 1
            Dim c As Char = stringValue(i)
            If Char.IsNumber(c) Then
                number += stringValue(i)
            End If
            If c = ","c OrElse c = "."c Then
                number += culture.NumberFormat.NumberDecimalSeparator
            End If
        Next
        Dim unit As TemperatureUnit = TemperatureUnit.Kelvin
        If stringValue.ToUpper().Contains("K") Then
            unit = TemperatureUnit.Kelvin
        ElseIf stringValue.ToUpper().Contains("F") Then
            unit = TemperatureUnit.Fahrenheit
        ElseIf stringValue.ToUpper().Contains("C") Then
            unit = TemperatureUnit.Celsius
        Else
            'Dim item As PropertyGrid = TryCast(context, PropertyGridItem)
            'If item IsNot Nothing Then
            '    Dim oldTemp As Temperature = TryCast(item.Value, Temperature)
            '    If oldTemp IsNot Nothing Then
            '        unit = oldTemp.Unit
            '    End If
            'End If
        End If
        Return New Temperature() With { _
             .Value = Double.Parse(number), _
             .Unit = unit _
        }
    End Function

    
   
End Class
Public Class BloodTypeConverter
    Inherits TypeConverter
    Public Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
        Return True
    End Function
    Public Overrides Function GetStandardValuesExclusive(ByVal context As ITypeDescriptorContext) As Boolean
        Return True
    End Function
    Public Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
        Return New StandardValuesCollection(New String() {"O−", "O+", "A−", "A+", "B−", "B+", _
            "AB−", "AB+"})
    End Function
End Class

Public Class BloodType

End Class