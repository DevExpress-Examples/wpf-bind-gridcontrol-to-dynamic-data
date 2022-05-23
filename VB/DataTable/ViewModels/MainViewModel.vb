Imports System
Imports System.Data
Imports DevExpress.Mvvm.CodeGenerators

Namespace DataTable.ViewModels

    <GenerateViewModel>
    Public Partial Class MainViewModel

        <GenerateProperty>
        Private _items As Data.DataTable

        Public Sub New()
            _items = New Data.DataTable()
            _items.Columns.Add(New DataColumn("Id", GetType(Integer)))
            _items.Columns.Add(New DataColumn("Name", GetType(String)))
            _items.Columns.Add(New DataColumn("CreatedAt", GetType(Date)))
            _items.Columns.Add(New DataColumn("Value", GetType(Integer), "Id * 10"))
            For i = 0 To 10 - 1
                _items.Rows.Add(i, $"Item {i}", Date.Today.AddDays(i))
            Next
        End Sub

        <GenerateCommand>
        Public Sub AddColumn()
            _items.Columns.Add(New DataColumn($"Value {_items.Columns.Count}", GetType(Integer), $"Id * {_items.Columns.Count * 10}"))
        End Sub
    End Class
End Namespace
