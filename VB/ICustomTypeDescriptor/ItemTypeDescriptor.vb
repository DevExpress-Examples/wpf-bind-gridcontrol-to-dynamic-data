Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports ICustomTypeDescriptor.ViewModels

Namespace ICustomTypeDescriptor

    Friend Class CustomField

        Public Sub New(ByVal name As String, ByVal dataType As Type)
            Me.Name = name
            Me.DataType = dataType
        End Sub

        Public ReadOnly Property Name As String

        Public ReadOnly Property DataType As Type
    End Class

    Friend Class ItemDescriptionProvider
        Inherits TypeDescriptionProvider

        Private Shared ReadOnly DefaultTypeProvider As TypeDescriptionProvider = TypeDescriptor.GetProvider(GetType(Item))

        Public Sub New()
            MyBase.New(DefaultTypeProvider)
        End Sub

        Public Overrides Function GetTypeDescriptor(ByVal objectType As Type, ByVal instance As Object) As System.ComponentModel.ICustomTypeDescriptor
            Return New ItemTypeDescriptor(MyBase.GetTypeDescriptor(objectType, instance))
        End Function
    End Class

    Friend Class ItemTypeDescriptor
        Inherits CustomTypeDescriptor

        Public Shared CustomFields As List(Of PropertyDescriptor) = New(_, _)()

        Public Sub New(ByVal parent As System.ComponentModel.ICustomTypeDescriptor)
            MyBase.New(parent)
        End Sub

        Public Overrides Function GetProperties() As PropertyDescriptorCollection
            Return New PropertyDescriptorCollection(MyBase.GetProperties().Cast(Of PropertyDescriptor)().Union(CustomFields).ToArray())
        End Function

        Public Overrides Function GetProperties(ByVal attributes As Attribute()) As PropertyDescriptorCollection
            Return New PropertyDescriptorCollection(MyBase.GetProperties(attributes).Cast(Of PropertyDescriptor)().Union(CustomFields).ToArray())
        End Function
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
