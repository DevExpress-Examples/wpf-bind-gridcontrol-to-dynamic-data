Imports System
Imports System.ComponentModel

Namespace VirtualSources.InfiniteAsyncSource

    Friend Class CustomField

        Public Sub New(ByVal name As String, ByVal dataType As Type)
            Me.Name = name
            Me.DataType = dataType
        End Sub

        Public ReadOnly Property Name As String

        Public ReadOnly Property DataType As Type
    End Class

    Friend Class ItemPropertyDescriptor
        Inherits PropertyDescriptor

        Public Sub New(ByVal customField As CustomField)
            MyBase.New(customField.Name, Array.Empty(Of Attribute)())
            Me.CustomField = customField
        End Sub

        Public ReadOnly Property CustomField As CustomField

        Public Overrides ReadOnly Property ComponentType As Type
            Get
                Return GetType(Item)
            End Get
        End Property

        Public Overrides ReadOnly Property IsReadOnly As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property PropertyType As Type
            Get
                Return CustomField.DataType
            End Get
        End Property

        Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
            Return False
        End Function

        Public Overrides Function GetValue(ByVal component As Object) As Object
            Dim item = CType(component, Item)
            Return If(item(CustomField.Name), If(CustomField.DataType.IsValueType, Activator.CreateInstance(CustomField.DataType), Nothing))
        End Function

        Public Overrides Sub ResetValue(ByVal component As Object)
            Throw New NotImplementedException()
        End Sub

        Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
            Dim item = CType(component, Item)
            item(CustomField.Name) = value
        End Sub

        Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
            Return False
        End Function
    End Class
End Namespace
