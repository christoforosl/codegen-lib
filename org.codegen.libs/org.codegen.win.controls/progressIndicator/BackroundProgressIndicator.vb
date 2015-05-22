Imports org.codegen.common.ProgressIndicator

Namespace BackroundProgressIndicator
    ''' <summary>
    ''' Class that encapsulates a form with a progress bar and 
    ''' a backround worker
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BackroundProgressIndicator
        Implements IDisposable

        Public Const STR_FORM_PROGRESS As String = "frmProgress"

        Private _frmProgress As frmProgress

        Private _isDone As Boolean

        ''' <summary>
        ''' Returns a boolean value indicating wheher the process was finished or not
        ''' 
        ''' </summary>
        Public ReadOnly Property isDone As Boolean
            Get
                Return _isDone
            End Get
        End Property


        ''' <summary>
        ''' Gets/Sets show cancel button indicator. If false, the process will not cancellable
        ''' </summary>
        Public Property showCancel As Boolean


        Private ReadOnly Property frmProgress As frmProgress
            Get
                If _frmProgress Is Nothing Then

                    If Application.OpenForms(STR_FORM_PROGRESS) Is Nothing Then
                        _frmProgress = New frmProgress
                    Else
                        _frmProgress = CType(Application.OpenForms(STR_FORM_PROGRESS),  _
                                    controls.BackroundProgressIndicator.frmProgress)
                    End If

                End If
                Return _frmProgress

            End Get
        End Property

        Public Property progressWindowTitle() As String
            Get
                Return Me.frmProgress.Text
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
        Private Delegate Sub CloseCallback()

        Private Sub SetText(ByVal x As String)
            frmProgress.Text = x
        End Sub

        Private Sub CloseForm()
            frmProgress.Close()
        End Sub

        Public Sub Start(ByVal workMethod As System.ComponentModel.DoWorkEventHandler)

            Me.frmProgress.Text = Me.progressWindowTitle
            Me.frmProgress.Refresh()
            Me.frmProgress.setShowCancelButton(Me.showCancel)
            Me.frmProgress.setMessageLabelText(String.Empty)
            Me.frmProgress.TopMost = True
            Me.frmProgress.Show()
            Me.frmProgress.Activate()
            Me.frmProgress.BringToFront()
            Me.frmProgress.Refresh()
            Me._isDone = False

            AddHandler frmProgress.ProgressCancelled, AddressOf progressCancelled
            AddHandler frmProgress.backroundWorkerProgress.DoWork, workMethod
            AddHandler frmProgress.backroundWorkerProgress.RunWorkerCompleted, _
                                AddressOf RunWorkerCompleted

            AddHandler frmProgress.backroundWorkerProgress.ProgressChanged, _
                               AddressOf ProgressChanged

            Me.frmProgress.backroundWorkerProgress.RunWorkerAsync()

        End Sub

        Public Sub EndProcess()

            If Me.frmProgress.InvokeRequired Then
                Dim d As New CloseCallback(AddressOf CloseForm)
                Me.frmProgress.Invoke(d, New Object() {})
            Else
                Me.frmProgress.Close()
            End If
            Me._isDone = True

        End Sub

        Private Sub RunWorkerCompleted(ByVal sender As Object, _
                     ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)

            Me.frmProgress.Close()
            Me._isDone = True

        End Sub

        Private Sub ProgressChanged(ByVal sender As Object, _
                ByVal e As System.ComponentModel.ProgressChangedEventArgs)

            Dim msg As String = String.Empty
            If e.UserState IsNot Nothing Then
                msg = e.UserState.ToString()
            End If

            Call Me.frmProgress.Progress(e.ProgressPercentage, msg)
            Me.frmProgress.Refresh()
        End Sub

        Private Sub progressCancelled(ByVal sender As Object, ByVal e As EventArgs)

            Me.frmProgress.backroundWorkerProgress.CancelAsync()
            Me.frmProgress.Hide()
            Me._isDone = True

        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    If Me._frmProgress IsNot Nothing Then
                        _frmProgress.Dispose()
                    End If
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
