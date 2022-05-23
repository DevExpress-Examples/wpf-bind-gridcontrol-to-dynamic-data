using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ITypedList.ViewModels;

namespace ITypedList {
    internal class CustomField {
        public CustomField(string name, Type dataType) {
            Name = name;
            DataType = dataType;
        }

        public string Name { get; }

        public Type DataType { get; }
    }

    public class ItemCollection : ObservableCollection<Item>, System.ComponentModel.ITypedList {
        public static List<PropertyDescriptor> CustomFields = new();

        public ItemCollection(IEnumerable<Item> collection) : base(collection) { }

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors) {
            return new PropertyDescriptorCollection(TypeDescriptor.GetProperties(typeof(Item))
                                                                  .Cast<PropertyDescriptor>().Union(CustomFields)
                                                                  .ToArray());
        }

        public string GetListName(PropertyDescriptor[] listAccessors) {
            return nameof(Item);
        }
    }

    internal class ItemPropertyDescriptor : PropertyDescriptor {
        public ItemPropertyDescriptor(CustomField customField)
            : base(customField.Name, Array.Empty<Attribute>()) {
            CustomField = customField;
        }

        public CustomField CustomField { get; }

        public override Type ComponentType => typeof(Item);

        public override bool IsReadOnly => false;

        public override Type PropertyType => CustomField.DataType;

        public override bool CanResetValue(object component) {
            return false;
        }

        public override object GetValue(object component) {
            var item = (Item)component;
            return item[CustomField.Name] ??
                   (CustomField.DataType.IsValueType ? Activator.CreateInstance(CustomField.DataType) : null);
        }

        public override void ResetValue(object component) {
            throw new NotImplementedException();
        }

        public override void SetValue(object component, object value) {
            var item = (Item)component;
            item[CustomField.Name] = value;
        }

        public override bool ShouldSerializeValue(object component) {
            return false;
        }
    }
}