## How to bind GridControl to dynamic data
This example demonstrates how to bind GridControl to dynamic data in several different ways.
### What is the problem?
GridControl is a data-aware control. It displays data from a source collection. In most cases, this data is pre-determined and can be easily obtained using property descriptors for a class that represents source records.

However, this data structure does not alwas suffice developer needs. Sometimes, values should be calculated at runtime and fall into a collection of an arbitrary size. The worst part is that such a collection may grow in both directions - not only vertically, but also horizontally. It may not always be possible to use predefined acessors for such values.

### The standard way
The standard WPF data binding solves this problem. Provided the correct path, the data will be displayed anywhere you need it. In fact, you can also use it with GridControl - see [Binding Columns to Data Source Fields](https://docs.devexpress.com/WPF/120400/controls-and-libraries/data-grid/grid-view-data-layout/columns-and-card-fields/binding-columns-to-data-source-fields). However, this is not the optimal way. In certain cases and with excessive amounts of data, bindings may show significant performance issues.

We recommend using bindings for columns only when necessary. For example, to display unsupported data formats with custom cell templates or to make GridControl work with interface inheritance.

### What can we offer
* First and foremost, GridControl has its own predefined way to display any data in its cells - see [Unbound Columns](https://docs.devexpress.com/WPF/6124/controls-and-libraries/data-grid/grid-view-data-layout/columns-and-card-fields/unbound-columns). The key idea of this feature is to handle the special event ([CustomUnboundColumnData](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridControl.CustomUnboundColumnData)) to manually fetch and save edited data;
* [UnboundSource](https://docs.devexpress.com/CoreLibraries/DevExpress.Data.UnboundSource) is a separate component that works in a similar way;
* You can also use our Virtual Data Sources for this task. See [Bind to any Data Source with Virtual Sources](https://docs.devexpress.com/WPF/10803/controls-and-libraries/data-grid/bind-to-data/bind-to-any-data-source-with-virtual-sources). Such sources support [Custom Properties](https://docs.devexpress.com/WPF/DevExpress.Xpf.Data.VirtualSourceBase.CustomProperties).

### What else

* Use [DataTable](https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable?view=net-6.0). You have full control over its rows and columns;
* Use [ExpandoObject](https://docs.microsoft.com/en-us/dotnet/api/system.dynamic.expandoobject?view=net-6.0) to add members to your classes at runtime;
* Implement the [ICustomTypeDescriptor](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.icustomtypedescriptor?view=net-5.0)/[ITypedListInterface](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.itypedlist?view=netcore-3.1). These interfaces allow you to use custom property descriptors so you can change the way data is fetched from your classes.


### Stats

||Complexity*|Overall Performance (1M rows)**|Sorting|Filtering (even records)|Scrolling
--|--|--|--|--|--
DataTable|Simple|Bad|0.5x|3.8x|0.6x
ExpandoObject|Medium|Good|0.5x|1.4x|0.5x
ICustomTypeDescriptor|Hard|Average|2x|1.5x|0.3x
ITypedList|Hard|Average|1.5x|1.6x|0.4x
Unbound Columns|Simple|Average|1.5x|1.5x|0.4x
Unbound Source|Medium|Bad|3.6x|4x|0.4x
Virtual Data Source***|Medium|Good|-|-|-|
\* The complexity is mostly measured by our subjective opinion and the aggregate necessity to create custom classes, implement interfaces, or handle events.
\*\* The exact time frames may differ depending on the machine specs or overall project implementation. This table shows relative values measured in multiple attempts on the same machine.
\*\*\* When using a virtual data source, adding new columns at runtime is only possible if you reset the source and fetch all rows from the start. The performance of common data shaping operations depends on the way the rows are fetched to the source.

