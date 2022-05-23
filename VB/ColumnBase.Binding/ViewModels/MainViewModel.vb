Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Mvvm.CodeGenerators

Namespace ColumnBase.Binding.ViewModels

    <GenerateViewModel>
    Public Partial Class MainViewModel

        <GenerateProperty>
        Private _items As ObservableCollection(Of IBaseItem)

        Public Sub New()
            _items = New ObservableCollection(Of IBaseItem)(Enumerable.Range(0, 10).[Select](Function(i) New Item With {.Id = i, .Name = $"Item {i}", .CreatedAt = Date.Today.AddDays(i)}))
        End Sub
    End Class

    Public Interface IBaseItem

        Property CreatedAt As Date

    End Interface

    Public Interface IItem
        Inherits IBaseItem

        Property Id As Integer

        Property Name As String

    End Interface

    <GenerateViewModel>
    Public Partial Class Item
        Implements IItem

        <GenerateProperty>
        Private _createdAt As Date

        <GenerateProperty>
        Private _id As Integer

        <GenerateProperty>
        Private _name As String
    End Class
End Namespace
