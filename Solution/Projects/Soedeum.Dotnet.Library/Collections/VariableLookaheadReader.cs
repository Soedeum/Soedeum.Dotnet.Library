using System;
using System.Collections.Generic;

namespace Soedeum.Dotnet.Library.Collections
{
    public class VariableLookaheadReader<T> : BaseLookaheadReader<T>
    {
        List<T> buffer = new List<T>();

        int index;


        public VariableLookaheadReader(IEnumerator<T> enumerator, Func<T, T> generateEndItem = null)
            : base(enumerator, generateEndItem) { }

        protected int Index { get => index; set => index = value; }

        protected int Size { get => buffer.Count; }


        protected virtual bool CanReset { get => true; }


        protected override void VerifyLookahead(int lookahead = 0)
        {
            // Find out if we have enough lookahead.
            int available = (Size - index);

            int difference = available - lookahead;

            // Prefetch (d + 1) items (since index and lookaheads are indices)
            if (difference <= 0)
                Prefetch(difference + 1);
        }

        private void Prefetch(int amount)
        {
            if (EndFound)
                return;

            int lastPosition = Size;

            for (int i = 0; i < amount; i++)
            {
                bool success = GetNext(out T next);

                if (success)
                {
                    buffer.Add(next);
                }
                else
                {
                    EndPosition = lastPosition + i;
                    break;
                }
            }
        }

        protected override T RawPeek(int lookahead = 0)
        {
            // We don't want to add infinite end items, the set one should just be returned
            if (EndFound && Position + lookahead >= EndPosition)
                return LastItem;
            else
                return buffer[index + lookahead];
        }

        // Prefetch(0) should've done this for us
        protected override void Initialize() { }

        protected override void MoveToNext()
        {
            index++;

            if (index == Size && CanReset)
            {
                index = 0;
                buffer.Clear();                
            }

            Prefetch(1);
        }
    }
}