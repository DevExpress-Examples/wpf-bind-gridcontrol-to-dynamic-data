Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Windows.Input
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native

Namespace ITypedList

    Public Class MainViewModel
        Inherits ViewModelBase

        Public ReadOnly Property Items As ItemCollection

        Shared Sub New()
            ItemCollection.CustomFields.Add(New ItemPropertyDescriptor(New CustomField("Name", GetType(String))))
            ItemCollection.CustomFields.Add(New ItemPropertyDescriptor(New CustomField("CreatedAt", GetType(Date))))
        End Sub

        Public Sub New()
            Items = New ItemCollection(Enumerable.Range(0, 10).[Select](Function(i) New Item With {.Id = i}))
            Items.ForEach(Sub(x) x("Name") = $"Item {Items.IndexOf(x)}")
            Items.ForEach(Sub(x) x("CreatedAt") = Date.Now.AddDays(Items.IndexOf(x)))
            AddColumnCommand = New DelegateCommand(AddressOf AddColumn)
        End Sub

        Public Sub AddColumn()
            Dim fieldCount = ItemCollection.CustomFields.Count
            ItemCollection.CustomFields.Add(New ItemPropertyDescriptor(New CustomField($"Value {fieldCount}", GetType(Integer))))
            Items.ForEach(Sub(x) x($"Value {fieldCount}") = Items.IndexOf(x) * fieldCount)
        End Sub

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
End Namespace
