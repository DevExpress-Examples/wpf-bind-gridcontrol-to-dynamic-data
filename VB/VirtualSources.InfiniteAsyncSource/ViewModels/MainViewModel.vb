Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Threading.Tasks
Imports DevExpress.Mvvm.CodeGenerators
Imports DevExpress.Xpf.Data

Namespace VirtualSources.InfiniteAsyncSource.ViewModels

    <GenerateViewModel>
    Public Partial Class MainViewModel

        Private ReadOnly _items As ObservableCollection(Of Item) = New(_, _)()

        <GenerateProperty>
        Private _source As DevExpress.Xpf.Data.InfiniteAsyncSource

        Public Sub New()
            _items = New ObservableCollection(Of Item)(Enumerable.Range(0, 10).[Select](Function(i) New Item With {.Id = i, .Name = $"Item {i}", .CreatedAt = Date.Now.AddDays(i)}))
            Source = New DevExpress.Xpf.Data.InfiniteAsyncSource()
            Source.CustomProperties = New PropertyDescriptorCollection(TypeDescriptor.GetProperties(GetType(Item)).Cast(Of PropertyDescriptor)().ToArray())
            Source.FetchRows += Function(__, e)
                If _items IsNot Nothing Then e.Result = Task.FromResult(New FetchRowsResult(_items.Cast(Of Object)().ToArray(), False))
            End Function
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
