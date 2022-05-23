Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports DevExpress.Data
Imports DevExpress.Mvvm.CodeGenerators
Imports DevExpress.Mvvm.Native

Namespace UnboundSource.ViewModels

    <GenerateViewModel>
    Public Partial Class MainViewModel

        Private ReadOnly _items As ObservableCollection(Of Item)

        Private ReadOnly _random As Random = New(_, _)()

        Private _columnCount As Integer = 2

        <GenerateProperty>
        Private _unboundSource As DevExpress.Data.UnboundSource

        Public Sub New()
            _items = New ObservableCollection(Of Item)(Enumerable.Range(0, 10).[Select](Function(i) New Item With {.Id = i, .Name = $"Item {i}"}))
            _items.ForEach(Sub(item) item("Value 1") = Convert.ToDouble(item.Id))
            UnboundSource = New DevExpress.Data.UnboundSource()
            UnboundSource.ValueNeeded += Function(__, e)
                If EqualityComparer(Of String).Default.Equals(e.PropertyName, NameOf(Item.Id)) Then
                    e.Value = _items(e.RowIndex).Id
                ElseIf EqualityComparer(Of String).Default.Equals(e.PropertyName, NameOf(Item.Name)) Then
                    e.Value = _items(e.RowIndex).Name
                ElseIf EqualityComparer(Of String).Default.Equals(e.PropertyName, NameOf(Item.CreatedAt)) Then
                    e.Value = _items(e.RowIndex).CreatedAt
                Else
                    e.Value = _items(e.RowIndex)(e.PropertyName)
                End If
            End Function
            UnboundSource.ValuePushed += Function(__, e)
                If EqualityComparer(Of String).Default.Equals(e.PropertyName, NameOf(Item.Id)) Then
                    _items(e.RowIndex).Id = CInt(e.Value)
                ElseIf EqualityComparer(Of String).Default.Equals(e.PropertyName, NameOf(Item.Name)) Then
                    _items(e.RowIndex).Name = CStr(e.Value)
                ElseIf EqualityComparer(Of String).Default.Equals(e.PropertyName, NameOf(Item.CreatedAt)) Then
                    _items(e.RowIndex).CreatedAt = CDate(e.Value)
                Else
                    _items(e.RowIndex)(e.PropertyName) = e.Value
                End If
            End Function
            Properties.Add(New UnboundSourceProperty(NameOf(Item.Id), GetType(Item).GetProperty(NameOf(Item.Id))?.PropertyType))
            Properties.Add(New UnboundSourceProperty(NameOf(Item.Name), GetType(Item).GetProperty(NameOf(Item.Name))?.PropertyType))
            Properties.Add(New UnboundSourceProperty("Value 1", GetType(Double)))
            UnboundSource.SetRowCount(_items.Count)
        End Sub

        <GenerateCommand>
        Private Sub AddColumn()
            _items.ForEach(Sub(item) item($"Value {_columnCount}") = _random.NextDouble())
            UnboundSource?.Properties.Add(New UnboundSourceProperty($"Value {_columnCount}", GetType(Double)))
            UnboundSource?.SetRowCount(_items.Count)
            _columnCount += 1
        End Sub
    End Class

    <GenerateViewModel>
    Public Partial Class Item
        Implements INotifyPropertyChanged

        Private ReadOnly _customFieldValues As Dictionary(Of String, Object) = New(_, _)()

        <GenerateProperty>
        Private _createdAt As Date

        <GenerateProperty>
        Private _id As Integer

        <GenerateProperty>
        Private _name As String

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
End Namespace
