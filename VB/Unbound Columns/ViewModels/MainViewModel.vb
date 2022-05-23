Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports DevExpress.Data
Imports DevExpress.Mvvm.CodeGenerators
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.Xpf

Namespace Unbound_Columns.ViewModels

    <GenerateViewModel>
    Public Partial Class MainViewModel

        <GenerateProperty>
        Private _columns As ObservableCollection(Of Column)

        <GenerateProperty>
        Private _items As ObservableCollection(Of Item)

        Public Sub New()
            _items = New ObservableCollection(Of Item)(Enumerable.Range(0, 10).[Select](Function(i) New Item With {.Id = i}))
            _items.ForEach(Sub(x)
                x("Name") = $"Item {_items.IndexOf(x)}"
                x("CreatedAt") = Date.Now.AddDays(_items.IndexOf(x))
            End Sub)
            _columns = New ObservableCollection(Of Column) From {New(_, _) With {.FieldName = "Id"}, New(_, _) With {.FieldName = "Name", .UnboundType = UnboundColumnType.String}, New(_, _) With {.FieldName = "CreatedAt", .UnboundType = UnboundColumnType.DateTime}}
        End Sub

        <GenerateCommand>
        Public Sub OnCustomUnboundColumnData(ByVal e As UnboundColumnRowArgs)
            If e.IsGetData Then e.Value = _items(e.SourceIndex)(e.FieldName)
            If e.IsSetData Then _items(e.SourceIndex)(e.FieldName) = e.Value
        End Sub

        <GenerateCommand>
        Public Sub AddColumn()
            _columns.Add(New Column With {.FieldName = $"Value {_columns.Count}", .UnboundType = UnboundColumnType.Integer})
        End Sub
    End Class

    <GenerateViewModel>
    Public Partial Class Item
        Implements INotifyPropertyChanged

        Private ReadOnly _customFieldValues As Dictionary(Of String, Object) = New(_, _)()

        <GenerateProperty>
        Private _id As Integer

        Default Public Property Item(ByVal fieldName As String) As Object
            Get
                Dim value As Object = Nothing
                _customFieldValues.TryGetValue(fieldName, value)
                Return value
            End Get

            Set(ByVal value As Object)
                _customFieldValues(fieldName) = value
                OnPropertyChanged(fieldName)
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Overridable Sub OnPropertyChanged(<CallerMemberName> ByVal Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class

    <GenerateViewModel>
    Public Partial Class Column

        <GenerateProperty>
        Private _fieldName As String

        <GenerateProperty>
        Private _unboundType As UnboundColumnType?
    End Class
End Namespace
