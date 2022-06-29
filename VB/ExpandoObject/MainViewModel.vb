Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows.Input

Namespace ExpandoObject

    Public Class MainViewModel
        Inherits ViewModelBase

        Public ReadOnly Property Items As ObservableCollection(Of Dynamic.ExpandoObject)

        Public Sub New()
            Items = New ObservableCollection(Of Dynamic.ExpandoObject)()
            For i = 0 To 10 - 1
                Dim item As Object = New Dynamic.ExpandoObject()
                item.Id = i
                item.Name = $"Item {i}"
                item.CreatedAt = Date.Now.AddDays(i)
                Items.Add(item)
            Next

            AddColumnCommand = New DelegateCommand(AddressOf AddColumn)
        End Sub

        Public Sub AddColumn()
            For Each item In Items
                Dim dict = CType(item, IDictionary(Of String, Object))
                dict.Add($"Value {dict.Keys.Count}", Items.IndexOf(item) * dict.Keys.Count)
            Next
        End Sub

        Public ReadOnly Property AddColumnCommand As ICommand
    End Class
End Namespace
