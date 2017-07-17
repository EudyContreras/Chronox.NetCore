using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Chronox.Wrappers
{
    internal class GroupWrapper : IComparable<GroupWrapper> , IEquatable<GroupWrapper>,  IEqualityComparer<GroupWrapper>
    {
        public Group Group { get; set; }

        public string Value { get; set; }

        public string Name { get; set; }

        public int Index { get; set; }

        public bool GroupUsed { get; set; }

        public GroupWrapper(Group group)
        {
            Group = group;
            Value = group.Value;
            Index = group.Index;
            Name = group.Name;
        }

        public GroupWrapper(Capture group)
        {
            Group = null;
            Value = group.Value;
            Index = group.Index;
            //Name = group.Name;
        }

        public override string ToString() => Value;

        public int CompareTo(GroupWrapper other)
        {
            var a = Value.CompareTo(other.Value);

            var b = Name.CompareTo(other.Name);

            return a / b;
        }

        public bool Equals(GroupWrapper x, GroupWrapper y)
        {
            return x.Name.Equals(y.Name) && x.Value.Equals(y.Value) && x.Index.Equals(y.Index);
        }

        public int GetHashCode(GroupWrapper obj)
        {
            return obj.Name.GetHashCode() + obj.Value.GetHashCode() + obj.Index.GetHashCode();
        }

        public bool Equals(GroupWrapper other)
        {
            return Name.Equals(other.Name) && Value.Equals(other.Value) && Index.Equals(other.Index);
        }
    }
}
