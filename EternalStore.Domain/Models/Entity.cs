namespace EternalStore.Domain.Models
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        protected virtual object Actual => this;

        public override bool Equals(object obj)
        {
            var other = obj as Entity;

            if (other == null) return false;

            if (ReferenceEquals(this, other)) return true;

            if (Actual.GetType() != other.Actual.GetType()) return false;

            if (Id == 0 || other.Id == 0) return false;

            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null) return true;

            if (a is null || b is null) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b) => !(a == b);

        public override int GetHashCode() => (Actual.GetType().ToString() + Id).GetHashCode();
    }
}
