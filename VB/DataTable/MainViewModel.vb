Imports DevExpress.Mvvm
Imports System
Imports System.Data
Imports System.Windows.Input

Namespace DataTable

    Public Class MainViewModel
        Inherits ViewModelBase

        Public ReadOnly Property Items As Data.DataTable

        Public Sub New()
            Items = New Data.DataTable()
            Items.Columns.Add(New DataColumn("Id", GetType(Integer)))
            Items.Columns.Add(New DataColumn("Name", GetType(String)))
            Items.Columns.Add(New DataColumn("CreatedAt", GetType(Date)))
            Items.Columns.Add(New DataColumn("Value", GetType(Integer), "Id * 10"))
            For i = 0 To 10 - 1
                Items.Rows.Add(i, $"Item {i}", Date.Today.AddDays(i))
            Next

            AddColumnCommand = New DelegateCommand(AddressOf AddColumn)
        End Sub

        Public Sub AddColumn()
            Items.Columns.Add(New DataColumn($"Value {Items.Columns.Count}", GetType(Integer), $"Id * {Items.Columns.Count * 10}"))
        End Sub

        Public ReadOnly Property AddColumnCommand As ICommand
    End Class
End Namespace
