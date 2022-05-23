Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm.CodeGenerators

Namespace ExpandoObject.ViewModels

    <GenerateViewModel>
    Public Partial Class MainViewModel

        <GenerateProperty>
        Private _items As ObservableCollection(Of Dynamic.ExpandoObject)

        Public Sub New()
            _items = New ObservableCollection(Of Dynamic.ExpandoObject)()
            For i = 0 To 10 - 1
                Dim item As dynamic = New Dynamic.ExpandoObject()
                item.Id = i
                item.Name = $"Item {i}"
                item.CreatedAt = Date.Now.AddDays(i)
                _items.Add(item)
            Next
        End Sub

        <GenerateCommand>
        Public Sub AddColumn()
            For Each item In _items
                Dim dict = CType(item, IDictionary(Of String, Object))
                dict.Add($"Value {dict.Keys.Count}", _items.IndexOf(item) * dict.Keys.Count)
            Next
        End Sub
    End Class
End Namespace
