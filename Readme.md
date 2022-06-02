<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/495333708/21.2.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1091075)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
## How to Bind GridControl to Dynamic Data
This example demonstrates how to bind GridControl to dynamic data in several different ways.

### The Task
GridControl is a data-aware control. It displays data from a source collection. The data is usually pre-determined and can be easily obtained with property descriptors for a class that represents source records.

This data structure does not always satisfy the developer needs. One of the possible requirements is that values should be calculated at runtime and submitted into a collection of an arbitrary size. It may not always be possible to use predefined accessors for such values.

### The Standard Technique
The standard WPF data binding solves this problem. Provided the correct path, the data will be displayed anywhere you need it. In fact, you can also use it with GridControl - see [Binding Columns to Data Source Fields](https://docs.devexpress.com/WPF/120400/controls-and-libraries/data-grid/grid-view-data-layout/columns-and-card-fields/binding-columns-to-data-source-fields). This technique has its flaws. In certain cases and with excessive amounts of data, bindings may show significant performance issues.

We recommend that you use bindings for columns only when it is necessary. For example, to display unsupported data formats with custom cell templates or to make GridControl work with interface inheritance.

### The DevExpress Technique
* The GridControl has the [Unbound Columns](https://docs.devexpress.com/WPF/6124/controls-and-libraries/data-grid/grid-view-data-layout/columns-and-card-fields/unbound-columns) functionality suitable for dynamic data. Handle the ([CustomUnboundColumnData](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridControl.CustomUnboundColumnData)) event to manually fetch and save edited data;
* [UnboundSource](https://docs.devexpress.com/CoreLibraries/DevExpress.Data.UnboundSource) is a separate component that works in a similar way;
* You can also use our Virtual Data Sources for this task. See [Bind to any Data Source with Virtual Sources](https://docs.devexpress.com/WPF/10803/controls-and-libraries/data-grid/bind-to-data/bind-to-any-data-source-with-virtual-sources). Such sources support [Custom Properties](https://docs.devexpress.com/WPF/DevExpress.Xpf.Data.VirtualSourceBase.CustomProperties).

### Considerations

* Use [DataTable](https://docs.microsoft.com/en-us/dotnet/api/system.data.datatable?view=net-6.0). You have full control over its rows and columns;
* Use [ExpandoObject](https://docs.microsoft.com/en-us/dotnet/api/system.dynamic.expandoobject?view=net-6.0) to add members to your classes at runtime;
* Implement the [ICustomTypeDescriptor](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.icustomtypedescriptor?view=net-5.0)/[ITypedListInterface](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.itypedlist?view=netcore-3.1). These interfaces allow you to use custom property descriptors so you can change the way data is fetched from your classes.


### Stats

||Complexity*|Overall Performance (1M rows)**|Sorting|Filtering (even records)|Scrolling|
|--|--|--|--|--|--|
|DataTable|Simple|Reduced|0.5x|3.8x|0.6x|
|ExpandoObject|Average|Good|0.5x|1.4x|0.5x|
|ICustomTypeDescriptor|Difficult|Average|2x|1.5x|0.3x|
|ITypedList|Difficult|Average|1.5x|1.6x|0.4x|
|Unbound Columns|Simple|Average|1.5x|1.5x|0.4x|
|Unbound Source|Average|Reduced|3.6x|4x|0.4x|
|Virtual Data Source***|Average|Good|-|-|-|
\* The complexity is mostly measured by our subjective opinion and the aggregate necessity to create custom classes, implement interfaces, or handle events.
\*\* The exact time frames may differ depending on the machine specs or overall project implementation. This table shows relative values measured in multiple attempts on the same machine.
\*\*\* When using a virtual data source, adding new columns at runtime is only possible if you reset the source and fetch all rows from the start. The performance of common data shaping operations depends on the way the rows are fetched to the source.

