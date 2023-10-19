using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConcurrentCollectionsPerfTest
{


    public class ConcurentLinkedList<T>: IEnumerable<T>, IEnumerable, ICollection   
    {
        private class _concurentlLinkedListNodeValue
        {
            public T Value;
        }


        private class _concurentlLinkedListNode
        {
            public _concurentlLinkedListNodeValue Item = new _concurentlLinkedListNodeValue();
            //public bool IsRemoved = false;
            //public _concurentlLinkedListNode<T> Previous = null;
            public _concurentlLinkedListNode Next = null;

            //public void SetPrevious(_concurentlLinkedListNode<T> previous)
            //{
            //    Interlocked.Exchange(ref Previous, previous);
            //}

            public void SetNext(_concurentlLinkedListNode next)
            {
                Interlocked.Exchange(ref Next, next);
            }
        }

        private class _concurentlLinkedListEnumerator : IEnumerator<T>, IEnumerator 
        {
            public _concurentlLinkedListEnumerator(ConcurentLinkedList<T> list)
            {
                _list = list;                
            }

            private ConcurentLinkedList<T> _list;
            private _concurentlLinkedListNode _currentNode = null;
            private _concurentlLinkedListNodeValue _currentValue = null;


            public T Current => _currentValue.Value;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _list = null;
                _currentNode = null;
                _currentValue = null;
            }

            public bool MoveNext()
            {
                if (_currentNode == null)                
                    _currentNode = _list._root;



                for (; (_currentNode != null) && (_currentNode.Item == null); _currentNode = _currentNode.Next) ;

                if (_currentNode !=null)
                {
                    _currentValue = _currentNode.Item;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                _currentNode = null;
                _currentNode = null;
            }
        }


        public ConcurentLinkedList()
        {
            _root = new _concurentlLinkedListNode();
            _last = _root;
            _root.Item = null;
        }



        private _concurentlLinkedListNode _root;
        private _concurentlLinkedListNode _last;
        private SpinLock _frontAccessLock = new SpinLock();
        private long _count = 0;

        public int Count => (int)_count;
        public bool IsSynchronized => true;
        public object SyncRoot => null;

        public void AddLast(T item)
        {
            _concurentlLinkedListNode newNode = new _concurentlLinkedListNode();

            newNode.Item.Value = item;

            _concurentlLinkedListNode oldNode = Interlocked.Exchange(ref _last, newNode);
            oldNode.SetNext(newNode);

            Interlocked.Increment(ref _count);
        }
        
        public void Clear()
        {
            //not fully thread safe
            //but shall be enough for purposes of perf testing

            if (_root.Next == null)
                return;

            var lastNode = _last;

            _root.SetNext(lastNode);
            lastNode.Item = null;

            Interlocked.Exchange(ref _count, 0);
        }


        public void RemoveFirst()
        {
            if (_root.Next == null)
                return;

            bool lockStat = false;
            do
                _frontAccessLock.Enter(ref lockStat);
            while (!lockStat);

            if (_root.Next.Next == null)            
                _root.Next.Item = null;            
            else            
                _root.Next = _root.Next.Next;

            _frontAccessLock.Exit();

            Interlocked.Decrement(ref _count);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new _concurentlLinkedListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException("Not supported feature");
        }
    }



}
