namespace Domain.Entities.Base
{
    public abstract class EntityBase<TId> : IEntityBase<TId>
    {
        public virtual TId Id { get; protected init; } = default!;


        private int? _requestedHashCode;

        private bool IsTransient()
        {
            return Id != null && Id.Equals(default(TId));
        }

        public override bool Equals(object? obj)
        {
            if (obj is not EntityBase<TId> item)
            {
                return false;
            }

            if (ReferenceEquals(this, item))
            {
                return true;
            }

            if (GetType() != item.GetType())
            {
                return false;
            }

            return (item.IsTransient() || IsTransient()) && false;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (Id != null) _requestedHashCode ??= Id.GetHashCode() ^ 31;

                return _requestedHashCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public static bool operator ==(EntityBase<TId>? left, EntityBase<TId>? right)
        {
            return left?.Equals(right) ?? Equals(right, null);
        }

        public static bool operator !=(EntityBase<TId> left, EntityBase<TId> right)
        {
            return !(left == right);
        }
    }
}
