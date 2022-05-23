using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ICustomTypeDescriptor.ViewModels;

namespace ICustomTypeDescriptor {
    internal class CustomField {
        public CustomField(string name, Type dataType) {
            Name = name;
            DataType = dataType;
        }

        public string Name { get; }

        public Type DataType { get; }
    }

    internal class ItemDescriptionProvider : TypeDescriptionProvider {
        private static readonly TypeDescriptionProvider DefaultTypeProvider =
            TypeDescriptor.GetProvider(typeof(Item));

        public ItemDescriptionProvider() : base(DefaultTypeProvider) { }

        public override System.ComponentModel.ICustomTypeDescriptor GetTypeDescriptor(Type objectType,
            object instance) {
            return new ItemTypeDescriptor(base.GetTypeDescriptor(objectType, instance));
        }
    }

    internal class ItemTypeDescriptor : CustomTypeDescriptor {
        public static List<PropertyDescriptor> CustomFields = new();

        public ItemTypeDescriptor(System.ComponentModel.ICustomTypeDescriptor parent)
            : base(parent) { }

        public override PropertyDescriptorCollection GetProperties() {
            return new PropertyDescriptorCollection(base.GetProperties()
                                                        .Cast<PropertyDescriptor>().Union(CustomFields).ToArray());
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes) {
            return new PropertyDescriptorCollection(base.GetProperties(attributes)
                                                        .Cast<PropertyDescriptor>().Union(CustomFields).ToArray());
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