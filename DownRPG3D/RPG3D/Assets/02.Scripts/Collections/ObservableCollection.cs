﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace RPG.Collections
{
    [Serializable]
    public class ObservableCollection<T> : INotifyCollectionChanged<T>, IEnumerable<T>
    {
        public T this[int index]
        {
            get => items[index];
            set => Change(index, value);
        }

        public int Count => items.Count;

        public event Action<int, T> onItemChanged;
        public event Action<int, T> onItemAdded;
        public event Action<int, T> onItemRemoved;
        public event Action onCollectionChanged;

        public List<T> items = new List<T>();

        public ObservableCollection() { }

        public ObservableCollection(int count)
        {
            items = new List<T>();
            for (int i = 0; i < count; i++)
            {
                items.Add(default(T));
            }
        }

        public ObservableCollection(IEnumerable<T> copy)
        {
            items = new List<T>(copy);
        }

        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (match(items[i]))
                    return items[i];
            }
            return default;
        }

        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (match(items[i]))
                    return i;
            }
            return -1;
        }

        public void Change(int index, T item)
        {
            items[index] = item;
            onItemChanged?.Invoke(index, item);
            onCollectionChanged?.Invoke();
        }

        public void Swap(int index1, int index2)
        {
            if (index1 >= Count || index1 < 0 || index2 >= Count || index2 < 0)
                throw new IndexOutOfRangeException();

            T item2 = items[index2];
            Change(index2, items[index1]);
            Change(index1, item2);
        }

        public void Add(T item)
        {
            items.Add(item);
            onItemAdded?.Invoke(items.Count - 1, item);
            onCollectionChanged?.Invoke();
        }

        public bool Remove(T item)
        {
            int index = items.IndexOf(item);
            if (index < 0)
                return false;

            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            T tmp = items[index];
            items.RemoveAt(index);
            onItemRemoved?.Invoke(index, tmp);
            onCollectionChanged?.Invoke();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
