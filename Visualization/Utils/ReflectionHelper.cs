using System;
using System.ComponentModel;
using System.Linq;

namespace Visualization {
    public static class ReflectionHelper {
        public static PropertyDescriptor GetProperty(object item, string member) {
            //we avoid all checks and assume than the properties have been set correctly or whether this item has such a member.
            return TypeDescriptor.GetProperties(item).Find(member, true);
        }
    }
}
