﻿Option Strict On

'<comments>
'************************************************************
'
' Class autogenerated on 24-Jul-2011 10:23:56 AM by ModelGenerator
' Extends base model object class
' *** FELL FREE to change code in this class.  
'     It will NOT be re-generated and 
'     overwritten by the code generator ****
' 
'************************************************************
'</comments>

Namespace BusObjects

	Public Class EmployeeFactory
		
		'Shared function to create a new instance of the class and return it.
		'you can create other shared functions to return a new 
		'instance with parameters
		Public Shared Function Create() As Employee
            Return New Employee()
        End Function
    
    End Class


    <Serializable()> _
    Public Class Employee
        Inherits EmployeeBase
        Implements IEmployee

#Region "Constructor"

        Public Sub New()

            'Empty constructor.  

        End Sub

#End Region

#Region "Overrides"

#End Region

#Region "Before Save,After Save and Validate Overriden Methods "

#End Region

#Region "Shadowed and Other Methods "

#End Region

#Region "Methods "

#End Region

    End Class

End Namespace

