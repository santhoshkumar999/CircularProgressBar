Public Class Form1
    Private Sub InitializeComponent()
        Dim Alert1 As WindowApplication1.ALERT = New WindowApplication1.ALERT()
        Dim Critical1 As WindowApplication1.CRITICAL = New WindowApplication1.CRITICAL()
        Me.CpBar1 = New WindowApplication1.CPBar()
        Me.SuspendLayout()
        '
        'CpBar1
        '
        Me.CpBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CpBar1.BackColor = System.Drawing.Color.Transparent
        Alert1.From_Value = 61
        Alert1.To_Value = 75
        Me.CpBar1.CPB_Alert_Range = Alert1
        Me.CpBar1.CPB_AlertColor = System.Drawing.Color.Gold
        Me.CpBar1.CPB_BackColor = System.Drawing.Color.Black
        Critical1.From_Value = 76
        Critical1.To_Value = 100
        Me.CpBar1.CPB_Critical_Range = Critical1
        Me.CpBar1.CPB_CriticalColor = System.Drawing.Color.Red
        Me.CpBar1.CPB_SweepColor = System.Drawing.Color.Aqua
        Me.CpBar1.CPB_SweepMaximum = 260
        Me.CpBar1.CPB_SweepStartAngle = 140
        Me.CpBar1.CPB_Text_1 = "CPU"
        Me.CpBar1.CPB_Text_2 = "60"
        Me.CpBar1.CPB_Text_3 = "%"
        Me.CpBar1.Location = New System.Drawing.Point(46, 25)
        Me.CpBar1.Name = "CpBar1"
        Me.CpBar1.Size = New System.Drawing.Size(117, 117)
        Me.CpBar1.TabIndex = 0
        '
        'Form1
        '
        Me.ClientSize = New System.Drawing.Size(531, 372)
        Me.Controls.Add(Me.CpBar1)
        Me.Name = "Form1"
        Me.ResumeLayout(False)

    End Sub
End Class