Public Class ProgressableEventArgs
    Inherits System.EventArgs

    Sub New(ByVal current As Long, ByVal total As Long)
        If (total = 0) Then
            Throw New ArgumentException("total cannot be 0")
        End If
        Me.currentLength = current
        Me.totalLength = total
    End Sub

    Sub New(ByVal total As Long)
        If (total = 0) Then
            Throw New ArgumentException("total cannot be 0")
        End If
        Me.currentLength = 0
        Me.totalLength = total

    End Sub

    Public Property wasCancelled As Boolean
    Public Property currentLength As Long
    Public Property totalLength As Long

End Class


Public Interface IProgressable

    Property wasCancelled As Boolean

    Event processStart(ByVal sender As Object, ByVal e As ProgressableEventArgs)
    Event processGoing(ByVal sender As Object, ByVal e As ProgressableEventArgs)
    Event processFinished(ByVal sender As Object, ByVal e As ProgressableEventArgs)

End Interface

''' <summary>
''' Class that encapsulates a form with a progress bar and 
''' a backround worker
''' </summary>
''' <remarks></remarks>
Public Class ProgressIndicator

    Private Const STR_FORM_PROGRESS As String = "frmProgress"

    Private _frmProgress As frmProgress

    Private TotalSteps As Integer

    Public Property showCancel As Boolean


    Public ReadOnly Property frmProgress As frmProgress
        Get
            If _frmProgress Is Nothing Then
                If Application.OpenForms(STR_FORM_PROGRESS) Is Nothing Then
                    _frmProgress = New frmProgress
                Else
                    _frmProgress = CType(Application.OpenForms(STR_FORM_PROGRESS), controls.frmProgress)
                End If
            End If
            Return _frmProgress

        End Get
    End Property

    Public Property progressWindowTitle() As String
        Get
            Return frmProgress.Text
        End Get
        Set(ByVal value As String)

            ' InvokeRequired required compares the thread ID of the 
            ' calling thread to the thread ID of the creating thread. 
            ' If these threads are different, it returns true. 
            If Me.frmProgress.InvokeRequired Then
                Dim d As New SetTextCallback(AddressOf SetText)
                Me.frmProgress.Invoke(d, New Object() {value})
            Else
                Me.frmProgress.Text = value
            End If


        End Set
    End Property

    Private Delegate Sub SetTextCallback(ByVal [text] As String)

    Private Sub SetText(ByVal x As String)
        frmProgress.Text = x
    End Sub


    Public Sub Start(ByVal workMethod As System.ComponentModel.DoWorkEventHandler)

        Me.showCancel = showCancel

        frmProgress.Text = Me.progressWindowTitle
        frmProgress.Refresh()
        frmProgress.btnCancel.Visible = Me.showCancel
        frmProgress.lblMessage.Text = String.Empty
        AddHandler frmProgress.backroundWorkerProgress.DoWork, workMethod
        AddHandler frmProgress.backroundWorkerProgress.RunWorkerCompleted, AddressOf RunWorkerCompleted

        frmProgress.backroundWorkerProgress.RunWorkerAsync(frmProgress.lblMessage)

        frmProgress.TopMost = True
        frmProgress.Show()
        frmProgress.Activate()
        frmProgress.BringToFront()

    End Sub

    Public Sub prgEnd()

        If frmProgress.backroundWorkerProgress.IsBusy Then
            frmProgress.backroundWorkerProgress.CancelAsync()
        End If

        frmProgress.Close()

    End Sub

    Private Sub RunWorkerCompleted(ByVal sender As Object, _
                                   ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)

        Call prgEnd()

    End Sub

End Class
