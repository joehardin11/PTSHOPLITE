﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.4.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(sender As Global.System.Object, e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("Data Source=PTI-SRV\SQLEXPRESS;Initial Catalog=SHOPDB;Integrated Security=True;Co" &
            "nnect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=Rea" &
            "dWrite;MultiSubnetFailover=False")>
        Public Property PartDatabaseString() As String
            Get
                Return CType(Me("PartDatabaseString"), String)
            End Get
            Set
                Me("PartDatabaseString") = Value
            End Set
        End Property

        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.ConnectionString),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\pti-srv\e2app\Blswin32\Dat\POLYTEC"& _ 
            "H\BLSDATA.MDB")>  _
        Public ReadOnly Property E2Database() As String
            Get
                Return CType(Me("E2Database"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.ConnectionString),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Data Source=PTI-SRV\SQLEXPRESS;Initial Catalog=SHOPDB;Integrated Security=True")>  _
        Public ReadOnly Property SHOPDB() As String
            Get
                Return CType(Me("SHOPDB"),String)
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property SQLPT() As Boolean
            Get
                Return CType(Me("SQLPT"),Boolean)
            End Get
            Set
                Me("SQLPT") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("SELECT PartNo, (PartNo + '-' + Descrip) As DisplayMember2, REPLACE(REPLACE(Displa"& _ 
            "yMember2, '/',''),'\', '') As DisplayMember From Estim ORDER BY PartNo ASC")>  _
        Public Property GetPartsQueryNoslash() As String
            Get
                Return CType(Me("GetPartsQueryNoslash"),String)
            End Get
            Set
                Me("GetPartsQueryNoslash") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("SELECT PartNo, (PartNo + '-' + Descrip) As DisplayMember From Estim ORDER BY Part"& _ 
            "No ASC")>  _
        Public Property GetPartQuery() As String
            Get
                Return CType(Me("GetPartQuery"),String)
            End Get
            Set
                Me("GetPartQuery") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("V:\Engineering\FINAL PRINTS")>  _
        Public Property DCFolder() As String
            Get
                Return CType(Me("DCFolder"),String)
            End Get
            Set
                Me("DCFolder") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.PTSHOPLITE.My.MySettings
            Get
                Return Global.PTSHOPLITE.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
