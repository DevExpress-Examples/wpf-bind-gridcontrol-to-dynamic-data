Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Windows.Input
Imports DevExpress.Data
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.Xpf

Namespace Unbound_Columns

    Public Class MainViewModel
        Inherits ViewModelBase

        Public Property Columns As ObservableCollection(Of Column)
            Get
                Return GetProperty(Function() Me.Columns)
            End Get

            Set(ByVal value As ObservableCollection(Of Column))
                SetProperty(Function() Columns, value)
            End Set
        End Property

        Public ReadOnly Property Items As ObservableCollection(Of Item)

        Public Sub New()
            Items = New ObservableCollection(Of Item)(Enumerable.Range(0, 10).[Select](Function(i) New Item With {.Id = i}))
            Items.ForEach(Sub(x)
                x("Name") = $"Item {Items.IndexOf(x)}"
                x("CreatedAt") = Date.Now.AddDays(Items.IndexOf(x))
            End Sub)
            Columns = New ObservableCollection(Of Column) From {New Column() With {.FieldName = "Id"}, New Column() With {.FieldName = "Name", .UnboundType = UnboundColumnType.String}, New Column() With {.FieldName = "CreatedAt", .UnboundType = UnboundColumnType.DateTime}}
            OnCustomUnboundColumnDataCommand = New DelegateCommand(Of UnboundColumnRowArgs)(AddressOf OnCustomUnboundColumnData)
            AddColumnCommand = New DelegateCommand(AddressOf AddColumn)
        End Sub

        Public Sub OnCustomUnboundColumnData(ByVal e As UnboundColumnRowArgs)
            If e.IsGetData Then e.Value = Items(e.SourceIndex)(e.FieldName)
            If e.IsSetData Then Items(e.SourceIndex)(e.FieldName) = e.Value
        End Sub

        Public Sub AddColumn()
            Columns.Add(New Column With {.FieldName = $"Value {Columns.Count}", .UnboundType = UnboundColumnType.Integer})
        End Sub

        Public ReadOnly Property OnCustomUnboundColumnDataCommand As ICommand

        Public ReadOnly Property AddColumnCommand As ICommand
    End Class

    Public Class Item
        Inherits BindableBase

        Private ReadOnly _customFieldValues As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()

        Public Property Id As Integer
            Get
                Return GetProperty(Function() Me.Id)
            End Get

            Set(ByVal value As Integer)
                SetProperty(Function() Id, value)
            End Set
        End Property

        Default Public Property Item(ByVal fieldName As String) As Object
            Get
                Dim value As Object = Nothing
                _customFieldValues.TryGetValue(fieldName, value)
                Return value
            End Get

            Set(ByVal value As Object)
                _customFieldValues(fieldName) = value
                RaisePropertyChanged(fieldName)
            End Set
        End Property
    End Class

    Public Class Column
        Inherits BindableBase

        Public Property FieldName As String
            Get
                Return GetProperty(Function() Me.FieldName)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() FieldName, value)
            End Set
        End Property

        Public Property UnboundType As UnboundColumnType?
            Get
                Return GetProperty(Function() Me.UnboundType)
            End Get

            Set(ByVal value As UnboundColumnType?)
                SetProperty(Function() UnboundType, value)
            End Set
        End Property
    End Class
End Namespace
