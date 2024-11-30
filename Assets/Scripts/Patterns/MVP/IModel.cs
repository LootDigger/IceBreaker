using System;

public interface IModel<T> where T: struct
{
    event Action<int> OnModelChangedEvent;
    T Value { get; set; }
}
