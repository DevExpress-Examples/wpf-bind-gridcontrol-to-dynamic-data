Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports DevExpress.Mvvm.CodeGenerators
Imports DevExpress.Mvvm.Native

Namespace ICustomTypeDescriptor.ViewModels

    <GenerateViewModel>
    Public Partial Class MainViewModel

        <GenerateProperty>
        Private _items As ObservableCollection(Of Item)

        Shared Sub New()
            ItemTypeDescriptor.CustomFields.Add(New ItemPropertyDescriptor(New CustomField("Name", GetType(String))))
            ItemTypeDescriptor.CustomFields.Add(New ItemPropertyDescriptor(New CustomField("CreatedAt", GetType(Date))))
        End Sub

        Public Sub New()
            _items = New ObservableCollection(Of Item)(Enumerable.Range(0, 10).[Select](Function(i) New Item With {.Id = i}))
            _items.ForEach(Sub(x) x("Name") = $"Item {_items.IndexOf(x)}")
            _items.ForEach(Sub(x) x("CreatedAt") = Date.Now.AddDays(_items.IndexOf(x)))
        End Sub

        <GenerateCommand>
        Public Sub AddColumn()
            Dim fieldCount = ItemTypeDescriptor.CustomFields.Count
            ItemTypeDescriptor.CustomFields.Add(New ItemPropertyDescriptor(New CustomField($"Value {fieldCount}", GetType(Integer))))
            _items.ForEach(Sub(x) x($"Value {fieldCount}") = _items.IndexOf(x) * fieldCount)
        End Sub
    End Class

    <GenerateViewModel>
    <TypeDescriptionProvider(GetType(ItemDescriptionProvider))>
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
End Namespace
