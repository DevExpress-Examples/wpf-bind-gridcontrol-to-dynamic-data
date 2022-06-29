Imports System.Windows
Imports System.Windows.Controls

Namespace Unbound_Columns

    Public Class ColumnTemplateSelector
        Inherits DataTemplateSelector

        Public Property ColumnTemplate As DataTemplate

        Public Property UnboundColumnTemplate As DataTemplate

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim column As Column = Nothing
            Return If(CSharpImpl.__Assign(column, TryCast(item, Column)) IsNot Nothing, If(column.UnboundType.HasValue, UnboundColumnTemplate, ColumnTemplate), MyBase.SelectTemplate(item, container))
        End Function

        Private Class CSharpImpl

            <System.Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace
