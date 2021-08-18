Option Strict On
Imports System.Drawing
Imports System.Drawing.Text
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.Design
Imports System.ComponentModel.Design
Imports System.ComponentModel
<System.Drawing.ToolboxBitmap("E:\CPBar1.bmp")> _
Public Class CPBar

    Dim g_Alert As New ALERT(61, 75)
    Dim g_Critical As New CRITICAL(76, 100)


    Dim g_ValueText As String = "60"
    Dim g_HeadingText As String = "CPU"
    Dim g_FooterText As String = "%"

    Dim g_CCW As Boolean = False

    Dim g_BG_Color As Color = Color.Black
    Dim g_Header_Color As Color = Color.LightGray
    Dim g_Value_Color As Color = Color.LightGreen
    Dim g_Footer_Color As Color = Color.LightGray

    Dim g_ValueAlert_Color As Color = Color.Gold ' For values 50 to 70
    Dim g_ValueHighAlert_Color As Color = Color.HotPink ' For values 50 to 70
    Dim g_ValueDanger_Color As Color = Color.Red ' For values 70 to 100


    Dim g_Sweep_Color As Color = Color.Aqua
    'Dim g_Arc_Color As Color = Color.Chartreuse

    Dim g_BG_Brush As Brush = New SolidBrush(g_BG_Color)
    Dim g_HeaderBrush As Brush
    Dim g_ValueBrush As Brush = Brushes.LightGray
    Dim g_FooterBrush As Brush
    Dim g_AlertBrush As Brush
    Dim g_HighAlertBrush As Brush
    Dim g_DangerBrush As Brush



    Dim g_GreyPen As Pen
    Dim g_SweepPen1 As Pen
    Dim g_SweepPen2 As Pen
    Dim g_SweepPen3 As Pen
    Dim g_SpecularPen As Pen ' Specular Reflection Pen

    Dim g_BG_Rad As Integer = 150
    Dim g_Arc_Width As Integer = 10
    Dim g_InnerRad As Integer = 0
    Dim g_Start_Angle As Integer = 140
    Dim g_Sweep_Angle As Integer = 180
    Dim g_MaxSweep_Angle As Integer = 260 ' 360=Show Full Circle

    Dim g_OneDegree As Single = 3.6 ' 100%=360 Degree

    Dim g_DashStyle1 As Single() = {CSng(0.1 * g_BG_Rad), CSng(0.5 * g_BG_Rad), CSng(0.2 * g_BG_Rad), CSng(0.9 * g_BG_Rad)}
    Dim g_DashStyle2 As Single() = {CSng(0.3 * g_BG_Rad), CSng(0.25 * g_BG_Rad), CSng(0.3 * g_BG_Rad), CSng(0.1 * g_BG_Rad)}

    Dim g_ValueFont As New Font("Arial", CInt(0.2 * g_BG_Rad), FontStyle.Bold)
    Dim g_FooterFont As New Font("Arial", CInt(0.2 * g_BG_Rad), FontStyle.Bold)
    Dim g_HeadingFont As New Font("Arial", CInt(0.2 * g_BG_Rad), FontStyle.Bold)

    'Dim t As New Temperature With {.Value = 309.25, .Unit = TemperatureUnit.Kelvin}
    'Dim bt As BloodType


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Size = New Size(g_BG_Rad + 1, g_BG_Rad + 1)
        Me.BackColor = Color.Transparent
        Me.DoubleBuffered = True

        'SpeculatReflection
        g_SpecularPen = New Pen(Brushes.FloralWhite, 0.75)
        g_SpecularPen.StartCap = Drawing2D.LineCap.Round
        g_SpecularPen.EndCap = Drawing2D.LineCap.Round

        g_Sweep_Angle = CInt(g_MaxSweep_Angle / 100) * CInt(g_ValueText) - 20



    End Sub
    <System.ComponentModel.Category("CPBAR")> _
<System.ComponentModel.Description(" Specify  Alert Range Values. Ex:  66 , 75  ")> _
<TypeConverter(GetType(ALERTConverter))> _
<DefaultValue(True)>
    Public Property CPB_Alert_Range As ALERT
        Get
            Return g_Alert
        End Get

        Set(ByVal value As ALERT)

            g_Alert = value

        End Set
    End Property

    <System.ComponentModel.Category("CPBAR")> _
<System.ComponentModel.Description("Specify  Critical Range Values. Ex:  76 , 100 ")> _
    <TypeConverter(GetType(CRITICALConverter))> _
    <DefaultValue(True)>
    Public Property CPB_Critical_Range As CRITICAL
        Get
            Return g_Critical
        End Get

        Set(ByVal value As CRITICAL)

            g_Critical = value

        End Set
    End Property




    <System.ComponentModel.Category("CPBAR")> _
<System.ComponentModel.Description("If Set True, Changes Sweep Direction To Counter Clockwise Mode")> _
<DefaultValue(False)>
    Public Property CPB_SweepLeft As Boolean
        Get
            Return g_CCW
        End Get

        Set(ByVal value As Boolean)

            g_CCW = value

            ReSet_Pens_N_Brushes()

            Me.Invalidate()
        End Set
    End Property
    <System.ComponentModel.Category("CPBAR")> _
<System.ComponentModel.Description("Change BackgroundColor")> _
<DefaultValue(True)>
    Public Property CPB_BackColor As Color
        Get
            Return g_BG_Color
        End Get

        Set(ByVal value As Color)
            g_BG_Color = value
            ReSet_Pens_N_Brushes()
            Me.Invalidate()
        End Set
    End Property


    <System.ComponentModel.Category("CPBAR")> _
<System.ComponentModel.Description("Change SweepColor")> _
<DefaultValue(True)>
    Public Property CPB_SweepColor As Color
        Get
            Return g_Sweep_Color
        End Get

        Set(ByVal value As Color)
            g_Sweep_Color = value
            ReSet_Pens_N_Brushes()
            Me.Invalidate()
        End Set
    End Property

    <System.ComponentModel.Category("CPBAR")> _
<System.ComponentModel.Description("Change Alert Color")> _
<DefaultValue(True)>
    Public Property CPB_AlertColor As Color
        Get
            Return g_ValueAlert_Color
        End Get

        Set(ByVal value As Color)
            g_ValueAlert_Color = value
            ReSet_Pens_N_Brushes()
            Me.Invalidate()
        End Set
    End Property
    <System.ComponentModel.Category("CPBAR")> _
<System.ComponentModel.Description("Change Critical Color")> _
<DefaultValue(True)>
    Public Property CPB_CriticalColor As Color
        Get
            Return g_ValueDanger_Color
        End Get

        Set(ByVal value As Color)
            g_ValueDanger_Color = value
            ReSet_Pens_N_Brushes()
            Me.Invalidate()
        End Set
    End Property
    <System.ComponentModel.Category("CPBAR")> _
<System.ComponentModel.Description("Change First Line Text. Preferably 2 - 3 Characters ")> _
<DefaultValue(True)>
    Public Property CPB_Text_1 As String
        Get
            Return g_HeadingText
        End Get

        Set(ByVal value As String)
            g_HeadingText = value
            ReSet_Pens_N_Brushes()
            Me.Invalidate()
        End Set
    End Property
    <System.ComponentModel.Category("CPBAR")> _
<System.ComponentModel.Description("Change Middle Line Text. Preferably 2 - 3 Characters ")> _
<DefaultValue(True)>
    Public Property CPB_Text_2 As String 'Main Value
        Get
            Return g_ValueText
        End Get

        Set(ByVal value As String)


            Select Case CInt(value)
                Case Is < 0
                    g_ValueText = "0"
                Case Is > 100
                    g_ValueText = "100"
                Case Else
                    g_ValueText = value

            End Select

            g_OneDegree = CSng(g_MaxSweep_Angle / 100)


            g_Sweep_Angle = CInt(g_OneDegree * CInt(g_ValueText))
            ReSet_Pens_N_Brushes()
            Me.Invalidate()
        End Set
    End Property


    <System.ComponentModel.Category("CPBAR")> _
<System.ComponentModel.Description("Change Third Line Text. Preferably 2 - 3 Characters ")> _
<DefaultValue(True)>
    Public Property CPB_Text_3 As String
        Get
            Return g_FooterText
        End Get

        Set(ByVal value As String)
            g_FooterText = value

            ReSet_Pens_N_Brushes()
            Me.Invalidate()
        End Set
    End Property

    <System.ComponentModel.Category("CPBAR")> _
<System.ComponentModel.Description("Starting Angle Of The Sweep(0 To 360).Ex: 0=3'O Clock,90=6'O Clock,180=9'O Clock & 270= 12'O Clock Positions ")> _
<DefaultValue(True)>
    Public Property CPB_SweepStartAngle As Integer
        Get
            Return g_Start_Angle
        End Get

        Set(ByVal value As Integer)
            If IsNumeric(value) Then

                g_Start_Angle = CInt(IIf(value > 360, 360, value))
                g_Start_Angle = CInt(IIf(value < 0, 0, value))
            End If
            ' SetPens()
            Me.Invalidate()
        End Set
    End Property

    <System.ComponentModel.Category("CPBAR")> _
<System.ComponentModel.Description("Length Of Displayed Sweep .0=None, 360=Full")> _
<DefaultValue(True)>
    Public Property CPB_SweepMaximum As Integer
        Get
            Return g_MaxSweep_Angle
        End Get

        Set(ByVal value As Integer)

            Select Case value
                Case Is > 360
                    g_MaxSweep_Angle = 360
                Case Is < 0
                    g_MaxSweep_Angle = 0
                Case Else
                    g_MaxSweep_Angle = value
            End Select



            ReSet_Pens_N_Brushes()
            Me.Invalidate()
        End Set
    End Property





    Private Sub CPBar_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint

        Try
            Dim gr As Graphics = e.Graphics
            Dim InnerRad As Integer = CInt(g_BG_Rad - (0.2 * g_BG_Rad))
            Dim K As Integer = CInt((g_BG_Rad - InnerRad) / 2)
            Dim L As Integer = K ' (g_BG_Rad - InnerRad) / 2
            Dim NewSweepAngle As Integer

            Dim Direction As Integer = 1
            If g_CCW = True Then
                Direction = -1 'CW
            Else
                Direction = 1 'CCW 
            End If
            NewSweepAngle = g_Sweep_Angle * Direction

            g_SpecularPen.DashPattern = g_DashStyle1

            Select Case CInt(g_ValueText)
                
                Case g_Alert.From_Value To g_Alert.To_Value
                    g_ValueBrush = g_AlertBrush

                Case g_Alert.To_Value To g_Critical.From_Value
                    g_ValueBrush = g_HighAlertBrush

                Case g_Critical.From_Value To g_Critical.To_Value
                    g_ValueBrush = g_DangerBrush
                Case Else
                    g_ValueBrush = Brushes.Lime
            End Select




            With gr
                .SmoothingMode = Drawing2D.SmoothingMode.HighQuality

                '(1) FullBlack BG FilledCircle
                .FillEllipse(g_BG_Brush, 0, 0, g_BG_Rad, g_BG_Rad)
                '
                '(2) Full Sweep Gray BG Track
                '.DrawArc(g_GreyPen, K, L, InnerRad, InnerRad, 0, 360)
                .DrawArc(g_GreyPen, K, L, InnerRad, InnerRad, g_Start_Angle, g_MaxSweep_Angle * Direction)

                '(3) Actual Color Sweep (Dark)
                ' .DrawArc(g_SweepPen1, K, L, InnerRad, InnerRad, g_Start_Angle, g_Sweep_Angle)
                .DrawArc(g_SweepPen1, K, L, InnerRad, InnerRad, g_Start_Angle, NewSweepAngle)

                '(4) Actual Color Sweep (Bright)
                .DrawArc(g_SweepPen2, K, L, InnerRad, InnerRad, g_Start_Angle, NewSweepAngle)
                '(5) Actual Color Sweep (Brightest)
                .DrawArc(g_SweepPen3, K, L, InnerRad, InnerRad, g_Start_Angle, NewSweepAngle)
                '(6) Specular Reflection (Bright)
                .DrawArc(g_SpecularPen, K, L, InnerRad, InnerRad, g_Start_Angle, NewSweepAngle)





                'Render MainValue 100
                .TextRenderingHint = TextRenderingHint.AntiAlias
                Dim Measured As SizeF = gr.MeasureString(g_ValueText, g_ValueFont)
                .TextRenderingHint = TextRenderingHint.AntiAlias
                gr.DrawString(g_ValueText, g_ValueFont, g_ValueBrush, CInt(0.03 * g_BG_Rad) + (CInt((g_BG_Rad / 2) - (Measured.Width / 2))), CInt((g_BG_Rad / 2) - (Measured.Height / 2)) + 3)




                'Render % Symbol
                Measured = gr.MeasureString(g_FooterText, g_FooterFont)
                Dim XX As Integer = CInt((g_BG_Rad / 2) - (Measured.Width / 2))
                Dim YY As Integer = CInt((g_BG_Rad / 2) - (Measured.Height / 2))
                Dim Offset = CInt(0.25 * g_BG_Rad)




                .DrawString(g_FooterText, g_HeadingFont, Brushes.LightGray, CInt(0.03 * g_BG_Rad) + XX, YY + Offset)


                '.DrawRectangle(Pens.YellowGreen, XX, YY, Measured.Width, Measured.Height)

                'Render Heading (CPU)
                Measured = gr.MeasureString(g_HeadingText, g_HeadingFont)
                XX = CInt((g_BG_Rad / 2) - (Measured.Width / 2))
                YY = CInt((g_BG_Rad / 2) - (Measured.Height / 2))
                Offset = CInt(0.2 * g_BG_Rad)
                .DrawString(g_HeadingText, g_HeadingFont, Brushes.LightGray, CInt(0.03 * g_BG_Rad) + XX, YY - Offset)


            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message + ",   " + ",  " + g_Alert.From_Value.ToString + ",   " + g_Alert.To_Value.ToString, "CPBarPaint")

            'Dim trace = New System.Diagnostics.StackTrace(ex, True)
            'MsgBox(ex.Message & vbCrLf & "Error in Line number:" & trace.GetFrame(0).GetFileLineNumber().ToString)



            Threading.Thread.Sleep(5 * 1000)
        End Try

    End Sub

    Private Sub CPBar_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        Me.BackColor = Color.Transparent


        Try
            If Me.Height < 30 Then
                Me.Height = 30
            End If

            If Me.Height > 200 Then
                Me.Height = 200
            End If

            RemoveHandler MyBase.SizeChanged, AddressOf CPBar_SizeChanged
            Me.Size = New Size(Me.Height + 1, Me.Height + 1)
            g_BG_Rad = Me.Height - 1

            AddHandler MyBase.SizeChanged, AddressOf CPBar_SizeChanged


            ReSet_Pens_N_Brushes()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ReSet_Pens_N_Brushes()
        Select Case g_BG_Rad
            Case 30 To 40
                g_Arc_Width = 8
            Case 41 To 60
                g_Arc_Width = 9
            Case 61 To 80
                g_Arc_Width = 11
            Case 81 To 100
                g_Arc_Width = 12
            Case 101 To 130
                g_Arc_Width = 13
            Case 131 To 150
                g_Arc_Width = 14
            Case 151 To 180
                g_Arc_Width = 15
            Case 181 To 200
                g_Arc_Width = 16

        End Select

        If g_BG_Brush IsNot Nothing Then
            g_BG_Brush.Dispose()
            g_BG_Brush = Nothing
        End If

        'Main Value Font (100)
        If g_ValueFont IsNot Nothing Then
            g_ValueFont.Dispose()
            g_ValueFont = Nothing
        End If

        'Heading Font(CPU)
        If g_HeadingFont IsNot Nothing Then
            g_HeadingFont.Dispose()
            g_HeadingFont = Nothing
        End If

        ' Sign Font ("%")
        If g_FooterFont IsNot Nothing Then
            g_FooterFont.Dispose()
            g_FooterFont = Nothing
        End If


        If g_GreyPen IsNot Nothing Then
            g_GreyPen.Dispose()
            g_GreyPen = Nothing
        End If
        If g_SweepPen1 IsNot Nothing Then
            g_SweepPen1.Dispose()
            g_SweepPen1 = Nothing
        End If
        If g_SweepPen2 IsNot Nothing Then
            g_SweepPen2.Dispose()
            g_SweepPen2 = Nothing
        End If

        If g_SweepPen3 IsNot Nothing Then
            g_SweepPen3.Dispose()
            g_SweepPen3 = Nothing
        End If

        If g_AlertBrush IsNot Nothing Then
            g_AlertBrush.Dispose()
            g_AlertBrush = Nothing
        End If


        If g_HighAlertBrush IsNot Nothing Then
            g_HighAlertBrush.Dispose()
            g_HighAlertBrush = Nothing
        End If
        If g_DangerBrush IsNot Nothing Then
            g_DangerBrush.Dispose()
            g_DangerBrush = Nothing
        End If





        'For MainBG
        g_BG_Brush = New SolidBrush(g_BG_Color)


        'For Gray FullSweep  BG
        g_GreyPen = New Pen(Color.FromArgb(95, Color.LightGray), g_Arc_Width)
        g_GreyPen.StartCap = Drawing2D.LineCap.Round
        g_GreyPen.EndCap = Drawing2D.LineCap.Round

        g_SweepPen1 = New Pen(Color.FromArgb(95, g_Sweep_Color), g_Arc_Width)
        g_SweepPen1.StartCap = Drawing2D.LineCap.Round
        g_SweepPen1.EndCap = Drawing2D.LineCap.Round


        g_SweepPen2 = New Pen(Color.FromArgb(160, g_Sweep_Color), g_Arc_Width - 4)
        g_SweepPen2.StartCap = Drawing2D.LineCap.Round
        g_SweepPen2.EndCap = Drawing2D.LineCap.Round

        g_SweepPen3 = New Pen(g_Sweep_Color, 2)
        g_SweepPen3.StartCap = Drawing2D.LineCap.Round
        g_SweepPen3.EndCap = Drawing2D.LineCap.Round

        'Alert, HighAlert & Danger Brushes
        g_AlertBrush = New SolidBrush(g_ValueAlert_Color)
        g_HighAlertBrush = New SolidBrush(g_ValueHighAlert_Color)
        g_DangerBrush = New SolidBrush(g_ValueDanger_Color)


        'g_ValueFont = New Font("Arial", CInt(0.2 * g_BG_Rad), FontStyle.Bold)
        g_ValueFont = New Font("Arial", CInt(0.25 * g_BG_Rad), FontStyle.Bold)
        g_HeadingFont = New Font("Arial", CInt(0.1 * g_BG_Rad), FontStyle.Bold)
        g_FooterFont = New Font("Arial", CInt(0.125 * g_BG_Rad), FontStyle.Bold)




        Me.Refresh()
    End Sub
End Class 'CPBar



