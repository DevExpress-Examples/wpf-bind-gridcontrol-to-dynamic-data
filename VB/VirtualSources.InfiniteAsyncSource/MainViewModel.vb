Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Input
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Xpf
Imports DevExpress.Xpf.Data

Namespace VirtualSources.InfiniteAsyncSource

    Public Class MainViewModel
        Inherits ViewModelBase

        Private ReadOnly _items As ObservableCollection(Of Item) = New ObservableCollection(Of Item)()

        Public ReadOnly Property CustomFields As PropertyDescriptorCollection

        Public Sub New()
            _items = New ObservableCollection(Of Item)(Enumerable.Range(0, 10).[Select](Function(i) New Item With {.Id = i, .Name = $"Item {i}", .CreatedAt = Date.Now.AddDays(i)}))
            CustomFields = New PropertyDescriptorCollection(TypeDescriptor.GetProperties(GetType(Item)).Cast(Of PropertyDescriptor)().ToArray())
            FetchRowsCommand = New DelegateCommand(Of FetchRowsAsyncArgs)(AddressOf FetchRows)
        End Sub

        Public Sub FetchRows(ByVal e As FetchRowsAsyncArgs)
            If _items IsNot Nothing Then e.Result = Task.FromResult(New FetchRowsResult(_items.Cast(Of Object)().ToArray(), False))
        End Sub

        Public ReadOnly Property FetchRowsCommand As ICommand
    End Class

    Public Class Item
        Inherits BindableBase

        Private ReadOnly _customFieldValues As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()

        Public Property CreatedAt As Date
            Get
                Return GetProperty(Function() Me.CreatedAt)
            End Get

            Set(ByVal value As Date)
                SetProperty(Function() CreatedAt, value)
            End Set
        End Property

        Public Property Id As Integer
            Get
                Return GetProperty(Function() Me.Id)
            End Get

            Set(ByVal value As Integer)
                SetProperty(Function() Id, value)
            End Set
        End Property

        Public Property Name As String
            Get
                Return GetProperty(Function() Me.Name)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() Name, value)
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
End Namespace
